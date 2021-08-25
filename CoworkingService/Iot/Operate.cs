using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
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

            int whichToView = 0;
            string url = Constants.Domen + Constants.CoworkingsUrl + "coworkings" + "?userId=" + Constants.UserId;

            var response = ExecuteGet(url);

            var coworkings = (List<Coworking>)JsonSerializer.Deserialize(response, typeof(List<Coworking>));
            if (coworkings.Any())
            {
                Console.WriteLine("List of your coworkings");
                ListOfCoworkings(coworkings);
            }
            else
            {
                Console.WriteLine("You currently don't have any coworkings");
            }
        }

        public void ListOfCoworkings(List<Coworking> coworkings)
        {
            Console.WriteLine("Enter the number of coworking, you want to set tracking on");
            for (int i = 0; i < coworkings.Count(); i++)
            {
                Console.WriteLine($"Coworking №{i} |  id - {coworkings[i].Id} | coworking title - {coworkings[i].Name} ");
            }
            int number = Int32.Parse(Console.ReadLine());
            if (number! < 0 && number > coworkings.Count())
            {
                Console.Clear();
                Console.WriteLine("Incorrect number");
                Console.ReadLine();
                ListOfCoworkings(coworkings);
            }
            else
            {
                SetTheWatchOnCoworking(coworkings[number]);
            }

        }

        public void SetTheWatchOnCoworking(Coworking coworking)
        {
            Console.Clear();
            int totalPeopleInCoworking = coworking.PeopleCurrentlyIn;
            string initialUrl = Constants.Domen + Constants.CoworkingsUrl + "countPeople" + "?coworkingId=" + coworking.Id + "&peopleToCome" + 0;
            int peopleToCome = Int32.Parse(ExecuteGet(initialUrl));
            while (true)
            {
                if (peopleToCome > 0)
                {
                    int people = new Random().Next(0, 100) % 2 == 0 ? 1 : -1;
                    string url = Constants.Domen + Constants.CoworkingsUrl + "countPeople" + "?coworkingId=" + coworking.Id + "&peopleToCome=" + people;
                    peopleToCome = Int32.Parse(ExecuteGet(url));

                    if (people > 0)
                        Console.WriteLine($"{people} person enters your coworking");
                    else
                        Console.WriteLine($"{people*-1} person walk out of your coworking");
                }
                else
                {
                    string url = Constants.Domen + Constants.CoworkingsUrl + "countPeople" + "?coworkingId=" + coworking.Id + "&peopleToCome=" + 1;
                    peopleToCome = Int32.Parse(ExecuteGet(url));
                    Console.WriteLine($"1 person enters your coworking");
                }
                Thread.Sleep(1000);
            }
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
