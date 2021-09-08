using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;

namespace CrashCourse2021.PetShop.Domain.IRepositories
{
    public interface IPetRepository
    {
        List<Pet> FindAll();
        Pet Add(Pet pet);
        //Pet Edit(Pet pet);
        Pet Delete(Pet pet);
    }
}