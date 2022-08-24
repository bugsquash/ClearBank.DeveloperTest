using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces
{
    public interface IDatabaseService
    {
        public void UpdateDatabase(Account account, string dataStoreType);
    }
}
