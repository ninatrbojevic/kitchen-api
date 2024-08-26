using kitchen_api.Database;
using kitchen_api.Database.Entities;
using kitchen_api.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kitchen_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private RecipeContext _context;

        public RecipesController(RecipeContext context)
        {
            _context = context;
        }

        // GET api/recipes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recipes = await _context.Recipes.ToListAsync();

            if (recipes.Count == 0)
                return Ok(new List<Recipe>());

            return Ok(recipes);
        }

        // GET api/recipes/3
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == id);

            if (recipe is null)
                return NotFound();

            return Ok(recipe);
        }

        // POST api/recipes
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RecipeRequest recipeRequest)
        {
            var newRecipe = new Recipe
            {
                DishType = recipeRequest.DishType,
                DishName = recipeRequest.DishName,
                Ingredients = recipeRequest.Ingredients,
                Preparation = recipeRequest.Preparation
            };

            if (recipeRequest.ImageData != null)
            {
                using var memoryStream = new MemoryStream();

                await recipeRequest.ImageData.CopyToAsync(memoryStream);

                newRecipe.ImageData = memoryStream.ToArray();
            }

            _context.Recipes.Add(newRecipe);

            var result = await _context.SaveChangesAsync();

            if (result is not 1)
                return BadRequest(result.ToString());

            return Ok();
        }


        // PUT api/recipes/3
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id,
            [FromForm] UpdateRecipeRequest recipeRequest)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == id);

            if (recipe is null)
                return NotFound();

            recipe.DishType = recipeRequest.DishType;
            recipe.DishName = recipeRequest.DishName;
            recipe.Ingredients = recipeRequest.Ingredients;
            recipe.Preparation = recipeRequest.Preparation;

            if (recipeRequest.ImageData != null)
            {
                using var memoryStream = new MemoryStream();

                await recipeRequest.ImageData.CopyToAsync(memoryStream);

                recipe.ImageData = memoryStream.ToArray();
            }

            _context.Recipes.Update(recipe);

            var result = await _context.SaveChangesAsync();

            if (result is not 1)
                return BadRequest(result.ToString());

            return Ok();

        }

        // DELETE api/recipes/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(x => x.Id == id);

            if (recipe is null)
                return NotFound();

            _context.Recipes.Remove(recipe);

            var result = await _context.SaveChangesAsync();

            if (result is not 1)
                return BadRequest(result.ToString());

            return Ok();
        }
    }
}
