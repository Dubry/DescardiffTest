using DescartesDiff.Controllers;
using DescartesDiff.Data;
using DescartesDiff.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DescartesDiffUnitTest
{
    [TestClass]
    public class DiffTests
    {

        [TestMethod]
        public async Task DiffTestMissingOne()
        {
            var context = await GetTestDBContext();
            var controller = new DiffController(context);

            var result = controller.GetDataModel(1).Result.Result;
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            var putResult = controller.PutDataModelRight(1, new BaseData() { Data = "AAAAAA==" }).Result;
            Assert.IsInstanceOfType(putResult, typeof(CreatedAtActionResult));

            var equalResult = await controller.GetDataModel(1);
            Assert.IsInstanceOfType(equalResult.Result, typeof(OkObjectResult));
            // And so on
        }

        private static async Task<DiffContext> GetTestDBContext()
        {
            var options = new DbContextOptionsBuilder<DiffContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var testContext = new DiffContext(options);
            testContext.Database.EnsureCreated();
            if (await testContext.DiffResults.CountAsync() <= 0)
            {
                testContext.DiffResults.Add(new DescartesDiff.Models.DataModel()
                {
                    DataModelId = 1,
                    LeftBase = "AAAAAA=="
                });
                testContext.DiffResults.Add(new DescartesDiff.Models.DataModel()
                {
                    DataModelId = 2,
                    LeftBase = "AAAAAA==",
                    RightBase = "AAAAAA=="
                });
                testContext.DiffResults.Add(new DescartesDiff.Models.DataModel()
                {
                    DataModelId = 3,
                    LeftBase = "AAAAAA==",
                    RightBase = "QAAAAA=="
                });
                testContext.DiffResults.Add(new DescartesDiff.Models.DataModel()
                {
                    DataModelId = 4,
                    LeftBase = "AAAAA==",
                    RightBase = "QABAQ=="
                });
                testContext.DiffResults.Add(new DescartesDiff.Models.DataModel()
                {
                    DataModelId = 5,
                    LeftBase = "AAAAA==",
                    RightBase = "AAAAAA=="
                });
                await testContext.SaveChangesAsync();
            }

            return testContext;
        }
    }
}
