namespace CrashCourse2021.PetShop.Core.Models
{
    public class PetType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}