using MokshenAPI;
using System.Net;

internal class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
        {
            webBuilder.UseUrls("http://0.0.0.0:80", "https://0.0.0.0:443");
            webBuilder.UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 80);
            });
        }
    });
}