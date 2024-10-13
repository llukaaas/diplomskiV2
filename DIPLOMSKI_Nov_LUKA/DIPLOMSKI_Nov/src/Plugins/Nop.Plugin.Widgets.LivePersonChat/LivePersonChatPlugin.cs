using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System.Threading.Tasks;
using System;
using Nop.Plugin.Widgets.LivePersonChat.Components;

namespace Nop.Plugin.Widgets.LivePersonChat
{
    /// <summary>
    /// Live person provider
    /// </summary>
    public class LivePersonChatPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public bool HideInWidgetList => false;

        #endregion

        #region Ctor

        public LivePersonChatPlugin(ILocalizationService localizationService, 
            ISettingService settingService, 
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { "body_end_html_tag_before" });
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/WidgetsLivePersonChat/Configure";
        }
        
        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task InstallAsync()
        {
            //settings
            var settings = new LivePersonChatSettings
            {
                LiveEngageTag = ""
            };
            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Widgets.LivePersonChat.LiveEngageTag", "LiveEngage Tag");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Plugins.Widgets.LivePersonChat.LiveEngageTag.Hint", "Enter your LiveEngage Tag code here.");

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<LivePersonChatSettings>();

            //locales
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Widgets.LivePersonChat.LiveEngageTag");
            await _localizationService.DeleteLocaleResourceAsync("Plugins.Widgets.LivePersonChat.LiveEngageTag.Hint");

            await base.UninstallAsync();
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsLivePersonViewComponent);
        }

        #endregion
    }
}
