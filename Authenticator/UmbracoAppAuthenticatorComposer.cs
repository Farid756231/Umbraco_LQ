using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Security;

namespace My.Website;

public class UmbracoAppAuthenticatorComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        var identityBuilder = new BackOfficeIdentityBuilder(builder.Services);

        identityBuilder.AddTwoFactorProvider<UmbracoUserAppAuthenticator>(UmbracoUserAppAuthenticator.Name);
    }
}