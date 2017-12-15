using AutoMapper;
using DrivingSchoolDB;
using DrivingSchoolWeb.ViewModel;

namespace DrivingSchoolWeb.Models
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Serie, SerieViewModel>();
        }
    }
}
