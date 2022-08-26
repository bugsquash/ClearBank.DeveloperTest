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
			requestServiceMock.Setup(x => x.ValidatRequestWithAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(new MakePaymentResult() { Success = true });

			var paymentService = new PaymentService(requestServiceMock.Object);
			var paymentRequest = new MakePaymentRequest() { Amount = 10m, CreditorAccountNumber = "9876543210", DebtorAccountNumber = "0123456789", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };

			// Act
			MakePaymentResult result = paymentService.MakePayment(paymentRequest);

			// Assert
			Assert.NotNull(result);
			Assert.True(result.Success);
		}
	}
}
