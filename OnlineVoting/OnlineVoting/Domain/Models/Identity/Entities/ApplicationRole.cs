using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using OnlineVoting.Domain.Administration.Entities;
using OnlineVoting.Domain.Common;

namespace SystemOfWidget.Domain.Identity.Entities
{
    /// <summary>
    /// Роль в системе
    /// </summary>
    [Auditable]
    public class ApplicationRole : IdentityRole, IEntityWithId<string>
    {
    }
}
