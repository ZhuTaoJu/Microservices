using Email.API.Core;
using Microsoft.AspNetCore.Mvc;
using Services.Core.BaseController;

namespace Email.API.Controllers
{
    /// <summary>
    /// 域控制器
    /// </summary>
    [Route("websit-api/[area]/[controller]/[action]")]
    [Area(EmailConsts.AreaName)]
    public abstract class AreaController : BaseController
    {

    }
}
