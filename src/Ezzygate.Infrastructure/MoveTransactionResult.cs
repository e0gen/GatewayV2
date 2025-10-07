using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure;

public record MoveTransactionResult(int TrxId, TblCompanyTransPending PendingTrx);