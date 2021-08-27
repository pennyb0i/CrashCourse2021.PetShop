using System.Collections.Generic;
using CrashCourse2021.PetShop.Core.Models;

namespace CrashCourse2021.PetShop.Domain.IRepositories
{
    public interface IPetTypeRepository
    {
        PetType Add(PetType petType);
        List<PetType> FindAll();
    }
}