using PrismWithRealm.Common.Services;
using PrismWithRealm.Domains.Interventions.Entities;
using Realms;
using System;

namespace PrismWithRealm.Domains.Interventions._Migrations
{
	public class Schema2 : IMigration
	{
		public ulong SchemaVersion => 2;

		public void OnMigrate(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion)
		{
			var interventions = migration.NewRealm.All<Intervention>();

			foreach (var item in interventions)
			{
				item.Libelle += $" - {SchemaVersion}";
			}
		}
	}

	public class Schema3 : IMigration
	{
		public ulong SchemaVersion => 3;

		public void OnMigrate(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion)
		{
			var interventions = migration.NewRealm.All<Intervention>();

			foreach (var item in interventions)
			{
				item.Libelle += $" - {SchemaVersion}";
			}
		}
	}
}
