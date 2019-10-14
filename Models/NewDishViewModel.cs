using System.Collections.Generic;

namespace ChefsAndDishes.Models
{
    public class NewDishViewModel
    {
        public Dish New_Dish {get;set;}
        public List<Chef> AllChefs {get;set;}
    }
}