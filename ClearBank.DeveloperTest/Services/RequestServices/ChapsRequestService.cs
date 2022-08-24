using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.RequestServices
{
    public class ChapsRequestService : IRequestService
    {
        public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account)
        {
            var result = new MakePaymentResult() { Success = true };

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                result.Success = false;
            }
            else if (account.Status != AccountStatus.Live)
            {
                result.Success = false;
            }

            return result;
        }
    }
}
