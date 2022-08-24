using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces
{
	public interface IRequestService
	{
		public MakePaymentResult ValidatRequestWithAccount(MakePaymentRequest request, Account account);
	}
}
