using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Car_Agency.Models
{
    public class Car
    {
        public int Id { get; set; }

        [DisplayName("Car Name")]
        [Required(ErrorMessage = "You have to provide a valid Name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters.")]
        public string CarName { get; set; }

        [DisplayName("Brand")]
        [Required(ErrorMessage = "You have to provide a valid Brand.")]
        [MinLength(2, ErrorMessage = "Brand mustn't be less than 2 characters.")]
        [MaxLength(10, ErrorMessage = "Brand mustn't exceed 10 characters.")]
        public string CarBrand { get; set; }

        [DisplayName("Car Model")]
        public DateTime CarModel { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("EngineCapacity")]
        public int EngineCapacity { get; set; }

        [DisplayName("Car Image")]
        [ValidateNever]
        public string ImagePath { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Select a valid Brand.")]
        [DisplayName("Brand")]
        public int BrandId { get; set; }

        [ValidateNever]
        public Brand Brand { get; set; }
    }
}
