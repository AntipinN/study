using System.Collections.Generic;
using System.IO;


namespace AvaloniaApplication3.Models
{
    public class Text_for_presentation
    {
        public List<string> text = new List<string>();
        public List<List<string>> view = new List<List<string>>();
        public Text_for_presentation() 
        {
            using (FileStream fs = new FileStream($"AllRes/JSON_Instruction.json", FileMode.OpenOrCreate))
            {
                text = System.Text.Json.JsonSerializer.Deserialize<List<string>>(fs);
            }
            using (FileStream fs = new FileStream($"AllRes/FRACTIONS.json", FileMode.OpenOrCreate))
            {
                view = System.Text.Json.JsonSerializer.Deserialize<List<List<string>>>(fs);
            }

        }
    }
}
