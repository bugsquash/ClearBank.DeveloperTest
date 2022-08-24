using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Interfaces
{
	public interface IAccountService
	{
		public Account GetAccount(string accountNumber, string dataStoreType);
	}
}
