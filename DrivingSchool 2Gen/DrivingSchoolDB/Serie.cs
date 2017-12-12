using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolDB
{
    public class Serie
    {
        public int Id { get; set; }

        [Required, Display(Name = "Serie Number")]
        public string Number { get; set; }

        [Required, Display(Name = "Picture")]
        public string Image { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
