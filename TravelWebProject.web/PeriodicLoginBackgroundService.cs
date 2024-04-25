 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWebProject.service.Bank;

namespace TravelWebProject.web
{
    public class PeriodicLoginBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PeriodicLoginBackgroundService> _logger;

        public PeriodicLoginBackgroundService(IServiceProvider serviceProvider, ILogger<PeriodicLoginBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int count = 0;
            while (!stoppingToken.IsCancellationRequested || count < 2)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                        var accountNo = config["BANK_ACCOUNT_NO"];
                        var username = config["BANK_ACCOUNT_NUMBER"];
                        var password = config["BANK_ACCOUNT_PASSWORD"];
                        var fromDate = config["FROM_DATE"];
                        var toDate = config["TO_DATE"];
                        var bankService = scope.ServiceProvider.GetRequiredService<BankService>();

                        await bankService.LoginAsync(username, password);
                        await Task.Delay(2000, stoppingToken);

                        await bankService.GetListTransactionAsync(accountNo, fromDate, toDate);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing periodic login");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}