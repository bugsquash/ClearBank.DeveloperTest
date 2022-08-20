using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public class AccountService : IAccountService
	{
		public Account GetAccount(string accountNumber, string dataStoreType)
		{
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

		public Account DeductRequestFromAccount(MakePaymentRequest request, Account account)
		{
			account.Balance -= request.Amount;
			return account;
		}
	}
}
