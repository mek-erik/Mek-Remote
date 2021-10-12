using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TVRequester.Responses
{
    public class GeneralResponse
    {



 //       [JsonPropertyName("type")]
        public string Type;
 //       [JsonPropertyName("statusCode")]
        public int StatusCode;
//        [JsonPropertyName("description")]
        public string Description;

        public static  GeneralResponse GetGeneralResponse(string text)
        {
            //GeneralResponse gr = JsonSerializer.Deserialize<GeneralResponse>(text);
            
            GeneralResponse gr = JsonConvert.DeserializeObject<GeneralResponse>(text);
            return gr;
        }
    }

    
}
