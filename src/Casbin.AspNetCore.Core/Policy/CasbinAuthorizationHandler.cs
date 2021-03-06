using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Casbin.AspNetCore.Authorization.Policy
{
    public class CasbinAuthorizationHandler : AuthorizationHandler<CasbinAuthorizationRequirement, ICasbinAuthorizationContext>
    {
        private readonly IEnforceService _enforcerService;

        public CasbinAuthorizationHandler(IEnforceService enforcerService)
        {
            _enforcerService = enforcerService ?? throw new ArgumentNullException(nameof(enforcerService));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CasbinAuthorizationRequirement requirement,
            ICasbinAuthorizationContext casbinContext)
        {
            if (await _enforcerService.EnforceAsync(casbinContext))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
