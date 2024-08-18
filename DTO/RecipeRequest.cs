namespace kitchen_api.DTO
{
    public class RecipeRequest
    {
        public string DishName { get; set; }
        public string DishType { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public IFormFile Image { get; set; }
    }
}