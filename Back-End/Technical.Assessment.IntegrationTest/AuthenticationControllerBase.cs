using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Technical.Assessment.IntegrationTest.Builder;
using Technical.Assessment.IntegrationTest.Server;

namespace Technical.Assessment.IntegrationTest
{
    public abstract class AuthenticationControllerBase
    {
        private TestServer server;
        private HttpClient client;
        private UserDtoBuilder builder;

        public void AuthenticationInit()
        {
            server = ServerInit.GetServer();
            client = server.CreateClient();
            builder = new UserDtoBuilder();
        }

        public string GetToken()
        {
            string url = "api/Authentication";
            var json = JsonConvert.SerializeObject(builder.Get());
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = Task.Run(async () => await client.PostAsync(url, stringContent));
            response.Result.EnsureSuccessStatusCode();

            var content = response.Result.Content.ReadAsStringAsync().Result;
            TokeDto tokenDto = JsonConvert.DeserializeObject<TokeDto>(content);

            return tokenDto.Token;
        }
    }
}
