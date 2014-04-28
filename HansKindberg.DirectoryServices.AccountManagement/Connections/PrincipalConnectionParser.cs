using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;

namespace HansKindberg.DirectoryServices.AccountManagement.Connections
{
	public class PrincipalConnectionParser : IPrincipalConnectionParser
	{
		#region Fields

		private const char _defaultNameValueDelimiter = '=';
		private const char _defaultParameterDelimiter = ';';
		private static readonly IEqualityComparer<string> _defaultStringComparer = System.StringComparer.OrdinalIgnoreCase;
		private readonly string _nameValueDelimiter;
		private readonly string _parameterDelimiter;
		private readonly IEqualityComparer<string> _stringComparer;

		#endregion

		#region Constructors

		public PrincipalConnectionParser() : this(_defaultParameterDelimiter, _defaultNameValueDelimiter, _defaultStringComparer) {}

		public PrincipalConnectionParser(char parameterDelimiter, char nameValueDelimiter, IEqualityComparer<string> stringComparer)
		{
			string parameterDelimiterString = parameterDelimiter.ToString(CultureInfo.InvariantCulture);
			string nameValueDelimiterString = nameValueDelimiter.ToString(CultureInfo.InvariantCulture);

			if(parameterDelimiterString.Trim().Length == 0)
				throw new ArgumentException("The parameter-delimiter can not be empty.", "parameterDelimiter");

			if(nameValueDelimiterString.Trim().Length == 0)
				throw new ArgumentException("The name-value-delimiter can not be empty.", "nameValueDelimiter");

			if(stringComparer == null)
				throw new ArgumentNullException("stringComparer");

			this._nameValueDelimiter = nameValueDelimiterString;
			this._parameterDelimiter = parameterDelimiterString;
			this._stringComparer = stringComparer;
		}

		#endregion

		#region Properties

		public virtual string NameValueDelimiter
		{
			get { return this._nameValueDelimiter; }
		}

		public virtual string ParameterDelimiter
		{
			get { return this._parameterDelimiter; }
		}

		public virtual IEqualityComparer<string> StringComparer
		{
			get { return this._stringComparer; }
		}

		#endregion

		#region Methods

		protected internal virtual IDictionary<string, string> GetConnectionStringAsDictionary(string connectionString)
		{
			var dictionary = new Dictionary<string, string>(this.StringComparer);

			if(!string.IsNullOrEmpty(connectionString))
			{
				foreach(var nameValue in connectionString.Split(this.ParameterDelimiter.ToCharArray()))
				{
					var nameValueParts = nameValue.Split(this.NameValueDelimiter.ToCharArray(), 2);

					if(nameValueParts.Length == 0)
						continue;

					var value = nameValueParts.Length > 1 ? nameValueParts[1] : string.Empty;

					dictionary.Add(nameValueParts[0], value);
				}
			}

			return dictionary;
		}

		public virtual IPrincipalContext Parse(string connectionString)
		{
			if(connectionString == null)
				throw new ArgumentNullException("connectionString");

			var principalConnection = new PrincipalConnection();

			var dictionary = this.GetConnectionStringAsDictionary(connectionString);

			if(dictionary.Any())
			{
				try
				{
					foreach(var keyValuePair in dictionary)
					{
						this.TrySetValue(principalConnection, keyValuePair);
					}
				}
				catch(Exception exception)
				{
					throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The connection-string \"{0}\" could not be parsed.", connectionString), exception);
				}
			}

			return principalConnection;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected internal virtual void TrySetValue(PrincipalConnection principalConnection, KeyValuePair<string, string> keyValuePair)
		{
			if(principalConnection == null)
				throw new ArgumentNullException("principalConnection");

			if(string.IsNullOrEmpty(keyValuePair.Key))
				throw new FormatException("A key can not be empty.");

			switch(keyValuePair.Key.ToLowerInvariant())
			{
				case "container":
				{
					principalConnection.Container = keyValuePair.Value;
					break;
				}
				case "contexttype":
				{
					principalConnection.ContextType = (ContextType) Enum.Parse(typeof(ContextType), keyValuePair.Value);
					break;
				}
				case "name":
				{
					principalConnection.Name = keyValuePair.Value;
					break;
				}
				case "options":
				{
					principalConnection.Options = (ContextOptions) Enum.Parse(typeof(ContextOptions), keyValuePair.Value);
					break;
				}
				case "password":
				{
					principalConnection.Password = keyValuePair.Value;
					break;
				}
				case "username":
				{
					principalConnection.UserName = keyValuePair.Value;
					break;
				}
				default:
				{
					throw new FormatException(string.Format(CultureInfo.InvariantCulture, "The key \"{0}\" is not valid.", keyValuePair.Key));
				}
			}
		}

		#endregion
	}
}