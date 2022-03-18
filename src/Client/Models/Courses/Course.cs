using System.Collections.Generic;

namespace Client.Models.Courses
{
    public abstract class Course : Model<string>
    {
        public string Name { get; protected set; }

        public Course(string Id, string Name) {
            this.Id = Id;
            this.Name = Name;
        }

        public static ICollection<Course> Curriculum = new List<Course> {
            new DLD(),
            new DataStructures(),  
        };
    }
}