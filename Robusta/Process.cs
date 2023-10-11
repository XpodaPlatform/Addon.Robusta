using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Robusta
{

    public class ProcessStartRequest
    {
        public string runOnce { get; set; }
        public string runImmediately { get; set; }
        public string name { get; set; }
        public string worker { get; set; }
        public Dictionary<string, string> values { get; set; }
    }

    public class ProcessStartResponse
    {
        public string robustaSpid { get; set; }
        
        public string message { get; set; }
    }

    public class ProcessStatusResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    
    public class Process
    {
        public static string _robustaUsername = "devran.erogul";
        public static string _robustaPassword = "KKKKKKKKPassword";
       
        public static Dictionary<string, object> Start(List<Dictionary<string, object>> parameters)
        {
            var result = new Dictionary<string, object>();
            var List = new List<Dictionary<string, object>>();
            result["Error"] = "";


            using (var client = new HttpClient())
            {
                var uri = $"{parameters[0]["uri"].ToString()}/scheduler/connector-api/process/start";

                var req = new ProcessStartRequest
                {
                    name = parameters[2]["name"].ToString(),
                    runImmediately = "Y",
                    runOnce = "Y",
                    worker = parameters[1]["worker"].ToString()
                };

                // Authorization
                var authToken = Encoding.ASCII.GetBytes($"{_robustaUsername}:{_robustaPassword}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                req.values = JsonConvert.DeserializeObject<Dictionary<string, string>>( @"{" + parameters[3]["values"].ToString() + "}");

                var json = JsonConvert.SerializeObject(req);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = client.PostAsync(uri, data).GetAwaiter().GetResult();

                var s = JsonConvert.DeserializeObject<ProcessStartResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                
                List.Add(new Dictionary<string, object> { { "Result", s.robustaSpid } });
                
                result["List"] = List;

            }

            return result;
        }

        public static Dictionary<string, object> Status(List<Dictionary<string, object>> parameters)
        {
            var result = new Dictionary<string, object>();
            var List = new List<Dictionary<string, object>>();
            result["Error"] = "";


            using (var client = new HttpClient())
            {
                var id = parameters[1]["id"].ToString();
                var uri = $"{parameters[0]["uri"].ToString()}/scheduler/connector-api/process/status/{id}";

                // Authorization
                var authToken = Encoding.ASCII.GetBytes($"{_robustaUsername}:{_robustaPassword}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
           
                var response = client.GetAsync(uri).GetAwaiter().GetResult();

                var s = JsonConvert.DeserializeObject<ProcessStatusResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());

                List.Add(new Dictionary<string, object> { { "Result", s.message } });

                result["List"] = List;
            }

            return result;

        }
    }
}