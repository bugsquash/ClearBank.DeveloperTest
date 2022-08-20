using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
	public class AccountDataStore
	{
		private readonly Account _testDebtorAccount = new Account() { AccountNumber = "0123456789", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100m, Status = AccountStatus.Live };

		public Account GetAccount(string accountNumber)
		{
			// Access database to retrieve account, code removed for brevity
			// Added hard-coded Account object for testing
			if (accountNumber == "0123456789")
			{
				return _testDebtorAccount;
			}
			else
			{
				return new Account();
			}
		}

		// Changed to return boolean value to give indication of success or failure of I/O method
		public void UpdateAccount(Account account)
		{
			// Update account in database, code removed for brevity
		}
	}
}
