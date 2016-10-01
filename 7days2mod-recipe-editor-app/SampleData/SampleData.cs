using _7days2mod_recipe_editor_app.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7days2mod_recipe_editor_app.SampleData
{
    class SampleData
    {
        private ObservableCollection<Recipe> loadSampleRecipes()
        {
            ObservableCollection<Recipe> recipeData = new ObservableCollection<Recipe>();

            ObservableCollection<Ingredient> ing1 = new ObservableCollection<Ingredient>();
            ing1.Add(new Ingredient() { name = "bulletTip", count = "1" });
            ing1.Add(new Ingredient() { name = "gunPowder", count = "1" });
            ing1.Add(new Ingredient() { name = "bulletCasing", count = "1" });

            ObservableCollection<Ingredient> ing2 = new ObservableCollection<Ingredient>();
            ing2.Add(new Ingredient() { name = "bulletTip", count = "1" });
            ing2.Add(new Ingredient() { name = "gunPowder", count = "2" });
            ing2.Add(new Ingredient() { name = "bulletCasing", count = "1" });

            ObservableCollection<Ingredient> ing3 = new ObservableCollection<Ingredient>();
            ing3.Add(new Ingredient() { name = "bulletTip", count = "1" });
            ing3.Add(new Ingredient() { name = "gunPowder", count = "3" });
            ing3.Add(new Ingredient() { name = "bulletCasing", count = "1" });

            ObservableCollection<Ingredient> ing4 = new ObservableCollection<Ingredient>();
            ing4.Add(new Ingredient() { name = "moldyBread", count = "4" });
            ing4.Add(new Ingredient() { name = "potassiumNitratePowder", count = "3" });
            ing4.Add(new Ingredient() { name = "bottledWater", count = "1" });

            ObservableCollection<Ingredient> ing5 = new ObservableCollection<Ingredient>();
            ing5.Add(new Ingredient() { name = "rockSmall", count = "1" });
            ing5.Add(new Ingredient() { name = "gunPowder", count = "1" });
            ing5.Add(new Ingredient() { name = "paper", count = "1" });

            recipeData.Add(new Recipe()
            {
                index = 1,
                name = "9mmBullet",
                count = "1",
                craft_area = "workbench",
                ingredients = ing1
            });

            recipeData.Add(new Recipe()
            {
                index = 2,
                name = "10mmBullet",
                count = "1",
                craft_area = "workbench",
                ingredients = ing2
            });

            recipeData.Add(new Recipe()
            {
                index = 3,
                name = "44MagBullet",
                count = "1",
                craft_area = "workbench",
                ingredients = ing3
            });

            recipeData.Add(new Recipe()
            {
                index = 4,
                name = "762mmBullet",
                count = "1",
                craft_area = "workbench",
                ingredients = ing3
            });

            recipeData.Add(new Recipe()
            {
                index = 4,
                name = "antibiotics",
                count = "1",
                craft_area = "campfire",
                craft_tool = "beaker",
                ingredients = ing4
            });

            recipeData.Add(new Recipe()
            {
                index = 5,
                name = "blunderbussAmmo",
                count = "1",
                craft_area = "",
                craft_tool = "",
                ingredients = ing5
            });
            return recipeData;
        }

    }
}
