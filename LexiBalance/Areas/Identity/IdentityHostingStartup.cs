using System;
using LexiBalance.Areas.Identity.Data;
using LexiBalance.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LexiBalance.Areas.Identity.IdentityHostingStartup))]
namespace LexiBalance.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UsuariosContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("UsuariosContextConnection")));

                services.AddDefaultIdentity<LexiBalanceUser>()
                    .AddEntityFrameworkStores<UsuariosContext>();
            });
        }
    }
}