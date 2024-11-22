using AuctionKoi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DailyDiscountService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DailyDiscountService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auctionService = scope.ServiceProvider.GetRequiredService<iAuctionService>();

                try
                {
                    // Gọi phương thức giảm giá
                    auctionService.ApplyDailyDiscount();
                    Console.WriteLine($"[DailyDiscountService] Discount applied at {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DailyDiscountService] Error: {ex.Message}");
                }
            }

            // Chờ 24 giờ trước khi chạy lại
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
