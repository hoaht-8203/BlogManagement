using System;
using AutoMapper;
using blog_server.DTOs.Auth;
using blog_server.Models;

namespace blog_server.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegisterResponse>();
        CreateMap<User, MyInfoResponse>();
    }
}
