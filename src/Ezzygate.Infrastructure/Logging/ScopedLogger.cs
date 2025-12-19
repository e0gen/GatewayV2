using System.Text;
using Microsoft.Extensions.Logging;
using Ezzygate.Infrastructure.Ef.Models;

namespace Ezzygate.Infrastructure.Logging
{
    public sealed class ScopedLogger : IDisposable
    {
        private readonly StringBuilder _longMessage = new();
        private LogSeverity _severity = LogSeverity.Info;
        private bool _disposed;
        private readonly ILogger _logger;
        private readonly LogTag _logTag;
        private readonly Func<string?, string?> _formatShortMessage = message => message;
        private string? _shortMessage;

        public ScopedLogger(ILogger logger, LogTag logTag, string? shortMessage = null)
        {
            _logger = logger;
            _logTag = logTag;
            _shortMessage = shortMessage;
        }
        
        public ScopedLogger(ILogger logger, LogTag logTag, Func<string?, string?> formatShortMessage)
        {
            _logger = logger;
            _logTag = logTag;
            _formatShortMessage = formatShortMessage;
        }

        public void SetShortMessage(string message)
        {
            _shortMessage = message;
        }

        public void Info(string message)
        {
            _longMessage.AppendLine(message);
        }

        public void Warn(string message)
        {
            Info(message);
            _severity = LogSeverity.Warning;
        }

        public void Error(string message)
        {
            Info(message);
            _severity = LogSeverity.Error;
        }

        private string GetShortMessage() => _formatShortMessage.Invoke(_shortMessage) ?? "";

        private string GetLongMessage() => _longMessage.ToString();

        private void Flush()
        {
            switch (_severity)
            {
                case LogSeverity.Warning: _logger.WarnExtra(_logTag, GetLongMessage(), GetShortMessage());
                    break;
                case LogSeverity.Error: _logger.ErrorExtra(_logTag, GetLongMessage(), GetShortMessage());
                    break;
                case LogSeverity.Info:
                default: 
                    _logger.InfoExtra(_logTag, GetLongMessage(), GetShortMessage());
                    break;
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            Flush();
            _longMessage.Clear();
            _disposed = true;
        }
    }
}