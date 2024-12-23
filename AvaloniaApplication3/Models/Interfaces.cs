using System.Collections.Generic;


namespace AvaloniaApplication3.Models
{
    public interface IEquasionGenerator
    {
        Wrapper_fractions GenerateEquasion();
    }

    public interface ISolver
    {
        Fraction Solve(List<string> OPS, Dictionary<string, Fraction> fraction_substitutions);
    }
    public interface IOPZ
    {
        public List<string> CalculateOPZ(List<string> data_from_string);
    }

}
