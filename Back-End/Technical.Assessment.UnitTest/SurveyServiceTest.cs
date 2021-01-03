
#region Usings

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;
using Technical.Assessment.Domain.Services;
using Technical.Assessment.UnitTest.Builder;

#endregion

namespace Technical.Assessment.UnitTest
{
    [TestClass]
    public class SurveyServiceTest
    {
        IRepository<Survey> _Repository;
        ISurveyService _SurveyService;
        SurverBuilder builder;

        [TestInitialize]
        public void Initialize()
        {
            _Repository = Substitute.For<IRepository<Survey>>();
            _SurveyService = new SurveyService(_Repository);
            builder = new SurverBuilder();
        }

        [TestMethod]
        public async Task Create_Survey_Async()
        {
            int counter = 0;
            int expectedResult = 2;
            _Repository.GetAllAsync(null, null, null, false).ReturnsForAnyArgs(new List<Survey>());
            _Repository.When(c => c.Insert(Arg.Any<Survey>())).Do(o => counter++);
            _Repository.When(c => c.SaveChangesAsync()).Do(o => counter++);
            await _SurveyService.InsertAsync(builder.Get());
            Assert.AreEqual(expectedResult, counter);
        }

        [TestMethod]
        public async Task Get_All_Survey_Async()
        {
            int counter = 0;
            int expectedResult = 1;
            _Repository.GetAllAsync(null, null, null, false).ReturnsForAnyArgs(builder.Get_All());
            _Repository.When(c => c.GetAllAsync(null, null, null, false)).Do(o => counter++);
            IEnumerable<Survey> surveys = await _SurveyService.GetAllAsync(null,null,null,false);
            Assert.AreEqual(expectedResult, counter);
            Assert.IsTrue(surveys.Any());
            Assert.IsTrue(surveys.Count() == expectedResult);
        }
    }
}
