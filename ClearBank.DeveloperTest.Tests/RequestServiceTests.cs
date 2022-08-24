using ClearBank.DeveloperTest.Services.RequestServices;
using ClearBank.DeveloperTest.Types;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
	public class RequestServiceTests
	{
		[Fact]
		public void ValidateRequestWithAccount_GivenMatchingSchemes_ReturnsSuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Bacs };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 100m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.True(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenUnmatchingSchemes_ReturnsUnsuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 100m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.False(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenNonLiveAccountStatusWithChapsScheme_ReturnsUnsuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Chaps };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 100m, Status = AccountStatus.Disabled };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.False(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenLiveAccountStatusWithChapsScheme_ReturnsSuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Chaps };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 100m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.True(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenNonLiveAccountStatusWithMatchingNonChapsScheme_ReturnsSuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Bacs };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 100m, Status = AccountStatus.Disabled };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.True(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenFasterPaymentsSchemeAndBalanceLessThanRequestAmount_ReturnsUnsuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 100m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 10m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.False(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenFasterPaymentsSchemeAndBalanceMoreThanRequestAmount_ReturnsSuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.True(result.Success);
		}

		[Fact]
		public void ValidateRequestWithAccount_GivenNonFasterPaymentsSchemeAndBalanceLessThanRequestAmount_ReturnsSuccessfulResult()
		{
			// Arrange
			var requestService = new RequestService();

			var testRequest = new MakePaymentRequest() { Amount = 100m, CreditorAccountNumber = "0123456789", DebtorAccountNumber = "9876543210", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Chaps };
			var testAccount = new Account() { AccountNumber = "9876543210", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 10m, Status = AccountStatus.Live };

			// Act
			MakePaymentResult result = requestService.ValidatRequestWithAccount(testRequest, testAccount);

			// Assert
			Assert.True(result.Success);
		}
	}
}
