using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Mappings
{
    public class AutoMapperProfile:Profile
    { 
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<Security, SecurityDTO>().ReverseMap();

        }

    }
}