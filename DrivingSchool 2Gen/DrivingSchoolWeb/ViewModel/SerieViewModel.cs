using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolWeb.ViewModel
{
    public class SerieViewModel
    {
        [Required]
        public String Number { get; set; }
        [Required]
        public String Image { get; set; }

        public IFormFile MyImage { set; get; }
    }
}
