using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using MailSender.ViewModels;
using System.Windows;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MailSender.Data;
using MailSender.lib.Models;
using MailSender.Data.Stores.InMemory;
using MailSender.Data.Stores.InDB;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
               .ConfigureHostConfiguration(cfg => cfg
                   .AddJsonFile("appconfig.json", true, true)
                   .AddXmlFile("appsettings.xml", true, true)
                )
               .ConfigureAppConfiguration(cfg => cfg
                   .AddJsonFile("appconfig.json", true, true)
                   .AddXmlFile("appsettings.xml", true, true)
                )
               .ConfigureLogging(log => log
                   .AddConsole()
                   .AddDebug()
                )
               .ConfigureServices(ConfigureServices)
               .Build();

        public static  IServiceProvider Services => Hosting.Services;   

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            services.AddSingleton<IEncryptorService, RfcEncryptor>();

            services.AddDbContext<MailSenderDB>(opt => opt.UseSqlServer(host.Configuration
                .GetConnectionString("Default")));

            services.AddTransient<MailSenderDbInitialiser>();

            services.AddSingleton<IStore<Recipient>, RecipientsStoreInDB>();
            services.AddSingleton<IStore<Sender>, SendersStoreInDB>();
            services.AddSingleton<IStore<Server>, ServersStoreInDB>();
            services.AddSingleton<IStore<Message>, MessagesStoreInDB>();
            services.AddSingleton<IStore<SchedulerTask>, SchedulerTasksStoreInDB>();
            services.AddSingleton<IMailSchedulerService, TaskMailSchedulerService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            Services.GetRequiredService<MailSenderDbInitialiser>().Initialise();
            base.OnStartup(e);
        }
    }
}
