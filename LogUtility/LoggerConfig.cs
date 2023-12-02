using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Compact;


namespace LogUtility
{
    public static class LoggerConfig
    {

        /// <summary>
        /// write log in file or seq
        /// </summary>
        public static void Configure(IConfiguration configuration) //send config setting from appseting like log file path or seq path
        {
            
            Serilog.Log.Logger = new LoggerConfiguration()
                           .MinimumLevel.Verbose()
                           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                           .Enrich.FromLogContext()
                           .Enrich.WithMachineName()
                           .Enrich.WithProcessId()                        
                           .Enrich.WithThreadId()
                           .Enrich.WithProcessName()                                                     
                           // .WriteTo.Seq("http://localhost:5341") // to writ seq
                           .WriteTo.File(new CompactJsonFormatter(), $"appLogs/serilogExample.log-{DateTime.Now.ToString("dd-mm-yyyy")}.log", rollingInterval: RollingInterval.Day)
                           .CreateLogger();

        }
        /// <summary>
        /// wirt log in azure blob
        /// install extra package 
        /// 1. serilog.sinks.Async
        /// 2.serilog.sinks.AzureBlobStorage
        /// </summary>
        /// <param name="configuration"></param>
        //public static void Configure(IConfiguration configuration)
        //{
        //    Serilog.Log.Logger = new LoggerConfiguration().
        //    MinimumLevel.Override("Microsoft",
        //    LogEventLevel.Information)
        //    .Enrich.FromLogContext()
        //    .Enrich.WithMachineName()
        //    .Enrich.WithThreadId()
        //    .Enrich.WithProcessName()
        //    .WriteTo.Async(delegate (LoggerSinkConfiguration x)
        //    {
        //        x.AzureBlobStorage(configuration["AppConfig:BlobEndpoint"],
        //        LogEventLevel.Verbose, "logs", "{yyyy}/{MM}-{dd}/serilogExample.txt", "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
        //    }).CreateBootstrapLogger();
        //}

    }
}
