using System.Globalization;

namespace BlogAPI.Helpers
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation) 
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base (message) { }

        public AppException(string message, params object[] args)
            : base (String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
