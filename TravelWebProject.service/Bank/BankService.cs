using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TravelWebProject.service.Bank
{
    public class BankService
    {
        private HttpClient _httpClient;
        private string _accessToken;
        private int _accessTokenExpirationTime;
        private ILogger<BankService> _logger;
        // private readonly TransactionInfosRepository _transactionInfosRepository; , TransactionInfosRepository transactionInfosRepository

        public BankService(HttpClient httpClient, ILogger<BankService> logger)
        {
            _httpClient = httpClient;
            // _transactionInfosRepository = transactionInfosRepository;
            _logger = logger;
        }

        public async Task LoginAsync(string username, string password)
        {
            string url = "https://ebank.tpb.vn/gateway/api/auth/login";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            var formData = new Dictionary<string, string>
        {
            { "username", username },
            { "password", password },
        };

            request.Content = new FormUrlEncodedContent(formData);

            // Send the POST request
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<TpBankAPI>(responseContent);

            if (apiResponse != null)
            {
                _accessToken = apiResponse.AccessToken;
                _accessTokenExpirationTime = apiResponse.ExpiresIn;
                _logger.LogInformation("Access token: {0}", _accessToken);
            }
        }

        public async Task GetListTransactionAsync(string accountNo, string fromDate, string toDate)
        {
            string url = "https://ebank.tpb.vn/gateway/api/smart-search-presentation-service/v2/account-transactions/find";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Referer", "https://ebank.tpb.vn");
            request.Headers.Add("Authorization", "Bearer " + _accessToken);
            request.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"122\", \"Not(A:Brand\";v=\"24\", \"Microsoft Edge\";v=\"122\"");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0");
            var payload = new
            {
                pageNumber = 1,
                pageSize = 400,
                accountNo = accountNo,
                currency = "VND",
                maxAcentrysrno = "",
                fromDate = fromDate,
                toDate = toDate,
                keyword = ""
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json");

            // Send the POST request
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            // var transactionResponse = JsonConvert.DeserializeObject<ListTransactionResponse>(responseContent);
            //ghi log responseContent ra file để xem dữ liệu trả về
            _logger.LogInformation(responseContent);
            // Process the response data
        }
    }

    public class TpBankAPI
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}