using System;

namespace Library
{
    public class JsonConverterException : Exception
    {
        public JsonConverterException(String message) 
            :base(message)
        {
        }
    }
}