using System.Collections.Generic;

namespace DrivingSchoolDB
{
    public class Serie
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
