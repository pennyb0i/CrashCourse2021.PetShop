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

        public void Start()
        {
            int selection;
            do
            {
                selection = GetMainMenuSelection();
                switch (selection)
                {
                    case 1:
                        _petService.Add()
                    case 2:
                        _petTypeService.Add(CreateNewPetType());
                        break;
                }
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

            Console.WriteLine(StringConstants.ExitMenuText);
        }
        
        private int GetMainMenuSelection()
        {
            var selectedOption = Console.ReadKey();
            int selection;
            do
            {
                ShowOptions();
                selection = selectedOption.KeyChar - '0';
                if (selection < 0 || selection > 3)
                {
                    Console.WriteLine(StringConstants.PleaseSelectCorrectItem);
                }
            } while (selection < 0 || selection > 3);
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
            newPet.Color = Console.ReadLine();
            Console.WriteLine($"{newPet.Name} successfully added.");
            return newPet;
        }

        private PetType SelectPetType()
        {
            var selectedOption = Console.ReadKey();
            int selection;
            do
            {
                _petTypeService.GetPetTypes().ForEach(type => Console.Write(type.ToString()));
                
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
            
        }
    }
}