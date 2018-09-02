using PrismWithRealm.Common.Services;
using Realms;
using System;

namespace PrismWithRealm.Domains.Interventions.Entities
{
    public class Intervention : RealmObject, IPrimaryKeyObject
	{
		[PrimaryKey]
		public long Id { get; set; }
		
		public string Libelle { get; set; }

		public DateTimeOffset DateIntervention { get; set; }
	}

	//public class Lieu : RealmObject
	//{
	//	[PrimaryKey]
	//	public long Id { get; set; }


	//}
}
