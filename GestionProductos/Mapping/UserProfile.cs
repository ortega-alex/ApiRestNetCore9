using AutoMapper;
using GestionProductos.DTOs.User;
using GestionProductos.Models;

namespace GestionProductos.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            // Model -> DTO
            CreateMap<User, UserDto>();
            CreateMap<User, UserTokenDto>();

            // DTO -> Model
            CreateMap<LoginUserDto, User>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
