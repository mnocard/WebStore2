using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            //Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_Content()
        {
            const string base_line = "Action with value id:";

            var controller = new HomeController();

            const string id = "TestId";

            var result = controller.SecondAction(id);

            var content = Assert.IsType<ContentResult>(result);

            const string expected_content = base_line + id;

            Assert.Equal(expected_content, content.Content);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException()
        {
            var controller = new HomeController();

            controller.Throw();
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Test_error()
        {
            const string expected_exception_message = "Test error!";

            var controller = new HomeController();
            
            Exception exception = null;

            try
            {
                controller.Throw();
            }
            catch (ApplicationException e)
            {
                exception = e;
            }

            var app_exception = Assert.IsType<ApplicationException>(exception);

            Assert.Equal(expected_exception_message, app_exception.Message); 
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException_with_Test_error2()
        {
            const string expected_exception_message = "Test error!";

            var controller = new HomeController();

            var exception = Assert.Throws<ApplicationException>(() => controller.Throw());

            Assert.Equal(expected_exception_message, exception.Message);
        }
    }
}
