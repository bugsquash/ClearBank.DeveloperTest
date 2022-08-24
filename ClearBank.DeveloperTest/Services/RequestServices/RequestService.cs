﻿using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.RequestServices
{
	public class RequestService : IRequestService
	{
		public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account)
		{
			var result = new MakePaymentResult() { Success = true };

			if (request.PaymentScheme.ToString() != account.AllowedPaymentSchemes.ToString())
			{
				result.Success = false;
			}
			else
			{
				switch (request.PaymentScheme)
				{
					case PaymentScheme.FasterPayments:
						if (account.Balance < request.Amount)
						{
							result.Success = false;
						}
						break;

					case PaymentScheme.Chaps:
						if (account.Status != AccountStatus.Live)
						{
							result.Success = false;
						}
						break;
				}
			}

			return result;
		}
	}
}
