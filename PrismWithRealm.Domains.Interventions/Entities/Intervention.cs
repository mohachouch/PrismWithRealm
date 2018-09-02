using PrismWithRealm.Common.Services;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismWithRealm.Domains.Interventions.Entities
{
    public class Intervention : RealmObject, IPrimaryKeyObject
	{
		[PrimaryKey]
		public long Id { get; set; }
		
		public string Libelle { get; set; }

		public DateTimeOffset DateIntervention { get; set; }

		public Lieu Lieu { get; set; }

	}

	public class Lieu : RealmObject, IPrimaryKeyObject
	{
		[PrimaryKey]
		public long Id { get; set; }

		public string Libelle { get; set; }
		
		[Backlink(nameof(Intervention.Lieu))]
		public IQueryable<Intervention> Interventions { get; }
	}
}
