using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Processing;
using Ezzygate.Infrastructure.Processing.Models;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.WebApi.Controllers.Merchants;
using Ezzygate.WebApi.Dtos.Merchants.CreditCard;

namespace Ezzygate.WebApi.Tests.Controllers.Merchants;

[TestFixture]
public class CreditcardControllerTests
{
    private Mock<ILogger<CreditcardController>> _loggerMock;
    private Mock<ILegacyPaymentService> _legacyPaymentServiceMock;
    private Mock<IPaymentPageSettingsRepository> _paymentPageSettingsRepositoryMock;
    private Mock<ICurrencyRepository> _currencyRepositoryMock;
    private CreditcardController _controller;
    private Merchant? _stubMerchant;
    private Currency _stubCurrency;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<CreditcardController>>();
        _legacyPaymentServiceMock = new Mock<ILegacyPaymentService>();
        _paymentPageSettingsRepositoryMock = new Mock<IPaymentPageSettingsRepository>();
        _currencyRepositoryMock = new Mock<ICurrencyRepository>();

        _stubMerchant = new Merchant { Id = 1, CustomerNumber = "TEST123" };
        _stubCurrency = new Currency(
            id: 1,
            isoCode: "USD",
            isoNumber: "840",
            name: "US Dollar",
            symbol: "$",
            baseRate: 1.0m,
            exchangeFee: 0m,
            rateRequestDate: DateTime.UtcNow.Date,
            rateValueDate: DateTime.UtcNow.Date,
            maxTransactionAmount: 1000000m,
            isSymbolBeforeAmount: true
        );

        _currencyRepositoryMock
            .Setup(x => x.Get(It.IsAny<int>()))
            .Returns(_stubCurrency);

        _paymentPageSettingsRepositoryMock
            .Setup(x => x.IsTipAllowedAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _controller = CreateController(_stubMerchant);
    }

