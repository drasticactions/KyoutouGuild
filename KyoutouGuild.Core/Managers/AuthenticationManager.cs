using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using KyoutouGuild.Core.Entities;
using KyoutouGuild.Core.Exceptions;
using KyoutouGuild.Core.Interfaces;
using KyoutouGuild.Core.Tools;
using Newtonsoft.Json;

namespace KyoutouGuild.Core.Managers
{
    public class AuthenticationManager
    {
        private readonly IWebManager _webManager;
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public AuthenticationManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public AuthenticationManager()
            : this(new WebManager())
        {
        }

        public async Task<LoginUserEntity> Login(string sessionId)
        {
            if (!_webManager.IsNetworkAvailable)
            {
                throw new LoginFailedException(
                    "The network is unavailable. Check your network settings and please try again.");
            }

            try
            {
                var result = await this.SendLoginData(sessionId);
                return JsonConvert.DeserializeObject<LoginUserEntity>(result.ResultJson);
            }
            catch
            {
                throw new LoginFailedException("An error has occured");
            }
        }

        private async Task<WebManager.Result> SendLoginData(string sessionId)
        {
            var result = await _webManager.GetData(new Uri(string.Format(EndPoints.Login, sessionId)));
            if (result.IsSuccess)
            {
                _localSettings.Values["sessionId"] = sessionId;
            }
            return result;
        }
    }
}
