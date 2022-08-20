using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public class RequestService : IRequestService
	{
		public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account)
		{
			var result = new MakePaymentResult() { Success = true };

			switch (request.PaymentScheme)
			{
				case PaymentScheme.Bacs:
					if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
					{
						result.Success = false;
					}
					break;

				case PaymentScheme.FasterPayments:
					if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
					{
						result.Success = false;
					}
					else if (account.Balance < request.Amount)
					{
						result.Success = false;
					}
					break;

				case PaymentScheme.Chaps:
					if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
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
