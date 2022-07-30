using System.Collections.Generic;

namespace Client.Models
{
    public class Blog : Model<string>
    {
        public BlogContent Content { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; protected set; }
    }
}