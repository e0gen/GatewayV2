using System;
using System.Threading.Tasks;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Integrations.Ph3a.Api;

namespace Ezzygate.Integrations.Ph3a;

public class Ph3AService : IPh3AService
{
    private readonly Ph3AApiClient _client;
    private readonly IRiskSettingsRepository _riskSettingsRepository;

    private const string UserName = "dfc13c8d-a932-1e7b-d737-04f573bbef73";

    public Ph3AService(Ph3AApiClient client, IRiskSettingsRepository riskSettingsRepository)
    {
        _client = client;
        _riskSettingsRepository = riskSettingsRepository;
    }

    public async Task<bool> ValidateScore(Ph3ARequest request, int merchantId)
    {
        await _client.AuthenticateAsync(UserName);

        var analysisResult = await _client.GetQuizAnalysisAsync(request);
        if (analysisResult.Response is null)
            throw new Exception($"Analysis response is null. StatusCode: {analysisResult.StatusCode}");

        var response = analysisResult.Response;
        var score = response.Data.ParsedExecutionMessage.NotNull().Score;

        var settings = await _riskSettingsRepository.GetByMerchantIdAsync(merchantId);
        if (settings == null)
            return true;

        return settings.Ph3AMinScore > score;
    }
}