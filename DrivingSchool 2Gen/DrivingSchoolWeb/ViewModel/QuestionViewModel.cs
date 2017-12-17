using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolWeb.ViewModel
{
    public class QuestionViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Audio { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, Display(Name = "Answers")]
        public string Answeres { get; set; }

        [Required, Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }

        [Required, Display(Name = "Serie Number")]
        public int SerieId { get; set; }
    }
}
