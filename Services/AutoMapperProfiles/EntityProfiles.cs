using AutoMapper;
using DataLayer.Entities;
using Services.Dtos.Wish;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AutoMapperProfiles
{
    public class EntityProfiles : Profile
    {
        public EntityProfiles()
        {
            CreateMap<GetWishDto, Wish>().ReverseMap();
            CreateMap<AddWishDto, Wish>().ReverseMap();
        }
    }
}
