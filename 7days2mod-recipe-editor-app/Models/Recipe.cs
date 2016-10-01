using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7days2mod_recipe_editor_app.Models
{
    public class Ingredient
    {
        public string name { get; set; }
        public string count { get; set; }
        public string image
        {
            //TODO: check for image exists.
            //TODO: check for custom icon on block/item
            //TODO: check for custom icon in mods folder
            get { return "pack://siteoforigin:,,,/ItemIcons/" + name + ".png"; }
        }
    }
    public class Recipe
    {
        public int index { get; set; }
        public string name { get; set; }
        public string count { get; set; }
        public string craft_area { get; set; }
        public string craft_area_view
        {
            get
            {
                if (String.IsNullOrEmpty(craft_area))
                {
                    return "backpack";
                }
                return craft_area;
            }
            set
            {
                craft_area = value;
            }
        }
        public string craft_exp_gain { get; set; }
        public string craft_time { get; set; }
        public string craft_tool { get; set; }
        public bool material_based { get; set; }
        public string tooltip { get; set; }
        public ObservableCollection<Ingredient> ingredients { get; set; }
        public string image
        {
            //TODO: check for image exists.
            //TODO: check for custom icon on block/item
            //TODO: check for custom icon in mods folder
            get { return "pack://siteoforigin:,,,/ItemIcons/" + name + ".png"; }
        }
    }
}
