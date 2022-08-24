using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.RequestServices
{
    public class BacsRequestService : IRequestService
    {
        public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account)
        {
            var result = new MakePaymentResult() { Success = true };

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                result.Success = false;
            }

            return result;
        }
    }
}
