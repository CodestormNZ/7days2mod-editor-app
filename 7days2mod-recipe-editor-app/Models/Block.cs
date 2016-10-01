using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7days2mod_recipe_editor_app.Models
{
    public class Block
    {
        public int index { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string image
        {
            //TODO: check for image exists.
            //TODO: check for custom icon on block/item
            //TODO: check for custom icon in mods folder
            get { return "pack://siteoforigin:,,,/ItemIcons/" + name + ".png"; }
        }
    }
}
