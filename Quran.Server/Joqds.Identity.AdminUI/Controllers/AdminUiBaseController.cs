using Joqds.Identity.AdminUI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joqds.Identity.AdminUI.Controllers
{
    [ApiController]
    [Authorize(Roles = ConstSettings.RoleRequire)]
    [Route("AdminUi/[controller]")]
    public abstract class AdminUiBaseController: ControllerBase
    {

    }
}
