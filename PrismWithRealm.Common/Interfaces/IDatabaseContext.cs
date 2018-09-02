using Realms;

namespace PrismWithRealm.Interfaces
{
    public interface IDatabaseContext
    {
		Realm Database { get; }
	}
}
