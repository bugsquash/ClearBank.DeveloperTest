using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class PaymentService : IPaymentService
	{
		public MakePaymentResult MakePayment(MakePaymentRequest request)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

			//Account account = null;

			//if (dataStoreType == "Backup")
			//{
			//	var accountDataStore = new BackupAccountDataStore();
			//	account = accountDataStore.GetAccount(request.DebtorAccountNumber);
			//}
			//else
			//{
			//	var accountDataStore = new AccountDataStore();
			//	account = accountDataStore.GetAccount(request.DebtorAccountNumber);
			//}

			AccountService accountService = new AccountService();
			var account = accountService.GetAccount(request.DebtorAccountNumber, dataStoreType);

			//var result = new MakePaymentResult();

			//switch (request.PaymentScheme)
			//{
			//	case PaymentScheme.Bacs:
			//		if (account == null)
			//		{
			//			result.Success = false;
			//		}
			//		else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
			//		{
			//			result.Success = false;
			//		}
			//		break;

			//	case PaymentScheme.FasterPayments:
			//		if (account == null)
			//		{
			//			result.Success = false;
			//		}
			//		else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
			//		{
			//			result.Success = false;
			//		}
			//		else if (account.Balance < request.Amount)
			//		{
			//			result.Success = false;
			//		}
			//		break;

			//	case PaymentScheme.Chaps:
			//		if (account == null)
			//		{
			//			result.Success = false;
			//		}
			//		else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
			//		{
			//			result.Success = false;
			//		}
			//		else if (account.Status != AccountStatus.Live)
			//		{
			//			result.Success = false;
			//		}
			//		break;
			//}

			ValidateAccountService validateAccountService = new ValidateAccountService();
			var result = validateAccountService.ValidatAccountWithRequest(account, request);

			if (result.Success)
			{
				//account.Balance -= request.Amount;

				var modifiedAccount = DeductRequestFromAccount(request, account);

				//if (dataStoreType == "Backup")
				//{
				//	var accountDataStore = new BackupAccountDataStore();
				//	accountDataStore.UpdateAccount(modifiedAccount);
				//}
				//else
				//{
				//	var accountDataStore = new AccountDataStore();
				//	accountDataStore.UpdateAccount(modifiedAccount);
				//}

				DatabaseService databaseService = new DatabaseService();
				databaseService.UpdateDatabase(modifiedAccount, dataStoreType);
			}

			return result;
		}

		public Account DeductRequestFromAccount(MakePaymentRequest request, Account account)
		{
			account.Balance -= request.Amount;
			return account;
		}
	}
}
