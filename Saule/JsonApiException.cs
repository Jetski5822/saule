using System;
using System.Collections;

namespace Saule
{
    internal enum ErrorType
    {
        /// <summary>
        /// An error that is the server (application)'s fault.
        /// </summary>
        Server,

        /// <summary>
        /// An error that is the client (application user)'s fault.
        /// </summary>
        Client
    }

    /// <summary>
    /// The exception that is thrown when an the Json Api serializer misses necessary information.
    /// </summary>
    [Serializable]
    public class JsonApiException : Exception
    {
        internal JsonApiException(ErrorType type, string message)
            : base(message)
        {
            ErrorType = type;
        }

        internal JsonApiException(ErrorType type, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorType = type;
        }


        internal ErrorType ErrorType { get; }

        public override IDictionary Data {
           get {
               var data = base.Data;
               data.Add("ErrorType", ErrorType);
               return data;
           }
       }
    }
}