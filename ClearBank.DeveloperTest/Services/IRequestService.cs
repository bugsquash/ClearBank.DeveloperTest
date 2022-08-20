using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public interface IRequestService
	{
		public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account);
	}
}
