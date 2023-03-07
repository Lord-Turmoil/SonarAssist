using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAssist.Common
{
    public class Messages
    {
        private Messages() { }

        // Error messages.
        public const string BadArguments = "Arguments illegal";
        public const string NotInitialized = "Project not initialized";

        public const string BadEnvironment = "Missing environment variables";

        public const string ServiceError = "Service error encountered";

        public const string UnexpectedError = "Unexpected error encountered";

        public const string CompilationFailed = "Java compilation failed";
        public const string ScanFailed = "Failed to scan project";

        // Fine messages.
        
    }
}
