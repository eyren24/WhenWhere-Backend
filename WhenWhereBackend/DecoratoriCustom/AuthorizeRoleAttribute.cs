using System.Security.Claims;
using Auth.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WhenWhereBackend.DecoratoriCustom;


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeRoleAttribute(params ERuolo[] allowedRoles) : Attribute, IAsyncAuthorizationFilter {
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
        var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>().Any();

        if (hasAllowAnonymous) {
            return;
        }

        var user = context.HttpContext.User;

        if (user.Identity is not { IsAuthenticated: true }) {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (!Enum.TryParse(roleClaim, out ERuolo userRole)) {
            context.Result = new ForbidResult();
            return;
        }

        if (userRole == ERuolo.Amministratore || allowedRoles.Contains(userRole)) {
            return;
        }

        context.Result = new ForbidResult();
    }
}