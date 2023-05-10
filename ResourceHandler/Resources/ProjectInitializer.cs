using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHandler.Resources
{
    public static class ProjectInitializer
    {
        public static Config Config { get; set; } 

        public static void InitializeConfig()
        {
            using (var reader = new StreamReader(@"D:\C#_Files\TelegramBot\TelegramBot\ResourceHandler\Resources\Jsons\Config.json"))
            {
                if (reader != null)
                {
                    var configFile = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());

                    Config = new Config();


                    Config.CommandPrefix = configFile.CommandPrefix;
                    Config.API_KEY = configFile.API_KEY;
                }
            }
        }
    }
}
