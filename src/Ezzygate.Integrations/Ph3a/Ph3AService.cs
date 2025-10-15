using System;
using System.Threading.Tasks;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Integrations.Ph3a.Api;

namespace Ezzygate.Integrations.Ph3a;

public class Ph3AService : IPh3AService
{
    private readonly Ph3AApiClient _apiClient;

    private const string UserName = "dfc13c8d-a932-1e7b-d737-04f573bbef73";

    public Ph3AService(Ph3AApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<string> CheckScore(Ph3ARequest request, int merchantId)
    {
        await _apiClient.AuthenticateAsync(UserName);

        var analysisResult = await _apiClient.GetQuizAnalysisAsync(request);
        if (analysisResult.Response is null)
            throw new Exception($"Analysis response is null. StatusCode: {analysisResult.StatusCode}");

        var response = analysisResult.Response;
        var score = response.Data.ParsedExecutionMessage.NotNull().Score;

        //TODO Support Bll.Merchants.RiskSettings validation
        // var settings = Bll.Merchants.RiskSettings.Load(merchantId);
        // return settings.Ph3aMinScore > score ? "580" : "000";
        return "000";
    }
}