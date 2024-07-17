using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Car_Agency.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [DisplayName("Brand Name")]
        [Required(ErrorMessage = "You have to provide a valid Name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters.")]
        public string BrandName { get; set; }

        [DisplayName("Brand Start Date")]
        public DateTime BrandStartDate { get; set; }

        [DisplayName("Brand Description")]
        [Required(ErrorMessage = "You have to provide a valid Description.")]
        [MinLength(20, ErrorMessage = "Description mustn't be less than 20 characters.")]
        [MaxLength(400, ErrorMessage = "Description mustn't exceed 200 characters.")]
        public string BrandDescription { get; set;}

        [DisplayName("Brand Image")]
        [ValidateNever]
        public string ImagePath { get; set; }

        [ValidateNever]
        public List<Car> Cars { get; set; }
    }
}
