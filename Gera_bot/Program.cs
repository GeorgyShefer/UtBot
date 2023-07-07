using Gera_bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using Telegram.Bot;

class Program
{
    private static async Task Main(string[] args)
    {
        var host = new HostBuilder().ConfigureServices((hostContext, services) => ConfigureServices(services)).UseConsoleLifetime().Build();

        Console.WriteLine("Сервис запущен");

        await host.RunAsync();

        Console.WriteLine("Сервис закрыт");
    }
    static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6020178710:AAHBLeSOIQDMZ3quGXX6ba1Ycmo9KOp7VdM"));

        services.AddHostedService<Bot>();

        services.AddSingleton<IntSum>();

        services.AddTransient<TextMessage>();
        
    }
}