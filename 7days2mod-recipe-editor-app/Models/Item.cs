using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7days2mod_recipe_editor_app.Models
{
    public class ItemClassProperty
    {
        public string name { get; set; }
        public string value { get; set; }
        public string param1 { get; set; }
    }
    public class ItemProperty
    {
        public string name { get; set; }
        public string value { get; set; }
        public string param1 { get; set; }
        public string _class { get; set; }
        public ObservableCollection<ItemClassProperty> classproperties { get; set; }
    }

    public class Item
    {
        public int index { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public ObservableCollection<ItemProperty> properties { get; set; }
        public string image
        {
            //TODO: check for image exists.
            //TODO: check for custom icon in mods folder
            get
            {
                var icon = properties.LastOrDefault(p => p.name == "CustomIcon");
                if (icon != null)
                {
                    return "pack://siteoforigin:,,,/ItemIcons/" + icon.value + ".png";
                }
                return "pack://siteoforigin:,,,/ItemIcons/" + name + ".png";
            }

        }
    }
}
