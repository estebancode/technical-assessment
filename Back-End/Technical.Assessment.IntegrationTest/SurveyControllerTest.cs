using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Technical.Assessment.Api.Dto;
using Technical.Assessment.IntegrationTest.Builder;
using Technical.Assessment.IntegrationTest.Server;

namespace Technical.Assessment.IntegrationTest
{
    [TestClass]
    public class SurveyControllerTest : AuthenticationControllerBase
    {

        private TestServer _server;
        private HttpClient _client;
        const string URL = "api/Survey";
        SurveyDtoBuilder builder;

        [TestInitialize]
        public void Initialize()
        {
            _server = ServerInit.GetServer();
            _client = _server.CreateClient();
            builder = new SurveyDtoBuilder();
            AuthenticationInit();
            string token = GetToken();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        [TestMethod]
        public void Get_All()
        {
            //Act
            var response = Task.Run(async () => await _client.GetAsync(URL));
            response.Result.EnsureSuccessStatusCode();

            var content = response.Result.Content.ReadAsStringAsync().Result;
            IEnumerable<SurveyDto> surveyDtos = JsonConvert.DeserializeObject<IEnumerable<SurveyDto>>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.IsTrue(surveyDtos.Any());
        }

        [TestMethod]
        public void Create()
        {

            //Arrange

            var json = JsonConvert.SerializeObject(builder.Get());
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = Task.Run(async () => await _client.PostAsync(URL, stringContent));
            response.Result.EnsureSuccessStatusCode();

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
