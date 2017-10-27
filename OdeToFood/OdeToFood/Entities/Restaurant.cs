using System.ComponentModel.DataAnnotations;
namespace OdeToFood.Entities
{

    public enum CuisineType
    {
        None,
        Italian,
        Brazilian,
        Japanese
    }

    public class Restaurant
    {
        public int Id { get; set; }

        [Required,MaxLength(20)]
        [Display(Name = "Trade Name")]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
