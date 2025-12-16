using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.API
{
    public class ApiCorrections
    {
        public static string CapitalizeFirstLetter(string name)
        {
            var noFirstLetter = name.Substring(1);
            var onlyFirstLetter = name.Substring(0, 1);
            var capitalize = onlyFirstLetter.ToUpper();
            var capitalizedName = capitalize + noFirstLetter;
            return capitalizedName;
        }

        internal static string FuseTypesIntoString(List<string> fusedTyping)
        {
            if (fusedTyping.Count > 1)
            {
                var type1 = fusedTyping[0];
                var type2 = fusedTyping[1];
                var fused = $"{type1} / {type2}";
                return fused;
            }
            else { return fusedTyping[0]; }                
        }

        internal static string RemoveRowChange(string flavorText)
        {
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            string edited = flavorText
                .Replace("\n", " ")
                .Replace("\f", " ")
                .Replace("\r", " ")
                .Replace(lineSeparator, string.Empty)
                .Replace(paragraphSeparator, string.Empty);
            return edited;
        }
    }
}
