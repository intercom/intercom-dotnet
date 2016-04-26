using System;

namespace Library
{
    public class Note : Model
    {
        public int created_at { get; set; }
        public string body { get; set; }
        public Author author { get; set; }
        public User user { get; set; }

        public Note()
        {
        }
    }
}