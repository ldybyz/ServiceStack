﻿using System;
using Serilog;
using Serilog.Events;

namespace ServiceStack.Logging.Serilog
{
    /// <summary>
    /// Implementation of <see cref="ILog"/> for <see cref="Serilog"/>.
    /// </summary>
    public class SerilogLogger : ILog
    {
        private readonly ILogger _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogLogger"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/>.</param>
        public SerilogLogger(Type type)
        {
            _log = Log.ForContext(type);
        }

        /// <summary>
        /// Gets a value indicating if Debug messages are enabled.
        /// </summary>
        public bool IsDebugEnabled => _log.IsEnabled(LogEventLevel.Debug);

        /// <summary>
        /// Gets a value indicating if Info messages are enabled.
        /// </summary>
        public bool IsInfoEnabled => _log.IsEnabled(LogEventLevel.Information);

        /// <summary>
        /// Gets a value indicating if Warning messages are enabled.
        /// </summary>
        public bool IsWarnEnabled => _log.IsEnabled(LogEventLevel.Warning);

        /// <summary>
        /// Gets a value indicating if Error messages are enabled.
        /// </summary>
        public bool IsErrorEnabled => _log.IsEnabled(LogEventLevel.Error);

        /// <summary>
        /// Gets a value indicating if Fatal messages are enabled.
        /// </summary>
        public bool IsFatalEnabled => _log.IsEnabled(LogEventLevel.Fatal);

        /// <summary>
        /// Logs a Debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            if (IsDebugEnabled)
                Write(LogEventLevel.Debug, message);
        }

        /// <summary>
        /// Logs a Debug message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Debug(object message, Exception exception)
        {
            if (IsDebugEnabled)
                Write(LogEventLevel.Debug, message, exception);
        }

        /// <summary>
        /// Logs a Debug format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
                Write(LogEventLevel.Debug, format, args);
        }

        /// <summary>
        /// Logs a Error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            if (IsErrorEnabled)
                Write(LogEventLevel.Error, message);
        }

        /// <summary>
        /// Logs a Error message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            if (IsErrorEnabled)
                Write(LogEventLevel.Error, message, exception);
        }

        /// <summary>
        /// Logs a Error format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
                Write(LogEventLevel.Error, format, args);
        }

        /// <summary>
        /// Logs a Fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(object message)
        {
            if (IsFatalEnabled)
                Write(LogEventLevel.Fatal, message);
        }

        /// <summary>
        /// Logs a Fatal message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            if (IsFatalEnabled)
                Write(LogEventLevel.Fatal, message, exception);
        }

        /// <summary>
        /// Logs a Fatal format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
                Write(LogEventLevel.Fatal, format, args);
        }

        /// <summary>
        /// Logs a Info message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            if (IsInfoEnabled)
                Write(LogEventLevel.Information, message);
        }

        /// <summary>
        /// Logs a Info message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Info(object message, Exception exception)
        {
            if (IsInfoEnabled)
                Write(LogEventLevel.Information, message, exception);
        }

        /// <summary>
        /// Logs a Info format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
                Write(LogEventLevel.Information, format, args);
        }

        /// <summary>
        /// Logs a Warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(object message)
        {
            if (IsWarnEnabled)
                Write(LogEventLevel.Warning, message);
        }

        /// <summary>
        /// Logs a Warning message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warn(object message, Exception exception)
        {
            if (IsWarnEnabled)
                Write(LogEventLevel.Warning, message, exception);
        }

        /// <summary>
        /// Logs a Warning format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
                Write(LogEventLevel.Warning, format, args);
        }

        private void Write(LogEventLevel level, object message)
        {
            var messageTemplate = message as string;
            if (messageTemplate != null)
            {
                _log.Write(level, messageTemplate);
                return;
            }

            var exception = message as Exception;
            if (exception != null)
            {
                _log.Write(level, exception, exception.GetType().Name);
                return;
            }

            _log.Write(level, message.ToString());
        }

        private void Write(LogEventLevel level, object message, Exception exception)
        {
            var messageTemplate = message as string;
            if (messageTemplate != null)
            {
                _log.Write(level, exception, messageTemplate);
                return;
            }

            _log.Write(level, exception, message.ToString());
        }

        private void Write(LogEventLevel level, string format, params object[] args)
        {
            _log.Write(level, format, args);
        }
    }
}
