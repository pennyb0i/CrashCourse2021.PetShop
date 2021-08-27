using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;

namespace CrashCourse2021.PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        List<PetType> GetPetTypes();
        PetType FindByName(string name);
        PetType FindById(int id);
        PetType Add(PetType petType);
    }
}