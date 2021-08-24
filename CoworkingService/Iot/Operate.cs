using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Iot
{
    public class Operate
    {
        public async Task InitiateAsync()
        {
            Console.WriteLine("Enter your login");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            var instruments = new Instruments();
            if (await instruments.AuthenticateAsync(userName, password))
            {
                instruments.IfAuthenticated();
            }
            else
            {
                instruments.IfNotAuthenticated();
            }
            Console.ReadLine();
        }
    }


    public class Instruments
    {

        public async Task<bool> AuthenticateAsync(string userName, string password)
        {
            string url = Constants.Domen + Constants.LoginUrl + "?login=" + userName + "&password=" + password;

            string result = ExecuteGet(url);

            if (!String.IsNullOrEmpty(result))
            {
                Constants.UserEmail = userName;
                Constants.UserId = result;
                return true;
            }
            return false;
        }

        public void IfAuthenticated()
        {
            Console.WriteLine("List of your coworkings");

            int whichToView = 0;
            string url = Constants.Domen + Constants.CoworkingsUrl + "coworkings"+ "?userId=" + Constants.UserId;

            var response = ExecuteGet(url);

            var coworkings = (List<Coworking>)JsonSerializer.Deserialize(response,typeof(List<Coworking>));
            int suckDIck = 0;
        }

        public void IfNotAuthenticated()
        {
            Console.WriteLine("Wrong credantials or your account is not admin one");
        }

        public string ExecuteGet(string url)
        {
            string result = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

      
    }
}
