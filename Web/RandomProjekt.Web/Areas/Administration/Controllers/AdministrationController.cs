namespace RandomProjekt.Web.Areas.Administration.Controllers
{
    using RandomProjekt.Common;
    using RandomProjekt.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
