using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using UMWarszawa.Controllers;
using UMWarszawa.Interfaces;
using UMWarszawa.Models;

namespace UMWarszawa.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Response _apiResponse;
        private Mock<IHttpService> _httpService;
        private HomeController _controller;

        [TestInitialize]
        public void StartUp()
        {
            _apiResponse = LoadApiResponse();

            _httpService = new Mock<IHttpService>();
            _httpService.Setup(h => h.GetObjectAsync<Response>(It.IsAny<Uri>())).ReturnsAsync(_apiResponse);

            _controller = new HomeController(_httpService.Object);
        }

        private Response LoadApiResponse()
        {
            using (var r = new StreamReader("result.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Response>(json);
            }
        }

        [TestMethod]
        public async Task TestIndexViewData()
        {
            var result = await _controller.Index() as ViewResult;
            var model = (IList<Notification>)result.ViewData.Model;

            var expectedCount = _apiResponse.Result.Result.Notifications.Count(n => n.NotificationType == NotificationType.INCIDENT);
            Assert.AreEqual(expectedCount, model.Count);
        }

        [TestMethod]
        public async Task TestDetailsViewData()
        {
            var notification = _apiResponse.Result.Result.Notifications.Last(n => n.NotificationType == NotificationType.INCIDENT);

            var result = await _controller.Details(notification.NotificationNumber) as ViewResult;
            var model = (Notification)result.ViewData.Model;

            Assert.AreEqual(notification.Event, model.Event);
            Assert.AreEqual(notification.CreateDate, model.CreateDate);
        }

        [TestMethod]
        public async Task TestDetailsViewDataWithIncorrectNumber()
        {
            var result = await _controller.Details("Incorrect_Number") as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}
