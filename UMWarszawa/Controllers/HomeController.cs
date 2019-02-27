using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using UMWarszawa.Exceptions;
using UMWarszawa.Interfaces;
using UMWarszawa.Models;

namespace UMWarszawa.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpService _httpService;
        private static SemaphoreSlim _notificationsSemaphore = new SemaphoreSlim(1, 1);
        private const string _notificationsKey = "notifications";
        private const double _cacheExpirationInMinutes = 2.0;

        public HomeController(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ActionResult> Index()
        {
            var notifications = await GetNotifications();
            return View(notifications);
        }

        private async Task<IList<Notification>> GetNotifications()
        {
            var notifications = GetNotificationsFromCache();
            if (notifications == null)
            {
                await _notificationsSemaphore.WaitAsync();
                try
                {
                    notifications = GetNotificationsFromCache();
                    if (notifications == null)
                    {
                        notifications = await DownloadNotifications();
                    }
                }
                finally
                {
                    _notificationsSemaphore.Release();
                }
            }

            return notifications;
        }

        private IList<Notification> GetNotificationsFromCache()
        {
            return HttpRuntime.Cache.Get(_notificationsKey) as IList<Notification>;
        }

        private async Task<IList<Notification>> DownloadNotifications()
        {
            var uri = BuildNotificationsUri();
            var response = await _httpService.GetObjectAsync<Response>(uri);
            var notifications = response.Result.Result.Notifications.Where(n => n.NotificationType == NotificationType.INCIDENT).ToList();
            HttpRuntime.Cache.Insert(_notificationsKey, notifications, null, DateTime.Now.AddMinutes(_cacheExpirationInMinutes), Cache.NoSlidingExpiration);
            return notifications;
        }

        private Uri BuildNotificationsUri()
        {
            const string idQuery = "id=28dc65ad-fff5-447b-99a3-95b71b4a7d1e";
            const string Path = "api/action/19115store_getNotifications";

            var configuration = GetConfiguration();
            var apiKeyQuery = $"apikey={configuration.ApiKey}";

            var uBuilder = new UriBuilder(configuration.WarsawApi);
            uBuilder.Path = Path;
            uBuilder.Query = idQuery + "&" + apiKeyQuery;

            return uBuilder.Uri;
        }

        private (string WarsawApi, string ApiKey) GetConfiguration()
        {
            const string WarsawApiUrlKey = "WarsawApiUrl";
            const string ApiKeyKey = "apiKey";

            var warsawApiUrl = ConfigurationManager.AppSettings[WarsawApiUrlKey];
            if (warsawApiUrl == null)
            {
                throw new ConfigurationMissingException(WarsawApiUrlKey);
            }
            var apiKey = ConfigurationManager.AppSettings[ApiKeyKey];
            if (apiKey == null)
            {
                throw new ConfigurationMissingException(ApiKeyKey);
            }

            return (WarsawApi: warsawApiUrl, ApiKey: apiKey);
        }

        public async Task<ActionResult> Details(string number)
        {
            var notifications = await GetNotifications();
            var notification = notifications.SingleOrDefault(n => n.NotificationNumber == number);
            if (notification != null)
            {
                return View(notification);
            }

            return RedirectToAction("Index");
        }
    }
}