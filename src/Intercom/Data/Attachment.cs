using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;


namespace Library.Data
{
    public class Attachment: Model
    {
        public string name { get; set; }
        public string url { get; set; }
        public string content_type { get; set; }
        public int filesize { get; set; }
        public object width { get; set; }
        public object height { get; set; }
    }
}