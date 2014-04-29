using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Text;

namespace HansKindberg.DirectoryServices.AccountManagement.Connections
{
	public class PrincipalConnection : IPrincipalConnection
	{
		#region Fields

		private const string _defaultNameValueDelimiter = "=";
		private const string _defaultParameterDelimiter = ";";

		#endregion

		#region Properties

		public virtual string Container { get; set; }
		public virtual ContextType? ContextType { get; set; }
		public virtual string Name { get; set; }

		protected internal virtual string NameValueDelimiter
		{
			get { return _defaultNameValueDelimiter; }
		}

		public virtual ContextOptions? Options { get; set; }

		protected internal virtual string ParameterDelimiter
		{
			get { return _defaultParameterDelimiter; }
		}

		public virtual string Password { get; set; }
		public virtual string UserName { get; set; }

		#endregion

		#region Methods

		protected internal virtual void AddParameter(StringBuilder value, string parameterName, object parameterValue)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			if(string.IsNullOrEmpty(parameterName))
				throw new ArgumentException("The parameter-name can not be empty.", "parameterName");

			if(string.IsNullOrEmpty(parameterName.Trim()))
				throw new ArgumentException("The parameter-name can not only contain white-spaces.", "parameterName");

			var parameterDelimiter = !string.IsNullOrEmpty(value.ToString().Trim()) ? this.ParameterDelimiter : string.Empty;

			value.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[] {parameterDelimiter, parameterName, this.NameValueDelimiter, parameterValue ?? string.Empty}));
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Should be disposed by the caller. ")]
		public virtual IPrincipalContext CreatePrincipalContext()
		{
			ContextType contextType = this.ContextType.HasValue ? this.ContextType.Value : 0;
			ContextOptions? contextOptions = this.Options;

			if(!contextOptions.HasValue)
			{
				using(var defaultPrincipalContext = new PrincipalContext(contextType))
				{
					contextOptions = defaultPrincipalContext.Options;
				}
			}

			return (PrincipalContextWrapper) new PrincipalContext(contextType, this.Name, this.Container, contextOptions.Value, this.UserName, this.Password);
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			if(this.ContextType.HasValue)
				this.AddParameter(stringBuilder, "ContextType", this.ContextType.Value);

			if(!string.IsNullOrEmpty(this.Name))
				this.AddParameter(stringBuilder, "Name", this.Name);

			if(!string.IsNullOrEmpty(this.Container))
				this.AddParameter(stringBuilder, "Container", this.Container);

			if(!string.IsNullOrEmpty(this.UserName))
				this.AddParameter(stringBuilder, "UserName", this.UserName);

			if(!string.IsNullOrEmpty(this.Password))
				this.AddParameter(stringBuilder, "Password", this.Password);

			if(this.Options.HasValue)
				this.AddParameter(stringBuilder, "Options", this.Options.Value);

			return stringBuilder.ToString();
		}

		#endregion
	}
}