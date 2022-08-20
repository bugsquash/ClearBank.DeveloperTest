﻿using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
	public class PaymentService : IPaymentService
	{
		public MakePaymentResult MakePayment(MakePaymentRequest request)
		{
			var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

			var accountService = new AccountService();
			Account account = accountService.GetAccount(request.DebtorAccountNumber, dataStoreType);

			MakePaymentResult result = new MakePaymentResult();

			if (account != null)
			{
				var requestService = new RequestService();
				result = requestService.ValidatRequestWithAccount(request, account);
			}

			if (result.Success)
			{
				Account modifiedAccount = accountService.DeductRequestFromAccount(request, account);

				var databaseService = new DatabaseService();
				databaseService.UpdateDatabase(modifiedAccount, dataStoreType);
			}

			return result;
		}
	}
}
