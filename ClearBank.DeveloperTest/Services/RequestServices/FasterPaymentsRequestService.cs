using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.RequestServices
{
    public class FasterPaymentsRequestService : IRequestService
    {
        public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account)
        {
            var result = new MakePaymentResult() { Success = true };

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                result.Success = false;
            }
            else if (account.Balance < request.Amount)
            {
                result.Success = false;
            }

            return result;
        }
    }
}
