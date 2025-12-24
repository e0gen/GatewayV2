using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Services;
using Ezzygate.WebApi.Controllers.Merchants;
using Ezzygate.WebApi.Dtos.Merchants.Data;

namespace Ezzygate.WebApi.Tests.Controllers.Merchants;

[TestFixture]
public class DataControllerTests
{
    private Mock<ILogger<DataController>> _loggerMock;
    private Mock<ITransactionRepository> _transactionRepositoryMock;
    private Mock<ITransactionService> _transactionServiceMock;
    private DataController _controller;
    private Merchant? _stubMerchant;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<DataController>>();
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _transactionServiceMock = new Mock<ITransactionService>();

        _stubMerchant = new Merchant { Id = 1, CustomerNumber = "TEST123" };

        _controller = CreateController(_stubMerchant);
    }

    [Test]
    public void Version3_ReturnsSuccessWithVersion3()
    {
        var response = _controller.Version3();

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(response.Data, Is.EqualTo("3.00"));
        });
    }

    [Test]
    public void Version4_ReturnsSuccessWithVersion4()
    {
        var response = _controller.Version4();

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(response.Data, Is.EqualTo("4.00"));
        });
    }

    [Test]
    public async Task PendingData_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Pending, 12345);
        var searchResult = CreateTransactionSearchResult();

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([searchResult]);

        var response = await _controller.PendingData(request, CancellationToken.None);
        var responseData = response.Data as TransactionInfoResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.Id, Is.EqualTo(searchResult.Id));
            Assert.That(responseData.Status, Is.EqualTo(searchResult.Status));
            Assert.That(responseData.Amount, Is.EqualTo(searchResult.Amount));
        });
    }

    [Test]
    public async Task PendingData_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var controller = CreateController(null);
        var request = CreateValidPendingStatusRequest();

        var response = await controller.PendingData(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public async Task PendingData_WhenLookupReturnsNull_ReturnsTransactionNotFoundResponse()
    {
        var request = CreateValidPendingStatusRequest();

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PendingLookupResult?)null);

        var response = await _controller.PendingData(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("TransactionNotFound"));
    }

    [Test]
    public async Task PendingData_WhenSearchReturnsEmpty_ReturnsTransactionNotFoundResponse()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Pending, 12345);

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TransactionSearchResult>());

        var response = await _controller.PendingData(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("TransactionNotFound"));
    }

    [Test]
    public async Task PendingStatus_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Pending, 12345);

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        var response = await _controller.PendingStatus(request, CancellationToken.None);
        var responseData = response.Data as PendingStatusResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.TransactionId, Is.EqualTo(lookupResult.TransactionId));
            Assert.That(responseData.Status, Is.EqualTo(lookupResult.Status.ToString()));
        });
    }

    [Test]
    public async Task PendingStatus_WithAuthorizedStatus_ReturnsCorrectStatus()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Authorized, 12345);

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        var response = await _controller.PendingStatus(request, CancellationToken.None);
        var responseData = response.Data as PendingStatusResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData!.Status, Is.EqualTo("Authorized"));
        });
    }

    [Test]
    public async Task PendingStatus_WithCapturedStatus_ReturnsCorrectStatus()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Captured, 12345);

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        var response = await _controller.PendingStatus(request, CancellationToken.None);
        var responseData = response.Data as PendingStatusResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData!.Status, Is.EqualTo("Captured"));
        });
    }

    [Test]
    public async Task PendingStatus_WithDeclinedStatus_ReturnsCorrectStatus()
    {
        var request = CreateValidPendingStatusRequest();
        var lookupResult = CreatePendingLookupResult(TransactionStatusType.Declined, 12345);

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(lookupResult);

        var response = await _controller.PendingStatus(request, CancellationToken.None);
        var responseData = response.Data as PendingStatusResponseDto;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData!.Status, Is.EqualTo("Declined"));
        });
    }

    [Test]
    public async Task PendingStatus_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var controller = CreateController(null);
        var request = CreateValidPendingStatusRequest();

        var response = await controller.PendingStatus(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public async Task PendingStatus_WhenLookupReturnsNull_ReturnsTransactionNotFoundResponse()
    {
        var request = CreateValidPendingStatusRequest();

        _transactionServiceMock
            .Setup(x => x.LocatePendingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PendingLookupResult?)null);

        var response = await _controller.PendingStatus(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("TransactionNotFound"));
    }

    [Test]
    public async Task Transactions_WithValidRequest_ReturnsSuccessResponse()
    {
        var request = CreateValidTransactionInfoRequest();
        var searchResults = new List<TransactionSearchResult>
        {
            CreateTransactionSearchResult(),
            CreateTransactionSearchResult(2, "Authorized")
        };

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchResults);

        var response = await _controller.Transactions(request, CancellationToken.None);
        var responseData = response.Data as List<TransactionInfoResponseDto>;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.Count, Is.EqualTo(2));
            Assert.That(responseData[0].Id, Is.EqualTo(searchResults[0].Id));
            Assert.That(responseData[1].Id, Is.EqualTo(searchResults[1].Id));
        });
    }

    [Test]
    public async Task Transactions_WithDateRange_ReturnsFilteredResults()
    {
        var request = CreateValidTransactionInfoRequest();
        request.From = new DateTime(2025, 1, 1);
        request.To = new DateTime(2025, 12, 31);
        var searchResults = new List<TransactionSearchResult> { CreateTransactionSearchResult() };

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchResults);

        var response = await _controller.Transactions(request, CancellationToken.None);
        var responseData = response.Data as List<TransactionInfoResponseDto>;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Transactions_WithTransactionId_ReturnsFilteredResults()
    {
        var request = CreateValidTransactionInfoRequest();
        request.TransactionId = 12345;
        var searchResults = new List<TransactionSearchResult> { CreateTransactionSearchResult() };

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchResults);

        var response = await _controller.Transactions(request, CancellationToken.None);
        var responseData = response.Data as List<TransactionInfoResponseDto>;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Transactions_WithEmptyResults_ReturnsEmptyList()
    {
        var request = CreateValidTransactionInfoRequest();

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TransactionSearchResult>());

        var response = await _controller.Transactions(request, CancellationToken.None);
        var responseData = response.Data as List<TransactionInfoResponseDto>;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.EqualTo("Success"));
            Assert.That(responseData, Is.Not.Null);
            Assert.That(responseData!.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Transactions_WithMissingMerchant_ReturnsMerchantNotFoundResponse()
    {
        var controller = CreateController(null);
        var request = CreateValidTransactionInfoRequest();

        var response = await controller.Transactions(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("MerchantNotFound"));
    }

    [Test]
    public async Task Transactions_WhenRepositoryThrows_ReturnsGeneralErrorResponse()
    {
        var request = CreateValidTransactionInfoRequest();

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        var response = await _controller.Transactions(request, CancellationToken.None);

        Assert.That(response.Result, Is.EqualTo("GeneralError"));
    }

    [Test]
    public async Task Transactions_MapsAllTransactionFieldsCorrectly()
    {
        var request = CreateValidTransactionInfoRequest();
        var searchResult = CreateTransactionSearchResult(
            id: 12345,
            status: "Authorized",
            amount: 100.50m,
            approvalCode: "APP123",
            paymentMethodDisplay: "Visa ****1111",
            insertDate: new DateTime(2025, 1, 15, 10, 30, 0),
            currencyIso: "USD",
            installments: 3,
            comment: "Test transaction",
            responseCode: "000",
            responseMessage: "Approved",
            debitReferenceCode: "REF123",
            orderNumber: "ORDER123",
            payerFirstName: "John",
            payerLastName: "Doe",
            payerPersonalNumber: "12345",
            payerEmail: "john.doe@example.com",
            payerPhone: "+1234567890");

        _transactionRepositoryMock
            .Setup(x => x.SearchTransactionsAsync(
                It.IsAny<int>(),
                It.IsAny<TransactionStatusType>(),
                It.IsAny<int?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([searchResult]);

        var response = await _controller.Transactions(request, CancellationToken.None);
        var responseData = response.Data as List<TransactionInfoResponseDto>;
        var transaction = responseData!.First();

        Assert.Multiple(() =>
        {
            Assert.That(transaction.Id, Is.EqualTo(12345));
            Assert.That(transaction.Status, Is.EqualTo("Authorized"));
            Assert.That(transaction.Amount, Is.EqualTo(100.50m));
            Assert.That(transaction.ApprovalCode, Is.EqualTo("APP123"));
            Assert.That(transaction.Card, Is.EqualTo("Visa ****1111"));
            Assert.That(transaction.InsertDate, Is.EqualTo(new DateTime(2025, 1, 15, 10, 30, 0)));
            Assert.That(transaction.Currency, Is.EqualTo("USD"));
            Assert.That(transaction.Installments, Is.EqualTo(3));
            Assert.That(transaction.Comment, Is.EqualTo("Test transaction"));
            Assert.That(transaction.ResponseCode, Is.EqualTo("000"));
            Assert.That(transaction.ResponseMessage, Is.EqualTo("Approved"));
            Assert.That(transaction.DebitReferenceCode, Is.EqualTo("REF123"));
            Assert.That(transaction.OrderId, Is.EqualTo("ORDER123"));
            Assert.That(transaction.CardholderName, Is.EqualTo("John Doe"));
            Assert.That(transaction.PersonalNumber, Is.EqualTo("12345"));
            Assert.That(transaction.Email, Is.EqualTo("john.doe@example.com"));
            Assert.That(transaction.Phone, Is.EqualTo("+1234567890"));
        });
    }

    private DataController CreateController(Merchant? merchant)
    {
        var controller = new DataController(
            _loggerMock.Object,
            _transactionRepositoryMock.Object,
            _transactionServiceMock.Object);

        var httpContext = new DefaultHttpContext();
        if (merchant != null)
        {
            httpContext.SetMerchant(merchant);
        }
        controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        return controller;
    }

    private static PendingStatusRequestDto CreateValidPendingStatusRequest()
    {
        return new PendingStatusRequestDto
        {
            TransactionId = 12345
        };
    }

    private static TransactionInfoRequestDto CreateValidTransactionInfoRequest()
    {
        return new TransactionInfoRequestDto
        {
            Status = TransactionStatusType.Pending,
            TransactionId = null,
            From = null,
            To = null
        };
    }

    private static PendingLookupResult CreatePendingLookupResult(TransactionStatusType status, int transactionId)
    {
        return new PendingLookupResult(status, transactionId);
    }

    private static TransactionSearchResult CreateTransactionSearchResult(
        int id = 1,
        string status = "Pending",
        decimal amount = 100.00m,
        string? approvalCode = "APP001",
        string? paymentMethodDisplay = "Visa ****1111",
        DateTime? insertDate = null,
        string? currencyIso = "USD",
        int installments = 1,
        string? comment = "Test transaction",
        string? responseCode = "000",
        string? responseMessage = "Approved",
        string? debitReferenceCode = "REF001",
        string? orderNumber = "ORDER001",
        string? payerFirstName = "John",
        string? payerLastName = "Doe",
        string? payerPersonalNumber = "12345",
        string? payerEmail = "john.doe@example.com",
        string? payerPhone = "+1234567890")
    {
        return new TransactionSearchResult(
            Id: id,
            Status: status,
            Amount: amount,
            ApprovalCode: approvalCode,
            PaymentMethodDisplay: paymentMethodDisplay,
            InsertDate: insertDate ?? DateTime.UtcNow,
            CurrencyIso: currencyIso,
            Installments: installments,
            Comment: comment,
            ResponseCode: responseCode,
            ResponseMessage: responseMessage,
            DebitReferenceCode: debitReferenceCode,
            OrderNumber: orderNumber,
            PayerFirstName: payerFirstName,
            PayerLastName: payerLastName,
            PayerPersonalNumber: payerPersonalNumber,
            PayerEmail: payerEmail,
            PayerPhone: payerPhone);
    }
}