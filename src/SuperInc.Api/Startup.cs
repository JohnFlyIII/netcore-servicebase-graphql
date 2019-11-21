using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BOS.ServiceBase.GraphQL.Schemas.Types;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperInc.Api.Datastores;
using SuperInc.Api.Schemas.Enums;
using SuperInc.Api.Schemas.Mutations;
using SuperInc.Api.Schemas.Queries;
using SuperInc.Api.Schemas.Types;
using SuperInc.Auth;
using SuperInc.Core.Interfaces;
using SuperInc.Infrastructure.Data;
using SuperInc.Infrastructure.Repositories;

namespace SuperInc.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Startup));
            
            ConfigurePersistance(services);

            // Summary:
            // With GraphQL, the query and mutation object/types, the schemas and the object types
            // must be injected for use
            services.AddScoped<HeroType>();
            services.AddScoped<HeroQuery>();
            services.AddScoped<HeroSchema>();
            services.AddScoped<HeroMutation>();
            services.AddScoped<HeroClassificationEnum>();
            services.AddScoped<CreateHeroInputType>();
            services.AddScoped<IHeroesDatastore, HeroesDatastore>();


            // Summary:
            //  Required to resolve dependency injection with GraphQL 
            services.AddSingleton<IDependencyResolver>(
                c => new FuncDependencyResolver(type =>
                c.GetRequiredService(type)));

            // Summary:
            // GraphQL needs to be added here, and WebSockets being added is required if you want to allow for Subscriptions.
            // This may not always be necessary, but subcriptions are a powerful tool in GraphQL. The UserContextBuilder
            // established the context GraphQL can work with.
            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddDataLoader()
            .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

        }
        private void ConfigurePersistance(IServiceCollection services)
        {
            var heroesContextConnectionString = Configuration.GetConnectionString("HeroesDb");

            var heroesContextOptionsBuilder = new DbContextOptionsBuilder<HeroesDbContext>();

            heroesContextOptionsBuilder.UseNpgsql(heroesContextConnectionString);

            var o = heroesContextOptionsBuilder.Options;

            services.AddScoped<DbContextOptions<HeroesDbContext>>(_ => o);

            services.AddDbContext<HeroesDbContext>(options => options = heroesContextOptionsBuilder);

            services.AddScoped<IHeroesRepository, HeroesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            // Summary:
            //  There should be one endpoint "/graphql" that handles all of the services queries and mutations.
            app.UseGraphQL<HeroSchema>("/graphql");


            // Only load the GraphiQL server if in development
            if (env.IsEnvironment("Local"))
            {
                //app.UseGraphiQLServer(new GraphiQLOptions());
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
                //app.UseGraphQLVoyager(new GraphQLVoyagerOptions());
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
