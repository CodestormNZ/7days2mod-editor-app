using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7days2mod_recipe_editor_app.Models
{
    public class BlockClassProperty
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class BlockProperty
    {
        public string name { get; set; }
        public string value { get; set; }
        public string param1 { get; set; }
        public string _class { get; set; }
        public ObservableCollection<BlockClassProperty> classproperties { get; set; }
    }
    public class BlockDrop
    {
        public string name { get; set; }
        public string count { get; set; }
        public string prob { get; set; }
        public string stick_chance { get; set; }
        public string tool_category { get; set; }
        public string _event { get; set; }
    }

    public class Block
    {
        public int index { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public ObservableCollection<BlockProperty> properties { get; set; }
        public ObservableCollection<BlockDrop> drops { get; set; }
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
