using System;
using Library.Exceptions;

namespace Library
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