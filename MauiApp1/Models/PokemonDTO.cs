using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class PokemonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<PokedexEntry> Descriptions { get; set; } = new ObservableCollection<PokedexEntry>();
        public string Image { get; set; }
        public string Typing { get; set; }
        public List<string> Abilities { get; set; } = new List<string>();
        public string Generation { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SpATK { get; set; }
        public int SpDEF { get; set; }
        public int Speed { get; set; }
        
    }
}
