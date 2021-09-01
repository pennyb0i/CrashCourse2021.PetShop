using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.IServices;
using CrashCourse2021.PetShop.Core.Models;
using CrashCourse2021.PetShop.Domain.IRepositories;

namespace CrashCourse2021.PetShop.Domain.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _repo;
        public PetService(IPetRepository repo)
        {
            _repo = repo;
        }
        public List<Pet> GetPets()
        {
            return _repo.FindAll();
        }

        public Pet Add(Pet pet)
        {
            _repo.Add(pet);
            return pet;
        }
        
        public Pet Delete(Pet pet)
        {
            _repo.Delete(pet);
            return pet;
        }

        public Pet FindById(int id)
        {
            return GetPets().Find(pet => pet.Id == id);
        }
    }
}