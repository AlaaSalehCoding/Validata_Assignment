using VirtualShop.Infrastructure.Identity;

namespace VirtualShop.Web.Endpoints;
public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        ///Commented as customizatio to MapIdentityApi needed to reflect the new attributes
        ///Other option was to impement the needed apis
        //app.MapGroup(this)
        //    .MapIdentityApi<ApplicationUser>();

    }
}
