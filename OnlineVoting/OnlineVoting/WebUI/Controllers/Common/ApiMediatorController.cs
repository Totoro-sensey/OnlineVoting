using Microsoft.AspNetCore.Mvc;

namespace OnlineVoting.Controllers.Common
{
    [Route("/[controller]/[action]")]
    public abstract class ApiMediatorController : MediatorController
    {
    }
}