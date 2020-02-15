using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Valentine.Api.Interfaces
{
    public interface IAzureService
    {
        Task<string> GetKeyVaultSecretAsync(string secretName);
    }
}
