using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;

namespace CrashCourse2021.PetShop.Core.IServices
{
    public interface IPetService
    {
        List<Pet> GetPets();

        Pet Add(Pet pet);
        Pet Delete(Pet pet);
        Pet FindById(int id);
    }
}