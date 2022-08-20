using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class DatabaseService
	{
		public void UpdateDatabase(Account account)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

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
