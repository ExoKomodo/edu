using System.Collections.Generic;

namespace Client.Models.School
{
    public abstract class Curriculum : Model<string>
    {
        public ICollection<Course> Courses { get; protected set; }
        public string Name { get; protected set; }

        protected Curriculum(string Id, string Name, ICollection<Course> Courses) {
            this.Id = Id;
            this.Name = Name;
            this.Courses = Courses;
        }
    }
}