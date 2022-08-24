using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IAccountService _accountService;
		private readonly IRequestService _requestService;
		private readonly IDatabaseService _databaseService;

		public PaymentService(IAccountService accountService, IRequestService requestService, IDatabaseService databaseService)
		{
			_accountService = accountService;
			_requestService = requestService;
			_databaseService = databaseService;
		}

		public MakePaymentResult MakePayment(MakePaymentRequest request)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];
			var account = _accountService.GetAccount(request.DebtorAccountNumber, dataStoreType);

			var result = new MakePaymentResult();

			if (account != null)
			{
				result = _requestService.ValidatRequestWithAccount(request, account);
			}

			if (result.Success)
			{
				account.Balance -= request.Amount;
				_databaseService.UpdateDatabase(account, dataStoreType);
			}

			return result;
		}
	}
}
