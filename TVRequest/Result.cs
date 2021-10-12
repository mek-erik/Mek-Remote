using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace TVRequest
{
    class Result
    {
        private string type = "notice";

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int statusCode = 200;
        [JsonPropertyName("statusCode")]
        public int StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }
        private string description;
        [JsonPropertyName("description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
       
        public static string GetJSON(Result result)
        {
            return JsonSerializer.Serialize(result);
        }
        public bool ShouldSerializeType()
        {
            return false;
        }

    }
}
