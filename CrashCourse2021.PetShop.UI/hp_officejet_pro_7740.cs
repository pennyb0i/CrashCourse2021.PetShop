using System;
using CrashCourse2021.PetShop.Core.IServices;
using CrashCourse2021.PetShop.Core.Models;

namespace CrashCourse2021.PetShop.UI
{
    public class HpOfficeJetPro7740
    {
        private IPetService _petService;
        private IPetTypeService _petTypeService;
        
        public HpOfficeJetPro7740(IPetService petService,IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }

        public void PrintAllPets()
        {
            var pets = _petService.GetPets();
            
            Console.WriteLine("Available pets:");
            foreach (Pet pet in pets)
            {
                Console.WriteLine(pet.ToString());
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public void Start()
        {
            int selection;
            do
            {
                selection = GetMainMenuSelection();
                switch (selection)
                {
                    case 1:
                        _petService.Add(CreateNewPet());
                        break;
                    case 2:
                        _petTypeService.Add(CreateNewPetType());
                        break;
                    case 3:
                        PrintAllPets();
                        break;
                    case 4:
                        _petService.Delete(DeletePet());
                        break;
                }
                Pause();
            } while (selection != 0);
        }

        private void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine(StringConstants.WelcomeGreeting);
            Console.WriteLine(StringConstants.PleaseSelectMain);
            Console.WriteLine(StringConstants.AddPetMenuText);
            Console.WriteLine(StringConstants.AddPetTypeMenuText);
            Console.WriteLine(StringConstants.ShowAllPetsMenuText);
            Console.WriteLine(StringConstants.DeletePetMenuText);

            Console.WriteLine(StringConstants.ExitMenuText);
        }
        
        private int GetMainMenuSelection()
        {
            int selection;
            do
            {
                ShowOptions();
                var selectedOption = Console.ReadKey();
                selection = selectedOption.KeyChar - '0';
                if (selection < 0 || selection > 4)
                {
                    Console.WriteLine(StringConstants.PleaseSelectCorrectItem);
                }
            } while (selection < 0 || selection > 4);
            return selection;
        }

        private PetType CreateNewPetType()
        {
            PetType newPetType = new PetType();
            Console.WriteLine(StringConstants.AddPetTypeGreeting);
            Console.WriteLine(StringConstants.PetTypeNameLine);
            newPetType.Name = Console.ReadLine();
            Console.WriteLine($"{newPetType.Name} successfully added.");
            return newPetType;
        }
        
        private Pet CreateNewPet()
        {
            Pet newPet = new Pet();
            Console.WriteLine(StringConstants.AddPetGreeting);
            Console.WriteLine(StringConstants.PetTypeLine);
            newPet.Type = SelectPetType();
            Console.WriteLine(StringConstants.PetNameLine);
            newPet.Name = Console.ReadLine();
            Console.WriteLine(StringConstants.PetColorLine);
            newPet.Color = Console.ReadLine();
            Console.WriteLine(StringConstants.PetBirthDateLine);
            newPet.BirthDate = GetBirthDate();
            Console.WriteLine(StringConstants.PetPriceLine);
            newPet.Price = int.Parse(Console.ReadLine() ?? throw new ArgumentException("Invalid number"));
            Console.WriteLine($"{newPet.Name} successfully added.");
            return newPet;
        }

        private PetType SelectPetType()
        {
            int selection;
            do
            {
                _petTypeService.GetPetTypes().ForEach(type => Console.WriteLine(type.ToString()));
                var selectedOption = Console.ReadKey();
                
                selection = selectedOption.KeyChar - '0';
                if (selection < 0 || selection > _petTypeService.GetPetTypes().Count)
                {
                    Console.WriteLine($"Please select an option between 0 - {_petTypeService.GetPetTypes().Count}");
                }
            } while (selection < 0 || selection > _petTypeService.GetPetTypes().Count);
            return _petTypeService.FindById(selection);
        }

        private DateTime GetBirthDate()
        {
            //Simple implementation
            return DateTime.Now;
        }
        private Pet DeletePet()
        {
            int selection;
            do
            {
                Console.Clear();
                Console.WriteLine("Select the number of pet to delete:");
                PrintAllPets();
                Console.WriteLine(">0: GO BACK<");

                var selectedOption = Console.ReadKey();
                selection = selectedOption.KeyChar - '0';
            } while (selection < 0 || selection > _petService.GetPets().Count);

            if (selection == 0) return null;
            var petToDelete = _petService.FindById(selection);
            _petService.Delete(petToDelete);
            Console.WriteLine($"\n{petToDelete.Name} was sent to the butcher.");
            return petToDelete;
        }
    }
}