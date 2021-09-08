using System;
using System.Linq;
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
            var pets = _petService.GetPets().OrderByDescending(pet => pet.Price);
            
            Console.WriteLine("Available pets,sorted by price:");
            foreach (Pet pet in pets)
            {
                Console.WriteLine(pet.ToString());
            }
        }

        private static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(StringConstants.PauseLine);
            Console.ResetColor();
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
                        var pet = CreateNewPet();
                        if (pet != null)
                        {
                            _petService.Add(pet);
                        }
                        break;
                    case 2:
                        _petTypeService.Add(CreateNewPetType());
                        break;
                    case 3:
                        PrintAllPets();
                        break;
                    case 4:
                        PrintFiveCheapestPets();
                        break;
                    case 5:
                        PrintPetsByType();
                        break;
                    case 6:
                        EditPet();
                        break;
                    case 7:
                        _petService.Delete(DeletePet());
                        break;
                }
                Pause();
            } while (selection != 0);
        }

        private void PrintFiveCheapestPets()
        {
            var pets = _petService.GetPets().OrderBy(pet => pet.Price);
            
            Console.WriteLine("5 cheapest pets available:");
            foreach (var pet in pets.Take(5))
            {
                Console.WriteLine(pet.ToString());
            }
        }

        private void PrintPetsByType()
        {
            Console.WriteLine(StringConstants.PetTypeLine);
            PetType petType = SelectPetType();
            
            var pets = _petService.GetPets();
            
            Console.WriteLine($"Available {petType.Name} pets:");
            foreach (Pet pet in pets)
            {
                if (pet.Type == petType)
                {
                    Console.WriteLine(pet.ToString());
                }
            }
        }

        private Pet EditPet()
        {
            int selection;
            do
            {
                Console.Clear();
                Console.WriteLine("Select the number of pet to edit:");
                PrintAllPets();
                Console.WriteLine(">0: GO BACK<");

                var selectedOption = Console.ReadKey(true);
                selection = selectedOption.KeyChar - '0';
            } while (selection < 0 || selection > _petService.GetPets().Count);

            if (selection == 0) return null;
            var petToEdit = _petService.FindById(selection);
            
            Console.WriteLine(StringConstants.EditPetGreeting);
            Console.WriteLine(StringConstants.PetTypeLine);
            var type = SelectPetType();
            if (type == null)
            {
                return null;
            }
            petToEdit.Type = type;
            Console.WriteLine(StringConstants.PetNameLine);
            petToEdit.Name = Console.ReadLine();
            Console.WriteLine(StringConstants.PetColorLine);
            petToEdit.Color = Console.ReadLine();
            Console.WriteLine(StringConstants.PetBirthDateLine);
            petToEdit.BirthDate = GetBirthDate();
            Console.WriteLine(StringConstants.PetPriceLine);
            petToEdit.Price = int.Parse(Console.ReadLine() ?? throw new ArgumentException("Invalid number"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{petToEdit.Name} successfully edited.");
            Console.ResetColor();
            return petToEdit;
        }

        private void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine(StringConstants.WelcomeGreeting);
            Console.WriteLine(StringConstants.PleaseSelectMain);
            Console.WriteLine(StringConstants.AddPetMenuText);
            Console.WriteLine(StringConstants.AddPetTypeMenuText);
            Console.WriteLine(StringConstants.ShowAllPetsMenuText);
            Console.WriteLine(StringConstants.ShowFiveCheapestPetsMenuText);
            Console.WriteLine(StringConstants.ShowPetsByTypeMenuText);
            Console.WriteLine(StringConstants.EditPetMenuText);
            Console.WriteLine(StringConstants.DeletePetMenuText);

            Console.WriteLine(StringConstants.ExitMenuText);
        }
        
        private int GetMainMenuSelection()
        {
            int selection;
            do
            {
                ShowOptions();
                var selectedOption = Console.ReadKey(true);
                selection = selectedOption.KeyChar - '0';
                if (selection < 0 || selection > 7)
                {
                    Console.WriteLine(StringConstants.PleaseSelectCorrectItem);
                }
            } while (selection < 0 || selection > 7);
            return selection;
        }

        private PetType CreateNewPetType()
        {
            PetType newPetType = new PetType();
            Console.WriteLine(StringConstants.AddPetTypeGreeting);
            Console.WriteLine(StringConstants.PetTypeNameLine);
            newPetType.Name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{newPetType.Name} successfully added.");
            Console.ResetColor();
            return newPetType;
        }
        
        private Pet CreateNewPet()
        {
            Pet newPet = new Pet();
            Console.WriteLine(StringConstants.AddPetGreeting);
            Console.WriteLine(StringConstants.PetTypeLine);
            var type = SelectPetType();
            if (type == null)
            {
                return null;
            }
            newPet.Type = type;
            Console.WriteLine(StringConstants.PetNameLine);
            newPet.Name = Console.ReadLine();
            Console.WriteLine(StringConstants.PetColorLine);
            newPet.Color = Console.ReadLine();
            Console.WriteLine(StringConstants.PetBirthDateLine);
            newPet.BirthDate = GetBirthDate();
            Console.WriteLine(StringConstants.PetPriceLine);
            newPet.Price = int.Parse(Console.ReadLine() ?? throw new ArgumentException("Invalid number"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{newPet.Name} successfully added.");
            Console.ResetColor();
            return newPet;
        }

        private PetType SelectPetType()
        {
            int selection;
            do
            {
                _petTypeService.GetPetTypes().ForEach(type => Console.WriteLine(type.ToString()));
                Console.WriteLine(">0:CANCEL<");
                var selectedOption = Console.ReadKey(true);
                
                selection = selectedOption.KeyChar - '0';
                if (selection < 0 || selection > _petTypeService.GetPetTypes().Count)
                {
                    Console.WriteLine($"Please select an option between 0 - {_petTypeService.GetPetTypes().Count}");
                }
            } while (selection < 0 || selection > _petTypeService.GetPetTypes().Count);

            return selection == 0 ? null : _petTypeService.FindById(selection);
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

                var selectedOption = Console.ReadKey(true);
                selection = selectedOption.KeyChar - '0';
            } while (selection < 0 || selection > _petService.GetPets().Count);

            if (selection == 0) return null;
            var petToDelete = _petService.FindById(selection);
            _petService.Delete(petToDelete);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{petToDelete.Name} was sent to the butcher.");
            Console.ResetColor();
            return petToDelete;
        }
    }
}