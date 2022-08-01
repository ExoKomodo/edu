using System.Collections.Generic;

namespace Client.Models.School
{
    public abstract class Course : Model<string>
    {
        public string Name { get; protected set; }

        protected Course(string Id, string Name) {
            this.Id = Id;
            this.Name = Name;
        }
    }
}