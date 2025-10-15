using JetBrains.Annotations;

namespace Ezzygate.Integrations.Ph3a.Api;

public record LoginRequest([UsedImplicitly] string UserName);