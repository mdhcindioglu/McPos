using AutoMapper;
using McPos.Server.Data.Models;
using McPos.Shared.ViewModels;

namespace McPos.Server.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
        }
    }
}
