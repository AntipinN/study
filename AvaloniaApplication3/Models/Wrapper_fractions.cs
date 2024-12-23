using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication3.Models
{
    public class Wrapper_fractions
    {
        public List<string> term = new List<string>();
        public Dictionary<string,Fraction> fraction_substitutions = new Dictionary<string,Fraction>();

        public Wrapper_fractions(List<string> term, Dictionary<string, Fraction> fraction_substitutions)
        {
            this.term = term;
            this.fraction_substitutions = fraction_substitutions;
        }
    }
}
