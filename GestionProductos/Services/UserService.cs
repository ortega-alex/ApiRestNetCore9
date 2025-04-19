using AutoMapper;
using GestionProductos.DTOs.User;
using GestionProductos.Helpers;
using GestionProductos.Interfaces;
using GestionProductos.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestionProductos.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repository;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, JwtSettings jwtSettings, IMapper mapper)
        {
            _repository = repository;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsyn()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserTokenDto?> LoginAsync(LoginUserDto dto)
        {
            var user = await _repository.GetByUsernameAsync(username: dto.Username);
            if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.Password))
                return null;

            var userTokenDto = _mapper.Map<UserTokenDto>(user);
            userTokenDto.Token = JwtHelpers.GetToken(user, _jwtSettings);

            return userTokenDto;
        }

        public async Task<UserTokenDto?> RefreshToken(RefreshTokenDto dto)
        {
            var user = await _repository.GetByUsernameAsync(username: dto.Username);
            if (user == null || user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            user.RefreshToken = JwtHelpers.GetRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateAsync(user);
            await _repository.SaveAsync();


            var userTokenDto = _mapper.Map<UserTokenDto>(user);
            userTokenDto.Token = JwtHelpers.GetToken(user, _jwtSettings);
            userTokenDto.RefreshToken = user.RefreshToken;
            return userTokenDto;
        }

        public async Task<UserTokenDto?> RegisterAsync(RegisterUserDto dto)
        {
            var userExists = await _repository.GetByUsernameAsync(username: dto.Username);
            if (userExists != null)
                return null;

            var user = _mapper.Map<User>(dto);
            user.Password = PasswordHelper.HashPassword(dto.Password);
            user.RefreshToken = JwtHelpers.GetRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repository.AddAsync(user);
            await _repository.SaveAsync();

            var userTokenDto = _mapper.Map<UserTokenDto>(user);
            userTokenDto.Token = JwtHelpers.GetToken(user, _jwtSettings);
            userTokenDto.RefreshToken = user.RefreshToken;

            return userTokenDto;
        }

        public async Task UpdateAsync(int id, RegisterUserDto dto)
        {
             var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            existing.Password = PasswordHelper.HashPassword(dto.Password);
            existing.RefreshToken = JwtHelpers.GetRefreshToken();
            existing.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);           

            await _repository.UpdateAsync(existing);
            await _repository.SaveAsync();
        }
    }
}
