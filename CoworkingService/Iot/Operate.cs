using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Iot
{
    public class Operate
    {

    }


    public class Instruments
    {

        public async Task<bool> AuthenticateAsync(string userName, string password)
        {
            string url = Constants.Domen+Constants.LoginUrl+ "?login=" + userName + "&password=" + password;
            var result = await ExecuteGetRequestAsync(url);
            return true;
        }


        public async Task<dynamic> ExecuteGetRequestAsync(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public dynamic ExecutePostRequest(string url, dynamic body)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;
            return response;
        }
    }
}
