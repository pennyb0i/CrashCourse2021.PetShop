using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;
using CrashCourse2021.PetShop.Domain.IRepositories;

namespace CrashCourse2021.PetShop.DataAccess
{
    public class PetRepositoryMemory : IPetRepository
    {
        private static List<Pet> _petsTable = new();
        private static int _id = 1;
        
        public Pet Add(Pet pet)
        {
            pet.Id = _id++;
            _petsTable.Add(pet);
            return pet;
        }

        public Pet Delete(Pet pet)
        {
            _petsTable.Remove(pet);
            return pet;
        }

        public List<Pet> FindAll()
        {
            return _petsTable;
        }
    }
}