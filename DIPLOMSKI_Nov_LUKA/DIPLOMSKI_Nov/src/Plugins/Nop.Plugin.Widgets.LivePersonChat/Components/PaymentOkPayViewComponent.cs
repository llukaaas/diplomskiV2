using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.LivePersonChat.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.LivePersonChat.Components
{
    [ViewComponent(Name = "WidgetsLivePerson")]
    public class WidgetsLivePersonViewComponent : NopViewComponent
    {
        private readonly LivePersonChatSettings _livePersonChatSettings;

        public WidgetsLivePersonViewComponent(LivePersonChatSettings livePersonChatSettings)
        {
            _livePersonChatSettings = livePersonChatSettings;
        }

        public IViewComponentResult Invoke()
        {
            var model = new PublicInfoModel
            {
                LiveEngageTag = _livePersonChatSettings.LiveEngageTag
            };

            return View("~/Plugins/Widgets.LivePersonChat/Views/PublicInfo.cshtml", model);
        }
    }
}
