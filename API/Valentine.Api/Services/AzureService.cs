//using Microsoft.Azure.KeyVault;
//using Microsoft.Azure.KeyVault.Models;
//using Microsoft.Azure.Services.AppAuthentication;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Valentine.Api.Interfaces;

//namespace Valentine.Api.Services
//{
//    public class AzureService : IAzureService
//    {
//        private const string AzureKeyVaultUrl = "https://valentine-kv.vault.azure.net/secrets/{0}";

//        public async Task<string> GetKeyVaultSecretAsync(string secretName)
//        {
//            try
//            {
//                var tokenProvider = new AzureServiceTokenProvider();
//                var keyVaultClient = new KeyVaultClient(
//                    new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback)
//                );

//                var secret = await keyVaultClient.GetSecretAsync(string.Format(AzureKeyVaultUrl, secretName)).ConfigureAwait(false);
//                return secret.Value;
//            }
//            catch (KeyVaultErrorException)
//            {
//                throw;
//            }
//        }
//    }
//}
