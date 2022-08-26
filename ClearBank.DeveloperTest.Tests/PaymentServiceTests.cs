using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using Moq;
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
			var requestServiceMock = new Mock<IRequestService>();
			requestServiceMock.Setup(x => x.ValidatRequestWithAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(new MakePaymentResult() { Success = true }).Verifiable();

			var accountDataStoreMock = new Mock<IAccountDataStore>();
			accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account() { AccountNumber = "0123456789", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100m, Status = AccountStatus.Live }).Verifiable();
			accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable();

			var paymentService = new PaymentService(requestServiceMock.Object);
			var paymentRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "9876543210", DebtorAccountNumber = "0123456789", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };

			// Act
			MakePaymentResult result = paymentService.MakePayment(paymentRequest, accountDataStoreMock.Object);

			// Assert
			Assert.NotNull(result);
			Assert.True(result.Success);

			requestServiceMock.Verify(x => x.ValidatRequestWithAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Once);
			accountDataStoreMock.Verify(x => x.GetAccount(It.IsAny<string>()), Times.Once);
			accountDataStoreMock.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Once);
		}
	}
}
