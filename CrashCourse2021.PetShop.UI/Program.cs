using CrashCourse2021.PetShop.DataAccess;
using CrashCourse2021.PetShop.Domain.Services;

namespace CrashCourse2021.PetShop.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var petRepo = new PetRepositoryMemory();
            var petService = new PetService(petRepo);
            
            var petTypeRepo = new PetTypeRepositoryMemory();
            var petTypeService = new PetTypeService(petTypeRepo);
            var printer = new HpOfficeJetPro7740(petService, petTypeService);
            printer.Start();
        }
    }
}