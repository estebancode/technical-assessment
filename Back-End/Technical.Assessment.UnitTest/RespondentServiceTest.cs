
#region Usings

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;
using Technical.Assessment.Domain.Services;
using Technical.Assessment.UnitTest.Builder;

#endregion

namespace Technical.Assessment.UnitTest
{
    [TestClass]
    public class RespondentServiceTest
    {
        IRepository<Respondent> _Repository;
        IRespondentService _RespondentService;
        RespondentBuilder builder;

        [TestInitialize]
        public void Initialize()
        {
            _Repository = Substitute.For<IRepository<Respondent>>();
            _RespondentService = new RespondentService(_Repository);
            builder = new RespondentBuilder();
        }

        [TestMethod]
        public async Task Create_Respondent_Async()
        {
            int counter = 0;
            int expectedResult = 2;
            _Repository.GetAllAsync(null,null,null,false).ReturnsForAnyArgs(new List<Respondent>());
            _Repository.When(c => c.Insert(Arg.Any<Respondent>())).Do(o => counter++);
            _Repository.When(c => c.SaveChangesAsync()).Do(o => counter++);
            await _RespondentService.InsertAsync(builder.Get());
            Assert.AreEqual(expectedResult, counter);
        }

        [TestMethod]
        public async Task Respondent_is_already_exist_Async()
        {
            string expectedResult = "Username is already exist";
            _Repository.GetAllAsync(null, null, null, false).ReturnsForAnyArgs(new List<Respondent> { builder.Get() });

            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => 
            _RespondentService.InsertAsync(builder.Get())
            ).ConfigureAwait(false);

            Assert.AreEqual(expectedResult, ex.Message);
        }

        [TestMethod]
        public async Task Authenticate_user_Async()
        {
            Respondent entity = builder.Get();
            Respondent respondent = new Respondent 
            {
                Name = entity.Name,
                Email = entity.Email,
                HashedPassword = entity.HashedPassword
            };
            respondent.HashedPassword = ComputeSha256Hash(respondent.HashedPassword);
            _Repository.GetAllAsync(c => c.Email == entity.Email, null, null, false)
                .ReturnsForAnyArgs(new List<Respondent> { respondent });

            string token = await _RespondentService.AuthenticaUserAsync(entity);

            Assert.IsNotNull(token);
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
