using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            startProcess();
            //queryProcess();
        }

        static void startProcess()
        {

            // Assembly path
            var assembly = Assembly.LoadFrom(@"C:\Users\DevranErogul\source\repos\Robusta\Robusta\bin\Debug\Robusta.dll");

            // Namespace ve Class
            var type = assembly.GetType("Robusta.Process");
            var method = type.GetMethod("Start");
            var paramss = new object[1];
            var paramlist = new List<Dictionary<string, object>>();

            // Robusta host url
            paramlist.Add(new Dictionary<string, object>
            {
                {"uri", "https://prod.robusta.ai:8443"},
            });

            paramlist.Add(new Dictionary<string, object>
            {
                {"worker", "afd54558-8ebd-4655-97a7-5afb808ebc68" },
            });
            paramlist.Add(new Dictionary<string, object>
            {
                {"name", "xpodatest2" },
            });
            paramlist.Add(new Dictionary<string, object>
            {
                {"values", @"{""URL"": ""https://partner.xpodacloud.com/"",""UserName"": ""DevranE"",""Password"" : ""DevranE""}" },
            });

            paramss[0] = paramlist;

            var obj = Activator.CreateInstance(type);

            // Metodu cagiriyoruz
            var rs = method.Invoke(obj, paramss) as Dictionary<string, object>;

            List<Dictionary<string, object>> x = (List<Dictionary<string, object>>)rs["List"];

            foreach (var item in x)
            {
                Console.Write(item["Result"].ToString());
            }


            Console.ReadKey();
        }

        static void queryProcess()
        {
            // Assembly path
            var assembly = Assembly.LoadFrom(@"C:\Users\DevranErogul\source\repos\Robusta\Robusta\bin\Debug\Robusta.dll");

            // Namespace ve Class
            var type = assembly.GetType("Robusta.Process");
            var method = type.GetMethod("Status");
            var paramss = new object[1];
            var paramlist = new List<Dictionary<string, object>>();

            // Robusta host url
            paramlist.Add(new Dictionary<string, object>
            {
                {"uri", "https://prod.robusta.ai:8443"},
            });

            paramlist.Add(new Dictionary<string, object>
            {
                {"id", "4ac04650-23cb-41e9-99fc-202a34136b86" },
            });

         
            paramss[0] = paramlist;

            var obj = Activator.CreateInstance(type);

            // Metodu cagiriyoruz
            var rs = method.Invoke(obj, paramss) as Dictionary<string, object>;

            List<Dictionary<string, object>> x = (List<Dictionary<string, object>>)rs["List"];

            foreach (var item in x)
            {
                Console.Write(item["Result"].ToString());
            }

            Console.ReadKey();

        }
    }
}
