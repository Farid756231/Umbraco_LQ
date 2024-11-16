using System.Runtime.Serialization;
using Google.Authenticator;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;

namespace My.Website;

[DataContract]
public class TwoFactorAuthInfo : ISetupTwoFactorModel
{
    [DataMember(Name = "qrCodeSetupImageUrl")]
    public string? QrCodeSetupImageUrl { get; set; }

    [DataMember(Name = "secret")]
    public string? Secret { get; set; }
}

public class UmbracoUserAppAuthenticator : ITwoFactorProvider
{
 
    public const string Name = "UmbracoUserAppAuthenticator";

    private readonly IUserService _userService;

  
    public UmbracoUserAppAuthenticator(IUserService userService)
    {
        _userService = userService;
    }

 
    public string ProviderName => Name;

    public Task<ISetupTwoFactorModel> GetSetupDataAsync(Guid userOrMemberKey, string secret)
    {
        IUser? user = _userService.GetByKey(userOrMemberKey);

        ArgumentNullException.ThrowIfNull(user);

        var twoFactorAuthenticator = new TwoFactorAuthenticator();
        SetupCode setupInfo = twoFactorAuthenticator.GenerateSetupCode("My application name", user.Username, secret, false);
        return Task.FromResult<ISetupTwoFactorModel>(new TwoFactorAuthInfo()
        {
            QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl,
            Secret = secret
        });
    }

    public bool ValidateTwoFactorPIN(string secret, string code)
    {
        var twoFactorAuthenticator = new TwoFactorAuthenticator();
        return twoFactorAuthenticator.ValidateTwoFactorPIN(secret, code);
    }

    public bool ValidateTwoFactorSetup(string secret, string token) => ValidateTwoFactorPIN(secret, token);
}