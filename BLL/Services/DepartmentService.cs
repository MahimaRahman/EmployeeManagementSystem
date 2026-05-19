using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class DepartmentService
    {
        DepartmentRepo repo;
        Mapper mapper;

        public DepartmentService(DepartmentRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }



        public bool Create(DepartmentDTO d)
        {
            var data = mapper.Map<Department>(d);
            return repo.Create(data);
        }


        public List<DepartmentDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<DepartmentDTO>>(data);
        }

        public DepartmentDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<DepartmentDTO>(data);
        }


        public bool Update(DepartmentDTO d)
        {
            var data = mapper.Map<Department>(d);
            return repo.Update(data);
        }


        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}