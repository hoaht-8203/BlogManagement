using AutoMapper;
using blog_server.DTOs.Auth;
using blog_server.DTOs.Category;
using blog_server.Models;

namespace blog_server.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegisterResponse>();
        CreateMap<User, MyInfoResponse>();

        // Category
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CreateCategoryResponse>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, UpdateCategoryResponse>();
        CreateMap<Category, DetailCategoryResponse>();
        CreateMap<Category, ParentCategoryDto>();
        CreateMap<Category, ChildCategoryDto>();
    }
}
