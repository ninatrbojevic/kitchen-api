namespace kitchen_api.DTO
{
    public class UpdateRecipeRequest
    {
        public string DishName { get; set; }
        public string DishType { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public IFormFile? ImageData { get; set; }
    }
}