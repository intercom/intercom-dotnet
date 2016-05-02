using System;

namespace Intercom.Exceptions
{
    public class JsonConverterException : IntercomException
    {
        public String Json { set; get; }
        public String SerializationType { set; get; }

        public JsonConverterException(String message) 
            :base(message)
        {
        }

        public JsonConverterException (String message, Exception innerException) 
            :base(message, innerException)
        {
        }
    }
}