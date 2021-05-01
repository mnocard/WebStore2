using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Controllers;
using WebStore.Interfaces.TestApi;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void Index_Returns_View_with_Values()
        {
            var expected_values = new string[] { "1", "2", "3" };

            var valueService_mock = new Mock<IValueService>(); ;
            valueService_mock
                .Setup(service => service.Get())
                .Returns(expected_values);

            var controller = new ValuesController(valueService_mock.Object);

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_values.Length, model.Count());

            valueService_mock.Verify(service => service.Get());
            valueService_mock.VerifyNoOtherCalls();

        }
    }
}
