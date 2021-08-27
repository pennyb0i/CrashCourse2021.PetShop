using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.IServices;
using CrashCourse2021.PetShop.Core.Models;
using CrashCourse2021.PetShop.Domain.IRepositories;

namespace CrashCourse2021.PetShop.Domain.Services
{
    public class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _repo;
        public PetTypeService(IPetTypeRepository repo)
        {
            _repo = repo;
        }
        public List<PetType> GetPetTypes()
        {
            return _repo.FindAll();
        }

        public PetType Add(PetType petType)
        {
            return _repo.Add(petType);
        }
        
        public PetType FindByName(string name)
        {
            return GetPetTypes().Find(type => type.Name == name);
        }

        public PetType FindById(int id)
        {
            return GetPetTypes().Find(type => type.Id == id);
        }
    }
}