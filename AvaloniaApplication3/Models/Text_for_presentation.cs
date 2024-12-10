﻿using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaApplication3.Models
{
    public class Text_for_presentation
    {
        public List<string> text = new List<string>();
        public Text_for_presentation() 
        {
            using (FileStream fs = new FileStream($"H:\\3 курс\\GitFolder\\study\\AvaloniaApplication3\\AllRes\\JSON_Instruction.json", FileMode.OpenOrCreate))
            {
                text = System.Text.Json.JsonSerializer.Deserialize<List<string>>(fs);
            }
        }
    }
}
