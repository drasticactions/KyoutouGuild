using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using KyoutouGuild.Common;
using KyoutouGuild.Core.Entities;
using KyoutouGuild.Core.Managers;

namespace KyoutouGuild.ViewModels
{
    public class LoginPageViewModel : NotifierBase
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged();
            }
        }

        public async Task<LoginUserEntity> NewLogIn(string sessionId)
        {
            IsLoading = true;
            AuthenticationManager authenticationManager = new AuthenticationManager();
            var result = await authenticationManager.Login(sessionId);
            if (result == null)
            {
                IsLoading = false;
            }
            return result;
        }

        public async Task<LoginUserEntity> IsLoggedIn()
        {
            object sessionId;
            if (!_localSettings.Values.TryGetValue("sessionId", out sessionId) || sessionId == null)
            {
                return null;
            }
            IsLoading = true;
            AuthenticationManager authenticationManager = new AuthenticationManager();
            var result = await authenticationManager.Login((string)sessionId);
            if (result == null)
            {
                IsLoading = false;
            }
            return result;
        }
    }
}
