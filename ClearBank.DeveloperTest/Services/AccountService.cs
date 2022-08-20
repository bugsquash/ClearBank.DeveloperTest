using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class AccountService
	{
		public Account GetAccount(string accountNumber)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

			Account account;

			if (dataStoreType == "Backup")
			{
				var accountDataStore = new BackupAccountDataStore();
				account = accountDataStore.GetAccount(accountNumber);
			}
			else
			{
				var accountDataStore = new AccountDataStore();
				account = accountDataStore.GetAccount(accountNumber);
			}

			return account;
		}
	}
}
