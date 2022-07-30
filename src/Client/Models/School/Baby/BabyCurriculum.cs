using Client.Models.School;
using System.Collections.Generic;

namespace Client.Models.School.Baby
{
    public class BabyCurriculum : Curriculum
    {
        public BabyCurriculum()
            : base(
                "baby",
                "Baby Curriculum",
                new List<Course>
                {
                    new Colors(),
                    new Shapes(),
                }
            ) {}
    }
}