    [Test]
    public async Task Process_WithValidRequest_ReturnsPendingResponse()
    {
        var request = CreateValidProcessRequest();
        var legacyResult = CreatePendingLegacyResult();

        _legacyPaymentServiceMock
            .Setup(x => x.ProcessAsync(It.IsAny<LegacyProcessRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyResult);

        var response = await _controller.Process(request, CancellationToken.None);
        var responseData = response.Data as CreditcardProcessResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Pending"));
            Assert.That(responseData!.TransactionId, Is.EqualTo(legacyResult.TransactionId));
            Assert.That(responseData.CreditCard?.Number, Is.EqualTo(legacyResult.Last4));
            Assert.That(responseData.CreditCard?.Type, Is.EqualTo(legacyResult.CardType));
            Assert.That(responseData.CreditCard?.Cvv, Is.EqualTo("..."));
            Assert.That(responseData.Amount, Is.EqualTo(decimal.Parse(legacyResult.Amount)));
            Assert.That(responseData.CurrencyIso, Is.EqualTo(_stubCurrency.IsoCode));
            Assert.That(responseData.ReplyCode, Is.EqualTo(legacyResult.ReplyCode));
            Assert.That(responseData.ReplyDescription, Is.EqualTo(legacyResult.ReplyDescription));
            Assert.That(responseData.SavedCardId, Is.EqualTo(legacyResult.SavedCardId));
            Assert.That(responseData.Descriptor, Is.EqualTo(legacyResult.Descriptor));
            Assert.That(responseData.AuthenticationRedirectUrl, Is.EqualTo(legacyResult.AuthenticationRedirectUrl));
            Assert.That(responseData.RecurringSeriesId, Is.EqualTo(legacyResult.RecurringSeries));
        });
    }

    [Test]
    public async Task Process_WithNegativeTipAmount_ReturnsInvalidRequestResponse()
    {
        var request = CreateValidProcessRequest();
        request.TipAmount = -10;

        var response = await _controller.Process(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("Tip amount cannot be negative"));
    }

    [Test]
    public async Task Process_WithTipAmountExceedingTransactionAmount_ReturnsInvalidRequestResponse()
    {
        var request = CreateValidProcessRequest();
        request.TipAmount = request.Amount + 1;

        var response = await _controller.Process(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("tip Amount should be lesser than the actual amount"));
    }

    [Test]
    public async Task Process_WhenSavingCardWithExistingSavedCardId_ReturnsInvalidRequestResponse()
    {
        var request = CreateValidProcessRequest();
        request.SaveCard = true;
        request.SavedCardId = "saved_123";

        var response = await _controller.Process(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("Cannot save card when trying to use a saved card"));
    }

    [Test]
    public async Task Process_WhenAuthorizingWithSavedCard_ReturnsInvalidRequestResponse()
    {
        var request = CreateValidProcessRequest();
        request.SavedCardId = "saved_123";
        request.AuthorizeOnly = true;

        var response = await _controller.Process(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("Cannot authorize with a saved card"));
    }

    [Test]
    public async Task Process_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var controller = CreateController(null);
        var request = CreateValidProcessRequest();

        _legacyPaymentServiceMock
            .Setup(x => x.ProcessAsync(It.IsAny<LegacyProcessRequest>(), It.IsAny<CancellationToken>()));

        var response = await controller.Process(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public void Process_WhenLegacyServiceThrows_PropagatesException()
    {
        var request = CreateValidProcessRequest();

        _legacyPaymentServiceMock
            .Setup(x => x.ProcessAsync(It.IsAny<LegacyProcessRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Legacy service error"));

        var ex = Assert.ThrowsAsync<Exception>(() =>
            _controller.Process(request, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Legacy service error"));
    }

    [Test]
    public async Task ProcessEncrypted_WithValidRequest_ReturnsPendingResponse()
    {
        var request = CreateValidProcessRequest();
        var encryptedData = AesEncryption.EncryptStringAes(JsonSerializer.Serialize(request));
        var encryptedRequest = new CreditcardProcessEncryptedRequestDto { Data = encryptedData };
        var legacyResult = CreatePendingLegacyResult();

        _legacyPaymentServiceMock
            .Setup(x => x.ProcessAsync(It.IsAny<LegacyProcessRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyResult);

        var response = await _controller.ProcessEncrypted(encryptedRequest, CancellationToken.None);
        var responseData = response.Data as CreditcardProcessResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Pending"));
            Assert.That(responseData!.TransactionId, Is.EqualTo(legacyResult.TransactionId));
            Assert.That(responseData.CurrencyIso, Is.EqualTo(_stubCurrency.IsoCode));
            Assert.That(responseData.ReplyCode, Is.EqualTo(legacyResult.ReplyCode));
            Assert.That(responseData.ReplyDescription, Is.EqualTo(legacyResult.ReplyDescription));
        });
    }

    [Test]
    public async Task ProcessEncrypted_WithInvalidEncryptedData_ReturnsInvalidRequestResponse()
    {
        var encryptedRequest = new CreditcardProcessEncryptedRequestDto { Data = "invalid_encrypted_data" };

        var response = await _controller.ProcessEncrypted(encryptedRequest, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("Failed to decrypt request data"));
    }

    [Test]
    public async Task ProcessEncrypted_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var request = CreateValidProcessRequest();
        var encryptedData = AesEncryption.EncryptStringAes(JsonSerializer.Serialize(request));
        var encryptedRequest = new CreditcardProcessEncryptedRequestDto
        {
            Data = encryptedData
        };
        var controller = CreateController(null);

        var response = await controller.ProcessEncrypted(encryptedRequest, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public async Task ProcessEncrypted_WithNegativeTipAmount_ReturnsInvalidRequestResponse()
    {
        var request = CreateValidProcessRequest();
        request.TipAmount = -10;
        var encryptedData = AesEncryption.EncryptStringAes(JsonSerializer.Serialize(request));
        var encryptedRequest = new CreditcardProcessEncryptedRequestDto
        {
            Data = encryptedData
        };

        var response = await _controller.ProcessEncrypted(encryptedRequest, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("InvalidRequest"));
        Assert.That(response.Data, Is.EqualTo("Tip amount cannot be negative"));
    }

    [Test]
    public void ProcessEncrypted_WhenLegacyServiceThrows_PropagatesException()
    {
        var request = CreateValidProcessRequest();
        var encryptedData = AesEncryption.EncryptStringAes(JsonSerializer.Serialize(request));
        var encryptedRequest = new CreditcardProcessEncryptedRequestDto
        {
            Data = encryptedData
        };

        _legacyPaymentServiceMock
            .Setup(x => x.ProcessAsync(It.IsAny<LegacyProcessRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Legacy service error"));

        var ex = Assert.ThrowsAsync<Exception>(() =>
            _controller.ProcessEncrypted(encryptedRequest, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Legacy service error"));
    }

    [Test]
    public async Task Refund_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidRefundRequest();
        var legacyResult = CreateSuccessLegacyResult();

        _legacyPaymentServiceMock
            .Setup(x => x.RefundAsync(It.IsAny<LegacyRefundRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyResult);

        var response = await _controller.Refund(request, CancellationToken.None);
        var responseData = response.Data as RefundResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Approved"));
            Assert.That(responseData!.TransactionId, Is.EqualTo(int.Parse(legacyResult.TransactionId)));
            Assert.That(responseData.Amount, Is.EqualTo(decimal.Parse(legacyResult.Amount)));
            Assert.That(responseData.CurrencyIso, Is.EqualTo(_stubCurrency.IsoCode));
            Assert.That(responseData.ReplyCode, Is.EqualTo(legacyResult.ReplyCode));
            Assert.That(responseData.ReplyDescription, Is.EqualTo(legacyResult.ReplyDescription));
        });
    }

    [Test]
    public async Task Refund_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var request = CreateValidRefundRequest();
        var controller = CreateController(null);

        var response = await controller.Refund(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public void Refund_WhenLegacyServiceThrows_PropagatesException()
    {
        var request = CreateValidRefundRequest();

        _legacyPaymentServiceMock
            .Setup(x => x.RefundAsync(It.IsAny<LegacyRefundRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Legacy service error"));

        var ex = Assert.ThrowsAsync<Exception>(() =>
            _controller.Refund(request, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Legacy service error"));
    }

    [Test]
    public async Task Void_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidVoidRequest();
        var legacyResult = CreateSuccessLegacyResult();

        _legacyPaymentServiceMock
            .Setup(x => x.VoidAsync(It.IsAny<LegacyVoidRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyResult);

        var response = await _controller.Void(request, CancellationToken.None);
        var responseData = response.Data as VoidResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Approved"));
            Assert.That(responseData!.TransactionId, Is.EqualTo(legacyResult.TransactionId));
            Assert.That(responseData.ReplyCode, Is.EqualTo(legacyResult.ReplyCode));
            Assert.That(responseData.ReplyDescription, Is.EqualTo(legacyResult.ReplyDescription));
        });
    }

    [Test]
    public async Task Void_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var request = CreateValidVoidRequest();
        var controller = CreateController(null);

        var response = await controller.Void(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public void Void_WhenLegacyServiceThrows_PropagatesException()
    {
        var request = CreateValidVoidRequest();

        _legacyPaymentServiceMock
            .Setup(x => x.VoidAsync(It.IsAny<LegacyVoidRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Legacy service error"));

        var ex = Assert.ThrowsAsync<Exception>(() =>
            _controller.Void(request, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Legacy service error"));
    }

    [Test]
    public async Task Capture_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidCaptureRequest();
        var legacyResult = CreateSuccessLegacyResult();

        _legacyPaymentServiceMock
            .Setup(x => x.CaptureAsync(It.IsAny<LegacyCaptureRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(legacyResult);

        var response = await _controller.Capture(request, CancellationToken.None);
        var responseData = response.Data as CaptureResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Approved"));
            Assert.That(responseData!.TransactionId, Is.EqualTo(legacyResult.TransactionId));
            Assert.That(responseData.CurrencyIso, Is.EqualTo(_stubCurrency.IsoCode));
            Assert.That(responseData.ReplyCode, Is.EqualTo(legacyResult.ReplyCode));
            Assert.That(responseData.ReplyDescription, Is.EqualTo(legacyResult.ReplyDescription));
        });
    }

    [Test]
    public async Task Capture_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var request = CreateValidCaptureRequest();
        var controller = CreateController(null);

        var response = await controller.Capture(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public void Capture_WhenLegacyServiceThrows_PropagatesException()
    {
        var request = CreateValidCaptureRequest();

        _legacyPaymentServiceMock
            .Setup(x => x.CaptureAsync(It.IsAny<LegacyCaptureRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Legacy service error"));

        var ex = Assert.ThrowsAsync<Exception>(() =>
            _controller.Capture(request, CancellationToken.None));

        Assert.That(ex.Message, Is.EqualTo("Legacy service error"));
    }

    private CreditcardController CreateController(Merchant? merchant)
    {
        var controller = new CreditcardController(
            _loggerMock.Object,
            _legacyPaymentServiceMock.Object,
            _paymentPageSettingsRepositoryMock.Object,
            _currencyRepositoryMock.Object);

        var httpContext = new DefaultHttpContext();
        if (merchant != null)
        {
            httpContext.SetMerchant(merchant);
        }
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        return controller;
    }

    private static CreditcardProcessRequestDto CreateValidProcessRequest()
    {
        return new CreditcardProcessRequestDto
        {
            CreditCard = new CreditCardDto
            {
                Number = "4111111111111111",
                ExpirationMonth = 12,
                ExpirationYear = 2025,
                HolderName = "John Doe",
                Cvv = "123",
                Type = "Visa",
                BillingAddress = new AddressDto
                {
                    AddressLine1 = "123 Test St",
                    City = "Test City",
                    CountryIso = "US",
                    PostalCode = "12345",
                    StateIso = "CA"
                }
            },
            Customer = new CustomerDto
            {
                Email = "test@example.com",
                FullName = "John Doe",
                PhoneNumber = "+1234567890",
                PersonalIdNumber = "12345"
            },
            Amount = 100.50m,
            CurrencyIso = "USD",
            Installments = 1,
            PostRedirectUrl = "https://example.com/callback",
            SaveCard = false,
            AuthorizeOnly = false,
            TipAmount = 0
        };
    }

    private static LegacyPaymentResult CreatePendingLegacyResult()
    {
        return new LegacyPaymentResult
        {
            ReplyCode = "553",
            ReplyDescription = "3D Secure Redirection is needed",
            TransactionId = "12345",
            Currency = "840",
            Amount = "100.50",
            Last4 = "1111",
            CardType = "Visa",
            SavedCardId = null,
            Descriptor = "Test Merchant",
            AuthenticationRedirectUrl = "https://3dsecure.test/auth",
            RecurringSeries = null,
            Order = "ORDER123",
            Comment = "Test transaction"
        };
    }

    private static LegacyPaymentResult CreateSuccessLegacyResult()
    {
        return new LegacyPaymentResult
        {
            ReplyCode = "000",
            ReplyDescription = "Transaction approved",
            TransactionId = "67890",
            Currency = "840",
            Amount = "50.00",
            Last4 = "1111",
            CardType = "Visa",
            SavedCardId = null,
            Descriptor = "Test Merchant",
            AuthenticationRedirectUrl = null,
            RecurringSeries = null,
            Order = "ORDER456",
            Comment = "Test transaction"
        };
    }

    private static RefundRequestDto CreateValidRefundRequest()
    {
        return new RefundRequestDto
        {
            Amount = 50.00m,
            CurrencyIso = "USD",
            TransactionId = 12345,
            Comment = "Refund test"
        };
    }

    private static VoidRequestDto CreateValidVoidRequest()
    {
        return new VoidRequestDto
        {
            CurrencyIso = "USD",
            TransactionId = "12345"
        };
    }

    private static CaptureRequestDto CreateValidCaptureRequest()
    {
        return new CaptureRequestDto
        {
            CurrencyIso = "USD",
            TransactionId = "12345"
        };
    }
}