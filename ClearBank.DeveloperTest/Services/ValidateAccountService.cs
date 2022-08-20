﻿using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public class ValidateAccountService
	{
		public MakePaymentResult ValidatAccountWithRequest(Account account, MakePaymentRequest request)
		{
			var result = new MakePaymentResult();

			switch (request.PaymentScheme)
			{
				case PaymentScheme.Bacs:
					if (account == null)
					{
						result.Success = false;
					}
					else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
					{
						result.Success = false;
					}
					break;

				case PaymentScheme.FasterPayments:
					if (account == null)
					{
						result.Success = false;
					}
					else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
					{
						result.Success = false;
					}
					else if (account.Balance < request.Amount)
					{
						result.Success = false;
					}
					break;

				case PaymentScheme.Chaps:
					if (account == null)
					{
						result.Success = false;
					}
					else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
					{
						result.Success = false;
					}
					else if (account.Status != AccountStatus.Live)
					{
						result.Success = false;
					}
					break;
			}

			return result;
		}
	}
}
