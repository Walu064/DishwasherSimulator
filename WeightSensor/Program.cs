
namespace CapacitySensor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string strURL = string.Format(@"http://*:{0}", "36005");
                    webBuilder
                        .UseUrls(strURL)
                        .UseStartup<Startup>();
                });
    }
}
