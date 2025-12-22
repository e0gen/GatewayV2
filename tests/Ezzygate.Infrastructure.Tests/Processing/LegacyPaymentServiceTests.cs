using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using Ezzygate.Application.Configuration;
using Ezzygate.Infrastructure.Processing;
using Ezzygate.Infrastructure.Processing.Models;

namespace Ezzygate.Infrastructure.Tests.Processing;

[TestFixture]
public class LegacyPaymentServiceTests
{
    private Mock<ILogger<LegacyPaymentService>> _loggerMock;
    private Mock<IDomainConfiguration> _domainConfigurationMock;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<LegacyPaymentService>>();
        _domainConfigurationMock = new Mock<IDomainConfiguration>();
        _domainConfigurationMock.Setup(x => x.ProcessUrl).Returns("https://process.example.com/member/");
    }
    
    private LegacyPaymentService CreateService(HttpClient httpClient)
    {
        return new LegacyPaymentService(httpClient, _domainConfigurationMock.Object, _loggerMock.Object);
    }
    
    private HttpClient CreateHttpClient(MockHttpMessageHandler handler)
    {
        return new HttpClient(handler) { BaseAddress = new Uri("https://process.example.com/") };
    }

    [Test]
    public async Task ProcessAsync_WithValidRequest_ReturnsPendingResponseWith3DSecure()
    {
        var request = CreateValidProcessRequest();
        var expectedResponse = CreatePending3DSecureResponse();

        var handler = new MockHttpMessageHandler(expectedResponse);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("553"));
            Assert.That(result.ReplyDescription, Is.EqualTo("3D Secure Redirection is needed"));
            Assert.That(result.TransactionId, Is.EqualTo("13696"));
            Assert.That(result.Amount, Is.EqualTo("55.30"));
            Assert.That(result.Currency, Is.EqualTo("1"));
            Assert.That(result.Last4, Is.EqualTo("2151"));
            Assert.That(result.CardType, Is.EqualTo("Mastercard"));
            Assert.That(result.Descriptor, Is.EqualTo("Do not remove"));
            Assert.That(result.ConfirmationNumber, Is.EqualTo("cn-a2e11d7e-60a4-41ce-bd2e-eaaf0c0c5710"));
            Assert.That(result.AuthenticationRedirectUrl, Is.Not.Null);
            Assert.That(result.AuthenticationRedirectUrl, Contains.Substring("remoteCharge_ccDebitGeneric3DSAuth.asp"));
            Assert.That(result.Signature, Is.Not.Null);
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_BuildsCorrectUrl_WithAllParameters()
    {
        var request = CreateValidProcessRequest();
        var expectedResponse = CreatePending3DSecureResponse();
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(expectedResponse, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("CompanyNum=2020202"));
            Assert.That(capturedUrl, Contains.Substring("CVV2=111"));
            Assert.That(capturedUrl, Contains.Substring("CardNum=5200000000002151"));
            Assert.That(capturedUrl, Contains.Substring("ExpMonth=12"));
            Assert.That(capturedUrl, Contains.Substring("ExpYear=2027"));
            Assert.That(capturedUrl, Contains.Substring("TrmCode=7"));
            Assert.That(capturedUrl, Contains.Substring("Member=James Bobby"));
            Assert.That(capturedUrl, Contains.Substring("requestSource=53"));
            Assert.That(capturedUrl, Contains.Substring("Amount=55.3"));
            Assert.That(capturedUrl, Contains.Substring("Currency=USD"));
            Assert.That(capturedUrl, Contains.Substring("Payments=1"));
            Assert.That(capturedUrl, Contains.Substring("TransType=0"));
            Assert.That(capturedUrl, Contains.Substring("TypeCredit=1"));
            Assert.That(capturedUrl, Contains.Substring("PersonalNum=20004368"));
            Assert.That(capturedUrl, Contains.Substring("PhoneNumber=393923229295"));
            Assert.That(capturedUrl, Contains.Substring("StoreCc=0"));
            Assert.That(capturedUrl, Contains.Substring("Email=support@ezzygate.com"));
            Assert.That(capturedUrl, Contains.Substring("BillingAddress1=Via Carlo D'Adda, 13"));
            Assert.That(capturedUrl, Contains.Substring("BillingAddress2=Apt 2"));
            Assert.That(capturedUrl, Contains.Substring("BillingCity=Milan"));
            Assert.That(capturedUrl, Contains.Substring("BillingZipCode=20143"));
            Assert.That(capturedUrl, Contains.Substring("BillingState=IT"));
            Assert.That(capturedUrl, Contains.Substring("BillingCountry=IT"));
            Assert.That(capturedUrl, Contains.Substring("RetURL=http://localhost:4201/payment-callback"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithTipAmount_IncludesTipAmountInUrl()
    {
        var request = CreateValidProcessRequest();
        request.TipAmount = 10.50m;
        var expectedResponse = CreatePending3DSecureResponse();
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(expectedResponse, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.ProcessAsync(request, CancellationToken.None);

        Assert.That(capturedUrl, Contains.Substring("TipAmount=10.5"));
        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithZeroTipAmount_DoesNotIncludeTipAmountInUrl()
    {
        var request = CreateValidProcessRequest();
        request.TipAmount = 0;
        var expectedResponse = CreatePending3DSecureResponse();
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(expectedResponse, url => capturedUrl = url);
        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://process.example.com/") };
        var service = CreateService(httpClient);

        await service.ProcessAsync(request, CancellationToken.None);

        Assert.That(capturedUrl, Does.Not.Contain("TipAmount="));
        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithNullOptionalFields_HandlesGracefully()
    {
        var request = CreateValidProcessRequest();
        request.CreditCard!.BillingAddress!.AddressLine2 = null;
        request.Customer!.DateOfBirth = null;
        request.Order = null;
        request.Comment = null;
        request.OrderDescription = null;
        request.SavedCardId = null;
        var expectedResponse = CreatePending3DSecureResponse();

        var handler = new MockHttpMessageHandler(expectedResponse);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.ProcessAsync(request, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithMobileApiComment_IncludesMobileApiParameters()
    {
        var request = CreateValidProcessRequest();
        request.Comment = "fcm_token_12345";
        request.CardPresent = true;
        var expectedResponse = CreatePending3DSecureResponse();
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(expectedResponse, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("MobileApiRequest=true"));
            Assert.That(capturedUrl, Contains.Substring("MobileApiCardPresent=true"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithMobileApiCommentButCardNotPresent_OnlyIncludesMobileApiRequest()
    {
        var request = CreateValidProcessRequest();
        request.Comment = "fcm_token_12345";
        request.CardPresent = false;
        var expectedResponse = CreatePending3DSecureResponse();
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(expectedResponse, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("MobileApiRequest=true"));
            Assert.That(capturedUrl, Does.Not.Contain("MobileApiCardPresent="));
        });

        httpClient.Dispose();
    }

    [Test]
    public void ProcessAsync_WhenHttpClientThrows_PropagatesException()
    {
        var request = CreateValidProcessRequest();
        var handler = new MockHttpMessageHandler("", shouldThrow: true);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var ex = Assert.ThrowsAsync<HttpRequestException>(() =>
            service.ProcessAsync(request, CancellationToken.None));

        Assert.That(ex, Is.Not.Null);
        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_ParsesResponseCorrectly_WithAllFields()
    {
        var request = CreateValidProcessRequest();
        var response = "TransType=0&Reply=000&TransID=12345&Date=22/12/2025 14:57:10&Order=ORDER123&Amount=100.50&Payments=1&Currency=840&ConfirmationNum=cn-test-123&Comment=Test comment&ReplyDesc=Transaction approved&CCType=Visa&Descriptor=Test Merchant&RecurringSeries=RS123&Last4=1111&ccStorageID=CC123&Source=WEBAPI&WalletID=WALLET123&signature=test-signature";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("000"));
            Assert.That(result.ReplyDescription, Is.EqualTo("Transaction approved"));
            Assert.That(result.TransactionId, Is.EqualTo("12345"));
            Assert.That(result.Date, Is.EqualTo("22/12/2025 14:57:10"));
            Assert.That(result.Order, Is.EqualTo("ORDER123"));
            Assert.That(result.Amount, Is.EqualTo("100.50"));
            Assert.That(result.Payments, Is.EqualTo("1"));
            Assert.That(result.Currency, Is.EqualTo("840"));
            Assert.That(result.ConfirmationNumber, Is.EqualTo("cn-test-123"));
            Assert.That(result.Comment, Is.EqualTo("Test comment"));
            Assert.That(result.CardType, Is.EqualTo("Visa"));
            Assert.That(result.Descriptor, Is.EqualTo("Test Merchant"));
            Assert.That(result.RecurringSeries, Is.EqualTo("RS123"));
            Assert.That(result.Last4, Is.EqualTo("1111"));
            Assert.That(result.SavedCardId, Is.EqualTo("CC123"));
            Assert.That(result.WalletId, Is.EqualTo("WALLET123"));
            Assert.That(result.Signature, Is.EqualTo("test-signature"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithEmptyResponseFields_HandlesGracefully()
    {
        var request = CreateValidProcessRequest();
        var response = "Reply=&ReplyDesc=&TransID=&Amount=&Currency=";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo(string.Empty));
            Assert.That(result.ReplyDescription, Is.EqualTo(string.Empty));
            Assert.That(result.TransactionId, Is.EqualTo(string.Empty));
            Assert.That(result.Amount, Is.EqualTo(string.Empty));
            Assert.That(result.Currency, Is.EqualTo(string.Empty));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task ProcessAsync_WithUrlEncodedValues_DecodesCorrectly()
    {
        var request = CreateValidProcessRequest();
        var response = "Reply=553&ReplyDesc=3D+Secure+Redirection+is+needed&D3Redirect=https%3A%2F%2Fprocess.example.com%2Fmember%2FremoteCharge_ccDebitGeneric3DSAuth.asp";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.ProcessAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("553"));
            Assert.That(result.ReplyDescription, Is.EqualTo("3D Secure Redirection is needed"));
            Assert.That(result.AuthenticationRedirectUrl, Contains.Substring("https://process.example.com"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task RefundAsync_WithValidRequest_BuildsCorrectUrl()
    {
        var request = new LegacyRefundRequest
        {
            MerchantNumber = "2020202",
            Amount = 50.00m,
            CurrencyIso = "USD",
            TransactionId = 12345,
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Amount=50.00&Currency=840";
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(response, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.RefundAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("CompanyNum=2020202"));
            Assert.That(capturedUrl, Contains.Substring("requestSource=53"));
            Assert.That(capturedUrl, Contains.Substring("Amount=50"));
            Assert.That(capturedUrl, Contains.Substring("Currency=USD"));
            Assert.That(capturedUrl, Contains.Substring("TransType=0"));
            Assert.That(capturedUrl, Contains.Substring("TypeCredit=0"));
            Assert.That(capturedUrl, Contains.Substring("ClientIP=192.168.1.1"));
            Assert.That(capturedUrl, Contains.Substring("RefTransID=12345"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task RefundAsync_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = new LegacyRefundRequest
        {
            MerchantNumber = "2020202",
            Amount = 50.00m,
            CurrencyIso = "USD",
            TransactionId = 12345,
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Amount=50.00&Currency=840&ReplyDesc=Refund approved";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.RefundAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("000"));
            Assert.That(result.TransactionId, Is.EqualTo("12345"));
            Assert.That(result.Amount, Is.EqualTo("50.00"));
            Assert.That(result.Currency, Is.EqualTo("840"));
            Assert.That(result.ReplyDescription, Is.EqualTo("Refund approved"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task VoidAsync_WithValidRequest_BuildsCorrectUrl()
    {
        var request = new LegacyVoidRequest
        {
            MerchantNumber = "2020202",
            CurrencyIso = "USD",
            TransactionId = "12345",
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Currency=840";
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(response, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.VoidAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("CompanyNum=2020202"));
            Assert.That(capturedUrl, Contains.Substring("requestSource=53"));
            Assert.That(capturedUrl, Contains.Substring("Currency=USD"));
            Assert.That(capturedUrl, Contains.Substring("TransType=4"));
            Assert.That(capturedUrl, Contains.Substring("ClientIP=192.168.1.1"));
            Assert.That(capturedUrl, Contains.Substring("TransApprovalID=12345"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task VoidAsync_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = new LegacyVoidRequest
        {
            MerchantNumber = "2020202",
            CurrencyIso = "USD",
            TransactionId = "12345",
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Currency=840&ReplyDesc=Void approved";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.VoidAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("000"));
            Assert.That(result.TransactionId, Is.EqualTo("12345"));
            Assert.That(result.Currency, Is.EqualTo("840"));
            Assert.That(result.ReplyDescription, Is.EqualTo("Void approved"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task CaptureAsync_WithValidRequest_BuildsCorrectUrl()
    {
        var request = new LegacyCaptureRequest
        {
            MerchantNumber = "2020202",
            CurrencyIso = "USD",
            TransactionId = "12345",
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Currency=840";
        var capturedUrl = string.Empty;

        var handler = new MockHttpMessageHandler(response, url => capturedUrl = url);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        await service.CaptureAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(capturedUrl, Contains.Substring("CompanyNum=2020202"));
            Assert.That(capturedUrl, Contains.Substring("requestSource=53"));
            Assert.That(capturedUrl, Contains.Substring("Currency=USD"));
            Assert.That(capturedUrl, Contains.Substring("TransType=2"));
            Assert.That(capturedUrl, Contains.Substring("ClientIP=192.168.1.1"));
            Assert.That(capturedUrl, Contains.Substring("TransApprovalID=12345"));
        });

        httpClient.Dispose();
    }

    [Test]
    public async Task CaptureAsync_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = new LegacyCaptureRequest
        {
            MerchantNumber = "2020202",
            CurrencyIso = "USD",
            TransactionId = "12345",
            ClientIP = "192.168.1.1"
        };
        var response = "Reply=000&TransID=12345&Currency=840&ReplyDesc=Capture approved";

        var handler = new MockHttpMessageHandler(response);
        var httpClient = CreateHttpClient(handler);
        var service = CreateService(httpClient);

        var result = await service.CaptureAsync(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.ReplyCode, Is.EqualTo("000"));
            Assert.That(result.TransactionId, Is.EqualTo("12345"));
            Assert.That(result.Currency, Is.EqualTo("840"));
            Assert.That(result.ReplyDescription, Is.EqualTo("Capture approved"));
        });

        httpClient.Dispose();
    }

    private static LegacyProcessRequest CreateValidProcessRequest()
    {
        return new LegacyProcessRequest
        {
            MerchantNumber = "2020202",
            CreditCard = new LegacyCreditCard
            {
                Number = "5200000000002151",
                ExpirationMonth = 12,
                ExpirationYear = 2027,
                HolderName = "James Bobby",
                Cvv = "111",
                BillingAddress = new LegacyBillingAddress
                {
                    AddressLine1 = "Via Carlo D'Adda, 13",
                    AddressLine2 = "Apt 2",
                    City = "Milan",
                    PostalCode = "20143",
                    StateIso = "IT",
                    CountryIso = "IT"
                }
            },
            Customer = new LegacyCustomer
            {
                Email = "support@ezzygate.com",
                PhoneNumber = "393923229295",
                PersonalIdNumber = "20004368"
            },
            Amount = 55.3m,
            CurrencyIso = "USD",
            Installments = 1,
            TransType = 0,
            TypeCredit = 1,
            TrmCode = 7,
            PostRedirectUrl = "http://localhost:4201/payment-callback",
            StoreCc = 0,
            ClientIP = "",
            TipAmount = 0
        };
    }

    private static string CreatePending3DSecureResponse()
    {
        return "TransType=0&Reply=553&TransID=13696&Date=22/12/2025 14:57:10&Order=&Amount=55.30&Payments=1&Currency=1&ConfirmationNum=cn-a2e11d7e-60a4-41ce-bd2e-eaaf0c0c5710&Comment=&ReplyDesc=3D Secure Redirection is needed&CCType=Mastercard&Descriptor=Do+not+remove&RecurringSeries=&Last4=2151&ccStorageID=&Source=WEBAPI&WalletID=&D3Redirect=https%3A%2F%2Fprocess%2Eexample%2Ecom%2Fmember%2FremoteCharge%5FccDebitGeneric3DSAuth%2Easp%3FapproveUrl%3Dhttps%253a%252f%252fprocess%2Eexample%2Ecom%252fmember%252fremoteCharge%5FccDebitGenericFinalize%2Easp%253ftransactionReferenceCode%253dc0434925%2Daa23%2D4562%2D87a0%2Da59c8b28c4ae%2526type%253dSuccessRedirect%2526transactionLogId%253d17692%2526signature%253dQ0XwOnxZUqDK0phWjQojy2%25252fzd7q2HnolULdvR0KIkZk%25253d%26declineUrl%3Dhttps%253a%252f%252fprocess%2Eexample%2Ecom%252fmember%252fremoteCharge%5FccDebitGenericFinalize%2Easp%253ftransactionReferenceCode%253dc0434925%2Daa23%2D4562%2D87a0%2Da59c8b28c4ae%2526type%253dFailureRedirect%2526transactionLogId%253d17692%2526signature%253d8HRkjx8D2ngR6KEoqSlkZR0QAqhlHxPGNd3sHKUCgIQ%25253d%26TransID%3D13696&D3RedirectMethod=SameWindow&signType=SHA256&signature=jPvY1Gn4G5wgA9UBED7hlwTe5KaJFvOPn9PyNPzNA7s%3D";
    }

    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly Action<string>? _urlCapture;
        private readonly bool _shouldThrow;

        public MockHttpMessageHandler(string response, Action<string>? urlCapture = null, bool shouldThrow = false)
        {
            _response = response;
            _urlCapture = urlCapture;
            _shouldThrow = shouldThrow;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_shouldThrow)
            {
                throw new HttpRequestException("Simulated HTTP error");
            }

            var requestUri = request.RequestUri?.ToString() ?? string.Empty;
            _urlCapture?.Invoke(requestUri);

            return Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(_response, Encoding.UTF8, "text/plain")
            });
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return SendAsync(request, cancellationToken).GetAwaiter().GetResult();
        }
    }
}