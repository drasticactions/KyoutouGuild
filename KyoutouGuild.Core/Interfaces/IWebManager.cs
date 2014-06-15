using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KyoutouGuild.Core.Managers;

namespace KyoutouGuild.Core.Interfaces
{
    public interface IWebManager
    {
        bool IsNetworkAvailable { get; }
        Task<WebManager.Result> PostData(Uri uri, FormUrlEncodedContent header);

        Task<WebManager.Result> GetData(Uri uri);
    }
}
