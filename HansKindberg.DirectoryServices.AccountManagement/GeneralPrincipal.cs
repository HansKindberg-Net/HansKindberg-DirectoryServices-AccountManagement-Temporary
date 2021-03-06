﻿using System;
using System.DirectoryServices.AccountManagement;
using System.Reflection;

namespace HansKindberg.DirectoryServices.AccountManagement
{
	[DirectoryRdnPrefix("CN")]
	[DirectoryObjectClass("*)(|(objectClass=computer)(objectClass=group)(objectClass=user)")]
	public class GeneralPrincipal : Principal
	{
		#region Fields

		private static readonly FieldInfo _unpersistedField = typeof(Principal).GetField("unpersisted", BindingFlags.Instance | BindingFlags.NonPublic);

		#endregion

		#region Constructors

		public GeneralPrincipal(PrincipalContext context)
		{
			if(context == null)
				throw new ArgumentNullException("context");

			this.ContextRaw = context;
			_unpersistedField.SetValue(this, true);
		}

		#endregion

		#region Methods

		public static Principal Get(PrincipalContext context, Type principalType, string identityValue)
		{
			return FindByIdentityWithType(context, principalType, identityValue);
		}

		public static Principal Get(PrincipalContext context, Type principalType, IdentityType identityType, string identityValue)
		{
			return FindByIdentityWithType(context, principalType, identityType, identityValue);
		}

		#endregion
	}
}