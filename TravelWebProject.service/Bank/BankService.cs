using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Rfc7748;
using TravelWebProject.service.BookingService;
using TravelWebProject.service.TransactionService;
using TravelWebProject.service.Users;

namespace TravelWebProject.service.Bank
{
    public class BankService
    {
        private HttpClient _httpClient;
        private string _accessToken;
        private int _accessTokenExpirationTime;
        private ILogger<BankService> _logger;
        private readonly IBookingService _bookingService;
        private readonly ITransactionService _transactionService;

        public BankService(HttpClient httpClient, ILogger<BankService> logger, IBookingService bookingService, ITransactionService transactionService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _bookingService = bookingService;
            _transactionService = transactionService;
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
            var transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(responseContent);
            var transactions = _transactionService.GetListTransaction();
            //Transaction nao chua duoc luu thi luu vao db
            foreach (var transaction in transactionResponse.transactionInfos)
            {
                if (!transaction.ArrangementId.Contains(transactions.Select(x => x.ArrangementId).ToString()))
                {
                    //Check Description if it contains "#Travel " + "Booking ID" #Travel 123456
                    if (transaction.Description.Contains("Travel"))
                    {
                        var bookingId = transaction.Description.Split("Travel ")[1];
                        var booking = _bookingService.GetById(int.Parse(bookingId));
                        if (booking != null)
                        {
                            var transactionInfo = new TransactionInfo
                            {
                                ArrangementId = transaction.ArrangementId,
                                Reference = transaction.Reference,
                                Description = transaction.Description,
                                Category = transaction.Category,
                                BookingDate = transaction.BookingDate,
                                ValueDate = transaction.ValueDate,
                                Amount = transaction.Amount,
                                Currency = transaction.Currency,
                                CreditDebitIndicator = transaction.CreditDebitIndicator,
                                RunningBalance = transaction.RunningBalance,
                                OfsAcctNo = transaction.OfsAcctNo,
                                OfsAcctName = transaction.OfsAcctName,
                                CreditorBankNameVn = transaction.CreditorBankNameVn,
                                CreditorBankNameEn = transaction.CreditorBankNameEn,
                                BookingId = int.Parse(bookingId)
                            };
                            _transactionService.AddTransaction(transactionInfo);
                            //check if Amount is equal to Booking Amount
                            if (decimal.Parse(transaction.Amount) == booking.TotalAmount)
                            {
                                booking.Status = "UnActive";
                                _bookingService.Update(booking);
                            }
                            else
                            {
                                booking.Status = "Partially Paid";
                                _bookingService.Update(booking);
                            }
                        }
                        else
                        {
                            var transactionInfo = new TransactionInfo
                            {
                                ArrangementId = transaction.ArrangementId,
                                Reference = transaction.Reference,
                                Description = transaction.Description,
                                Category = transaction.Category,
                                BookingDate = transaction.BookingDate,
                                ValueDate = transaction.ValueDate,
                                Amount = transaction.Amount,
                                Currency = transaction.Currency,
                                CreditDebitIndicator = transaction.CreditDebitIndicator,
                                RunningBalance = transaction.RunningBalance,
                                OfsAcctNo = transaction.OfsAcctNo,
                                OfsAcctName = transaction.OfsAcctName,
                                CreditorBankNameVn = transaction.CreditorBankNameVn,
                                CreditorBankNameEn = transaction.CreditorBankNameEn,
                                BookingId = null
                            };
                            _transactionService.AddTransaction(transactionInfo);
                        }
                    }
                    else
                    {
                        var transactionInfo = new TransactionInfo
                        {
                            ArrangementId = transaction.ArrangementId,
                            Reference = transaction.Reference,
                            Description = transaction.Description,
                            Category = transaction.Category,
                            BookingDate = transaction.BookingDate,
                            ValueDate = transaction.ValueDate,
                            Amount = transaction.Amount,
                            Currency = transaction.Currency,
                            CreditDebitIndicator = transaction.CreditDebitIndicator,
                            RunningBalance = transaction.RunningBalance,
                            OfsAcctNo = transaction.OfsAcctNo,
                            OfsAcctName = transaction.OfsAcctName,
                            CreditorBankNameVn = transaction.CreditorBankNameVn,
                            CreditorBankNameEn = transaction.CreditorBankNameEn,
                        };
                        _transactionService.AddTransaction(transactionInfo);
                    }
                }
            }
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