using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using DAL.Repos;

namespace BLL.Services
{
    public class AuthService
    {
        AuthRepo repo;
        Mapper mapper;

        public AuthService(AuthRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public EmployeeDTO Login(LoginDTO login)
        {
            var hashedPassword = PasswordHelper.GetMd5(login.Password);

            var data = repo.Login(login.Email, hashedPassword);

            if (data == null)
            {
                data = repo.Login(login.Email, login.Password);
            }

            if (data == null)
            {
                return null;
            }

            return mapper.Map<EmployeeDTO>(data);
        }
    }
}