using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.LivePersonChat.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.LivePersonChat.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class WidgetsLivePersonChatController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly LivePersonChatSettings _livePersonChatSettings;
        private readonly IPermissionService _permissionService;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        public WidgetsLivePersonChatController(ISettingService settingService, 
            ILocalizationService localizationService, 
            LivePersonChatSettings livePersonChatSettings,
            IPermissionService permissionService,
            INotificationService notificationService)
        {
            _settingService = settingService;
            _localizationService = localizationService;
            _livePersonChatSettings = livePersonChatSettings;
            _permissionService = permissionService;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods
        
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new ConfigurationModel
            {
                LiveEngageTag = _livePersonChatSettings.LiveEngageTag
            };

            return View("~/Plugins/Widgets.LivePersonChat/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //save settings
            _livePersonChatSettings.LiveEngageTag = model.LiveEngageTag;

            await _settingService.SaveSettingAsync(_livePersonChatSettings);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return RedirectToAction("Configure");
        }
        
        #endregion
    }
}