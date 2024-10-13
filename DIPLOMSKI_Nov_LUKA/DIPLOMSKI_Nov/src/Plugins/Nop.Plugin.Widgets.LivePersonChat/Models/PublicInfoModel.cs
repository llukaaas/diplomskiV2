using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.LivePersonChat.Models
{
    public record PublicInfoModel : BaseNopModel
    {
        public string LiveEngageTag { get; set; }
    }
}