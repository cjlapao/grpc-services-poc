using System;

namespace Framework.Helpers
{
    public static class Guard
    {
        public static void ForNull(object param, string parameterName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(parameterName, "Parameter can not be null");
            }
        }

        public static void ForNull(object param, string parameterName, string customErrorMessage)
        {
            if (param == null)
            {
                throw new ArgumentNullException(parameterName, customErrorMessage);
            }
        }

        public static void EmptyGuid(Guid param, string parameterName)
        {
            if (param == Guid.Empty)
            {
                throw new ArgumentException(parameterName, "Parameter can not be an empty Guid");
            }
        }

        public static void EmptyString(string param, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                throw new ArgumentException(parameterName, "Parameter can not be an null/empty string");
            }
        }
    }
}