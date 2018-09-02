using PrismWithRealm.Common.Services;
using PrismWithRealm.Domains.Interventions.Entities;
using PrismWithRealm.Interfaces;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWithRealm.Domains.Interventions.Dal
{
	public class InterventionDal : DalBase<Intervention>, IInterventionDal
	{
		public InterventionDal(IRealmManager context) 
			: base(context)
		{
		}
	}

	public interface IInterventionDal 
		: IDalBase<Intervention>
	{

	}

	public interface IDalBase<T> 
		where T : RealmObject
	{
		IQueryable<T> GetQueryable();
		Task<bool> Insert(T item);
		Task<bool> Update(T item);
		Task<bool> Delete(long id);
	}


	public class DalBase<T> : IDalBase<T> where T : RealmObject
	{
		readonly IRealmManager realmManager;

		public DalBase(IRealmManager realmManager)
		{
			this.realmManager = realmManager;
		}
		
		public IQueryable<T> GetQueryable()
		{
			return realmManager.GetRealmInstance().All<T>();
		}

		public async Task<bool> Insert(T item)
		{
			var result = await MakeChange(r => r.Add(item));

			return result;
		}

		public async Task<bool> Update(T item)
		{
			var result = await MakeChange(r => r.Add(item, true));

			return result;
		}

		public async Task<bool> Delete(long id)
		{
			var result = false;
			var item = realmManager.GetRealmInstance().Find<T>(id);

			if (item == null)
				return result;

			result = await MakeChange(r => r.Remove(item));

			return result;
		}

		public async Task<bool> MakeChange(Action<Realm> action)
		{
			var result = false;

			await realmManager.GetRealmInstance().WriteAsync(action).ContinueWith(t =>
			{
				result = !t.IsFaulted && !t.IsCanceled && t.IsCompleted;
			});

			return result;
		}
	}
}
