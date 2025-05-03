using AutoMapper;
using blog_server.Constants;
using blog_server.DTOs.Auth;
using blog_server.DTOs.Category;
using blog_server.DTOs.Role;
using blog_server.DTOs.Type;
using blog_server.DTOs.User;
using blog_server.Models;
using blog_server.Services.Impl;

namespace blog_server.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, RegisterResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<User, MyInfoResponse>();
        CreateMap<UserCacheDto, MyInfoResponse>();
        CreateMap<User, UserCacheDto>();
        CreateMap<UpdateInfoRequest, User>();

        // Category
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CreateCategoryResponse>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, UpdateCategoryResponse>();
        CreateMap<Category, DetailCategoryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(
                dest => dest.Children,
                opt =>
                    opt.MapFrom(src =>
                        src.Children.Select(c => new ChildCategoryDto
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Description = c.Description,
                                ParentId = c.ParentId,
                                Status = c.Status,
                            })
                            .ToList()
                    )
            );
        CreateMap<Category, ParentCategoryDto>();
        CreateMap<Category, ChildCategoryDto>();

        // User
        CreateMap<User, ListUserResponse>()
            .ForMember(
                dest => dest.Roles,
                opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList())
            );

        // Type
        CreateMap<Role, ListTypeResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<AppStatus, ListTypeResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => ((int)src).ToString()));

        // Role
        CreateMap<Role, ListRoleResponse>();
        CreateMap<CreateRoleRequest, Role>();
        CreateMap<UpdateRoleRequest, Role>();
        CreateMap<DeleteRoleRequest, Role>();
    }
}
