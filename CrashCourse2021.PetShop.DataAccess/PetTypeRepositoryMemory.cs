using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;
using CrashCourse2021.PetShop.Domain.IRepositories;

namespace CrashCourse2021.PetShop.DataAccess
{
    public class PetTypeRepositoryMemory : IPetTypeRepository
    {
        private static List<PetType> _petTypesTable = new List<PetType>();
        private static int _id = 1;
        public PetType Add(PetType petType)
        {
            petType.Id = _id++;
            _petTypesTable.Add(petType);
            return petType;
        }

        public List<PetType> FindAll()
        {
            return _petTypesTable;
        }
    }
}