﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CarAdverts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddTransient<ICarAdvertService, CarAdvertService>();
            services.AddDbContext<ApplicationContext>(
                opts => opts.UseInMemoryDatabase(databaseName: "CarAdverts"));
            //opts => opts.UseSqlServer(Configuration.GetConnectionString("CarAdverts")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Car Adverts", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFromAllDomains",
                    builder => builder.AllowAnyOrigin());
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                // Seed the database.
                AddSeedData(context);
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Adverts");
            });
            app.UseCors("AllowFromAllDomains");
            app.UseMvc();
        }

        private void AddSeedData(ApplicationContext context)
        {
            var carAdverts = new List<CarAdvert>()
            {
                new CarAdvert
                {
                    Title = "Toyota Corolla 2012 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 50000,
                    New = true,

                },
                new CarAdvert
                {
                    Title = "Used Toyota Matrix 2014 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 45000,
                    New = false,
                    Mileage = 60000,
                    FirstRegistration= DateTime.Now.Subtract(TimeSpan.FromDays(200))

                },
                new CarAdvert
                {
                    Title = "Honda Accord 2016 for sale",
                    Fuel = FuelType.Gasoline,
                    Price = 50000,
                    New = true,

                },
                new CarAdvert
                {
                    Title = "Toyota Highlander 2012 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 120000,
                    New = false,
                    FirstRegistration = DateTime.Now.Subtract(TimeSpan.FromDays(720)),
                    Mileage = 29000

                },
                new CarAdvert
                {
                    Title = "Toyota Corolla 2012 for sale",
                    Fuel = FuelType.Gasoline,
                    Price = 50000,
                    New = true,
                },

            };
            context.CarAdverts.AddRange(carAdverts);
            context.SaveChanges();
        }
    }
}
