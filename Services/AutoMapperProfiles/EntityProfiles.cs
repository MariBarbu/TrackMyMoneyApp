﻿using AutoMapper;
using DataLayer.Entities;
using Services.Dtos.Category;
using Services.Dtos.Spending;
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
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();
            CreateMap<AddSpendingDto, Spending>().ReverseMap();

        }
    }
}
