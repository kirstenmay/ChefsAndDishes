using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsAndDishes.Models
{
    public class Dish
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int DishId { get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,5)]
        public int Tastiness { get; set; }
        [Required]
        [Range(0,10000)]
        public int Calories { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string Description {get;set;}
        // The MySQL DATETIME type can be represented by a DateTimecopy
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        [ForeignKey("ChefForeignKey")]
        public int ChefId {get;set;}
        public Chef Creator {get;set;}
    }
}