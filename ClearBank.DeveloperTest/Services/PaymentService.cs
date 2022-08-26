using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IRequestService _requestService;

		public PaymentService(IRequestService requestService)
		{
			_requestService = requestService;
		}

		public MakePaymentResult MakePayment(MakePaymentRequest request)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

			IAccountDataStore accountDataStore;
			Account debtorAccount;

			if (dataStoreType == "Backup")
			{
				accountDataStore = new BackupAccountDataStore();
				debtorAccount = accountDataStore.GetAccount(request.DebtorAccountNumber);
			}
			else
			{
				accountDataStore = new AccountDataStore();
				debtorAccount = accountDataStore.GetAccount(request.DebtorAccountNumber);
			}

			var result = new MakePaymentResult();

			if (debtorAccount != null)
			{
				result = _requestService.ValidatRequestWithAccount(request, debtorAccount);
			}

			if (result.Success)
			{
				debtorAccount.Balance -= request.Amount;
				accountDataStore.UpdateAccount(debtorAccount);
			}

			return result;
		}
	}
}
