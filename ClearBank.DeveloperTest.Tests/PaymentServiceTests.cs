using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
	public class PaymentServiceTests
	{
		[Fact]
		public void MakePayment_GivenValidParameter_ReturnsSuccessfulResult()
		{
			// Arrange
			var debtorAccount = new Account() { AccountNumber = "0123456789", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100m, Status = AccountStatus.Live };
			var paymentRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "9876543210", DebtorAccountNumber = "0123456789", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };

			var paymentService = new PaymentService();

			// Act
			MakePaymentResult result = paymentService.MakePayment(paymentRequest);

			// Assert
			Assert.NotNull(result);
			Assert.True(result.Success);
		}
	}
}
