using System;

namespace Client.Models
{
    public class Blog : Model<string>
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}