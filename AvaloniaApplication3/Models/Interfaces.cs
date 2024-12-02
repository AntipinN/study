using System.Collections.Generic;


namespace AvaloniaApplication3.Models
{
    public interface IEquasionGenerator
    {
        List<object> GenerateEquasion();
    }

    public interface ISolver
    {
        Fraction Solve(List<object> equasion);
    }
    public interface IOPZ
    {
        public List<object> CalculateOPZ(List<object> data_from_string);
    }

}
