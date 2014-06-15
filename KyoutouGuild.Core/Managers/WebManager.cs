using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using KyoutouGuild.Core.Interfaces;

namespace KyoutouGuild.Core.Managers
{
    public class WebManager : IWebManager
    {
        public bool IsNetworkAvailable
        {
            get { return NetworkInterface.GetIsNetworkAvailable(); }
        }

        public async Task<Result> PostData(Uri uri, FormUrlEncodedContent header)
        {
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsync(uri, header);
                string responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(false, string.Empty) : new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public async Task<Result> GetData(Uri uri)
        {
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(uri);
                string responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(false, string.Empty) : new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public class Result
        {
            public Result(bool isSuccess, string json)
            {
                IsSuccess = isSuccess;
                ResultJson = json;
            }

            public bool IsSuccess { get; private set; }
            public string ResultJson { get; private set; }
        }
    }
}
