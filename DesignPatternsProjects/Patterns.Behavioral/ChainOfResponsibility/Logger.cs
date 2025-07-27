using System;

namespace Patterns.Behavioral.ChainOfResponsibility
{
    /// <summary>
    /// Niveau de log
    /// </summary>
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
    }

    /// <summary>
    /// Message de log
    /// </summary>
    public class LogMessage
    {
        public LogLevel Level { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public LogMessage(LogLevel level, string message)
        {
            Level = level;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Handler abstrait pour les logs
    /// </summary>
    public abstract class LogHandler
    {
        protected LogLevel Level;
        protected LogHandler NextHandler;
        protected string HandlerName;

        protected LogHandler(LogLevel level, string name)
        {
            Level = level;
            HandlerName = name;
        }

        public LogHandler SetNext(LogHandler handler)
        {
            NextHandler = handler;
            return handler;
        }

        public virtual void Handle(LogMessage message)
        {
            if (message.Level >= Level)
            {
                ProcessMessage(message);
            }

            NextHandler?.Handle(message);
        }

        protected abstract void ProcessMessage(LogMessage message);
    }

    /// <summary>
    /// Concrete Handler - Console Logger
    /// </summary>
    public class ConsoleLogHandler : LogHandler
    {
        public ConsoleLogHandler(LogLevel level) 
            : base(level, "Console Logger")
        {
        }

        protected override void ProcessMessage(LogMessage message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = GetColorForLevel(message.Level);
            
            Console.WriteLine($"[{HandlerName}] [{message.Level}] {message.Timestamp:HH:mm:ss} - {message.Message}");
            
            Console.ForegroundColor = color;
        }

        private ConsoleColor GetColorForLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }

    /// <summary>
    /// Concrete Handler - File Logger
    /// </summary>
    public class FileLogHandler : LogHandler
    {
        private readonly string _filePath;

        public FileLogHandler(LogLevel level, string filePath) 
            : base(level, "File Logger")
        {
            _filePath = filePath;
        }

        protected override void ProcessMessage(LogMessage message)
        {
            var logEntry = $"{message.Timestamp:yyyy-MM-dd HH:mm:ss} [{message.Level}] {message.Message}";
            Console.WriteLine($"[{HandlerName}] Écriture dans le fichier {_filePath}:");
            Console.WriteLine($"   {logEntry}");
            // Dans une implémentation réelle, on écrirait dans le fichier :
            // File.AppendAllText(_filePath, logEntry + Environment.NewLine);
        }
    }

    /// <summary>
    /// Concrete Handler - Email Logger
    /// </summary>
    public class EmailLogHandler : LogHandler
    {
        private readonly string _recipientEmail;

        public EmailLogHandler(LogLevel level, string recipientEmail) 
            : base(level, "Email Logger")
        {
            _recipientEmail = recipientEmail;
        }

        protected override void ProcessMessage(LogMessage message)
        {
            Console.WriteLine($"[{HandlerName}] Envoi d'un email à {_recipientEmail}:");
            Console.WriteLine($"   Sujet: Log {message.Level} - {message.Timestamp:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"   Contenu: {message.Message}");
            // Dans une implémentation réelle, on enverrait un email
        }
    }

    /// <summary>
    /// Logger - Point d'entrée pour la chaîne de responsabilité
    /// </summary>
    public class Logger
    {
        private readonly LogHandler _handler;

        public Logger()
        {
            // Créer la chaîne de responsabilité
            var consoleHandler = new ConsoleLogHandler(LogLevel.Debug);
            var fileHandler = new FileLogHandler(LogLevel.Info, "app.log");
            var emailHandler = new EmailLogHandler(LogLevel.Error, "admin@example.com");

            // Configurer la chaîne
            consoleHandler.SetNext(fileHandler).SetNext(emailHandler);

            _handler = consoleHandler;
        }

        public void Log(LogLevel level, string message)
        {
            _handler.Handle(new LogMessage(level, message));
        }

        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warning(string message) => Log(LogLevel.Warning, message);
        public void Error(string message) => Log(LogLevel.Error, message);
    }
}
