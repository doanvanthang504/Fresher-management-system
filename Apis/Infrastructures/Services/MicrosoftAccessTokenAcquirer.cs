using Application.Interfaces;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class MicrosoftAccessTokenAcquirer : IOAuth2AccessTokenAcquirer
    {
        private readonly IPublicClientApplication _publicClientApplication;

        public MicrosoftAccessTokenAcquirer(IPublicClientApplication publicClientApplication)
        {
            _publicClientApplication = publicClientApplication;
        }

        public async Task<string> GetAccessTokenAsync(
            IEnumerable<string> scopes,
            string organizerEmail,
            IDeviceCodeNotifier deviceCodeNotifier)
        {
            var cachedAccessToken = await GetCachedAccessTokenAsync(scopes, organizerEmail);
            if (cachedAccessToken != null)
                return cachedAccessToken;
            return await GetAccessTokenRemotelyAsync(scopes, deviceCodeNotifier);
        }

        private async Task<string> GetAccessTokenRemotelyAsync(
            IEnumerable<string> scopes,
            IDeviceCodeNotifier deviceCodeNotifier)
        {
            // local function
            async Task DeviceCodeResultCallback(DeviceCodeResult deviceCodeResult)
            {
                var signInUrl = GetSignInUrl(deviceCodeResult);
                var userCode = deviceCodeResult.UserCode;
                await deviceCodeNotifier.OnDeviceCodeReceivedAsync(signInUrl, userCode);
            }

            var accessTokenResult = await _publicClientApplication
                                                .AcquireTokenWithDeviceCode(
                                                    scopes,
                                                    DeviceCodeResultCallback)
                                                .ExecuteAsync();
            return accessTokenResult.AccessToken;
        }

        private async Task<string?> GetCachedAccessTokenAsync(
            IEnumerable<string> scopes,
            string organizerEmail)
        {
            var account = await GetCachedAccountFromEmailAsync(organizerEmail);
            if (account == null)
                return null;
            try
            {
                var tokenResult = await _publicClientApplication
                                            .AcquireTokenSilent(scopes, account)
                                            .ExecuteAsync();
                return tokenResult.AccessToken;
            }
            catch (MsalUiRequiredException)
            {
                return null;
            }
        }

        private async Task<IAccount?> GetCachedAccountFromEmailAsync(string organizerEmail)
        {
            var accounts = await _publicClientApplication.GetAccountsAsync();

            // perform case insensitive in search
            return accounts.FirstOrDefault(e => e.Username.ToLower() == organizerEmail.ToLower());
        }

        private static string GetSignInUrl(DeviceCodeResult deviceCodeResult)
        {
            return $"{deviceCodeResult.VerificationUrl}" +
                        $"?device_code={deviceCodeResult.DeviceCode}" +
                        $"&client_id={deviceCodeResult.ClientId}";
        }
    }
}
