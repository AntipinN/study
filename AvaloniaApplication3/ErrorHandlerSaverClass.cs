using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


internal static class ErrorHandlerSaverClass
{
    public static void HandleError(Exception e)
    {
        string day = DateTime.Now.DayOfYear.ToString();
        string mills = DateTime.Now.Millisecond.ToString();
        if (!Directory.Exists("ErrorFolder"))
        {
            Directory.CreateDirectory("ErrorFolder");
        }
        using (StreamWriter fs = new StreamWriter($"ErrorFolder/errorlog {day + " " + mills}.txt", false))
        {
            fs.Write(e.Message);
        }
        using (FileStream fs = new FileStream($"ErrorFolder/errorlog {day + " " + mills}.json", FileMode.OpenOrCreate))
        {
            List<string> list = new List<string>()
                            {
                                e.Message,
                                e.StackTrace,
                                e.InnerException?.ToString(),
                                e.Source,
                                e.TargetSite?.ToString(),
                                e.HResult.ToString()
                            };
            JsonSerializer.Serialize(fs, list);
        }
    }
}

