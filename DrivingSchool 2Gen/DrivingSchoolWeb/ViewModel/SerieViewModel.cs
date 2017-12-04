using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolWeb.ViewModel
{
    public class SerieViewModel
    {
        [Required, Display(Name = "Serie Number")]
        public String Number { get; set; }
        [Display(Name = "Image Name")]
        public String Image { get; set; }
        [Required, Display(Name = "Image file")]
        public IFormFile MyImage { set; get; }
    }
}
