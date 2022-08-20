using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public class DatabaseService
	{
		public void UpdateDatabase(Account account, string dataStoreType)
		{
			if (dataStoreType == "Backup")
			{
				var accountDataStore = new BackupAccountDataStore();
				accountDataStore.UpdateAccount(account);
			}
			else
			{
				var accountDataStore = new AccountDataStore();
				accountDataStore.UpdateAccount(account);
			}
		}
	}
}
