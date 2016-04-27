using System;

namespace Library
{
    public class ConversationCount : Count
    {
        public int assigned { set; get; }
        public int closed { set; get; }
        public int open { set; get; }
        public int unassigned { set; get; }

        public ConversationCount()
        {
        }
    }
}