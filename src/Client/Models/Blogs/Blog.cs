using System.Collections.Generic;

namespace Client.Models.Courses
{
    public abstract class Blog : Model<string>
    {
				public string Title { get; protected set; }

        public Blog(string Id, string Title) {
            this.Id = Id;
            this.Title = Title;
        }
    }
}