using Prism.Ioc;
using Prism.Modularity;
using PrismWithRealm.Interfaces;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrismWithRealm.Common.Services
{
	public class RealmManager : IRealmManager
	{
		public static readonly ulong DATABASE_SCHEMA_VERSION = 4;
		public static readonly string DATABASE_PATH = "demo.realm";

		private readonly IContainerExtension containerExtension;
		private readonly IModuleCatalog moduleCatalog;
		
		public RealmManager(IContainerExtension containerExtension, IModuleCatalog moduleCatalog)
		{
			this.containerExtension = containerExtension;
			this.moduleCatalog = moduleCatalog;
		}

		public void Migrate()
		{
			var uri = RealmConfigurationBase.GetPathToRealm(DATABASE_PATH);

			bool databaseExist = File.Exists(uri);
			
			var configuration = new RealmConfiguration(DATABASE_PATH)
			{
				SchemaVersion = DATABASE_SCHEMA_VERSION,
				MigrationCallback = (migration, oldSchemaVersion) => OnMigration(migration, oldSchemaVersion, migration.NewRealm.Config.SchemaVersion)
			};

			Realm database = Realm.GetInstance(configuration);
			
			if (!databaseExist)
			{
				var transaction = database.BeginWrite();
				this.OnDataBaseCreated(database);
				transaction.Commit();
			}

			database.Dispose();
		}

		public Realm GetRealmInstance()
		{
			var configuration = new RealmConfiguration(DATABASE_PATH)
			{
				SchemaVersion = DATABASE_SCHEMA_VERSION
			};
			return Realm.GetInstance(configuration);
		}
		
		private void OnDataBaseCreated(Realm database)
		{
			var moduleInfos = moduleCatalog?.Modules?.Where(x => x.ModuleType.GetInterfaces().Contains(typeof(IRealmModule))).ToList();

			foreach (var module in moduleInfos)
			{
				var realmModule = (IRealmModule)containerExtension.Resolve(module.ModuleType);
				realmModule?.OnDataBaseCreated(database);
			}
		}

		private void OnMigration(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion)
		{
			var moduleInfos = moduleCatalog?.Modules?.Where(x => x.ModuleType.GetInterfaces().Contains(typeof(IRealmModule))).ToList();

			foreach (var module in moduleInfos)
			{
				var realmModule = (IRealmModule)containerExtension.Resolve(module.ModuleType);
				realmModule?.OnMigration(migration, oldSchemaVersion, newSchemaVersion);
			}
		}
	}

	public interface IRealmManager
	{
		Realm GetRealmInstance();

		void Migrate();
	}


	public abstract class RealmModule : IRealmModule
	{
		public abstract void OnInitialized(IContainerProvider containerProvider);

		public abstract void RegisterTypes(IContainerRegistry containerRegistry);

		public void OnMigration(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion)
		{
			var types = this.GetType().Assembly.GetTypes().Where(t => typeof(IMigration).IsAssignableFrom(t));

			foreach (var item in types)
			{
				var instance = Activator.CreateInstance(item) as IMigration;
				
				if(instance.SchemaVersion > oldSchemaVersion && instance.SchemaVersion <= newSchemaVersion)
				{
					instance.OnMigrate(migration, oldSchemaVersion, newSchemaVersion);
				}
			}

			OnDataBaseCreated(migration.NewRealm);
		}

		public abstract void OnDataBaseCreated(Realm database);
	}

	public interface IRealmModule : IModule
	{
		void OnDataBaseCreated(Realm database);

		void OnMigration(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion);
	}

	public interface IMigration
	{
		ulong SchemaVersion { get; }

		void OnMigrate(Migration migration, ulong oldSchemaVersion, ulong newSchemaVersion);
	}

	public static class RealmExtension
	{
		public static void HasData<T>(this Realm realm, params T[] realmObjects)
			where T : RealmObject, IPrimaryKeyObject
		{
			var listToRemove = realm.All<T>().ToList().Except(realmObjects, new IdComparer()).ToList();
			
			foreach (var itemRemove in listToRemove)
			{
				realm.Remove(itemRemove as RealmObject);
			}
			
			foreach (var realmObject in realmObjects)
			{
				realm.Add(realmObject, true);
			}
		}
	}

	public interface IPrimaryKeyObject
	{
		long Id { get; }
	}

	public class IdComparer : IEqualityComparer<IPrimaryKeyObject>
	{
		public int GetHashCode(IPrimaryKeyObject co)
		{
			if (co == null)
			{
				return 0;
			}
			return co.Id.GetHashCode();
		}

		public bool Equals(IPrimaryKeyObject x1, IPrimaryKeyObject x2)
		{
			if (object.ReferenceEquals(x1, x2))
			{
				return true;
			}
			if (object.ReferenceEquals(x1, null) ||
				object.ReferenceEquals(x2, null))
			{
				return false;
			}
			return x1.Id == x2.Id;
		}
	}
}
