using System.ComponentModel.DataAnnotations;

namespace kitchen_api.Database.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishType { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public byte[] ImageData { get; set; }  // Image stored as byte[] in the database
    }
}
