﻿using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
	public interface IAccountService
	{
		public Account GetAccount(string accountNumber, string dataStoreType);
		public Account DeductRequestFromAccount(MakePaymentRequest request, Account account);
	}
}