using AutoMapper;
using Dapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Signa.Library.Core;
using Signa.Library.Core.Aspnet.Domain.Entities;
using Signa.Library.Core.Aspnet.Filters;
using Signa.Library.Core.Aspnet.Filters.ErrorHandlings;
using Signa.Library.Core.Aspnet.Helpers;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Business;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Signa.OvaDesova.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly StartupValidator _startupValidator;
        private string applicationBasePath { get; }
        private string applicationName { get; }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            Configuration = configuration;
            applicationBasePath = env.ContentRootPath;
            applicationName = env.ApplicationName;
            Global.ConnectionString = Configuration["DATABASE_CONNECTION"];
            _startupValidator = new StartupValidator();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(c =>
            {
            }), typeof(Startup));

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidateModelAttribute));
                })
                .AddFluentValidation();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters = new List<JsonConverter> { new DecimalConverter() };
                });

            #region :: Acesso a Dados / Dapper ::
            services.AddTransient<ComboDAO>();
            services.AddTransient<DadosGeraisDAO>();
            services.AddTransient<MaterialPeacaoDAO>();
            services.AddTransient<OvaDesovaDAO>();
            services.AddTransient<TarifaEspecialDAO>();
            services.AddTransient<TarifasPadraoDAO>();

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            Dapper.SqlMapper.AddTypeMap(typeof(string), System.Data.DbType.AnsiString);
            #endregion

            #region :: Generic Classes ::
            services.AddTransient<HelperRepository>();
            #endregion

            #region :: Business ::
            services.AddTransient<ComboBL>();
            services.AddTransient<DadosGeraisBL>();
            services.AddTransient<MaterialPeacaoBL>();
            services.AddTransient<OvaDesovaBL>();
            services.AddTransient<TarifaEspecialBL>();
            services.AddTransient<TarifasPadraoBL>();
            #endregion

            #region :: AutoMapper ::
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                // TODO: seria possível deixar isso em outras classes?
                cfg.CreateMap<AcordoEspecialEntity, AcordoEspecialModel>().ReverseMap();
                cfg.CreateMap<AcordoRodoviarioEntity, AcordoRodoviarioModel>().ReverseMap();
                cfg.CreateMap<FamiliaMercadoriaEntity, FamiliaMercadoriaModel>().ReverseMap();
                cfg.CreateMap<FornecedorEntity, FornecedorModel>().ReverseMap();
                cfg.CreateMap<MaterialEntity, MaterialModel>().ReverseMap();
                cfg.CreateMap<MunicipioEntity, MunicipioModel>().ReverseMap();
                cfg.CreateMap<UfEntity, UfModel>().ReverseMap();
                cfg.CreateMap<UnidadeMedidaEntity, UnidadeMedidaModel>().ReverseMap();
                cfg.CreateMap<VeiculoEntity, VeiculoModel>().ReverseMap();
                cfg.CreateMap<ConsultaEntity, ConsultaModel>().ReverseMap();
                cfg.CreateMap<DadosGeraisEntity, DadosGeraisModel>().ReverseMap();
                cfg.CreateMap<MaterialPeacaoEntity, MaterialPeacaoModel>().ReverseMap();
                cfg.CreateMap<OvaDesovaEntity, OvaDesovaModel>().ReverseMap();
                cfg.CreateMap<ResultadoEntity, ResultadoModel>().ReverseMap();
                cfg.CreateMap<TarifaEspecialEntity, TarifaEspecialModel>().ReverseMap();
                cfg.CreateMap<TarifasPadraoEntity, TarifasPadraoModel>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region :: Swagger ::
            //Necessário para a documentação do Swagger
            services.AddMvcCore().AddApiExplorer();

            services.AddResponseCompression();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Signa Consultoria e Sistemas",
                        Version = "v1",
                        Description = "API Template Signa",
                        Contact = new OpenApiContact
                        {
                            Name = "Signa",
                            Url = new Uri("http://signainfo.com.br")
                        }
                    });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region :: Filters ::
            // TODO: deixar em uma inclusão apenas
            services.AddTransient<SignaRegraNegocioExceptionHandling>();
            services.AddTransient<SignaSqlNotFoundExceptionHandling>();
            services.AddTransient<SqlExceptionHandling>();
            services.AddTransient<GenericExceptionHandling>();
            #endregion

            #region :: AppSettings ::
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            _startupValidator.Validate(appSettings);
            #endregion

            #region :: JWT / Token / Auth ::
            var signingConfigurations = new SigningConfigurations(appSettings.Secret);
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.SaveToken = true;

                    var paramsValidation = bearerOptions.TokenValidationParameters;

                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.ValidateIssuer = false;
                    paramsValidation.ValidateAudience = false;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("./v1/swagger.json", "Template de API .NET Core Signa");
            });

            app.UseRouting();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();

            loggerFactory.AddSerilog();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            #region :: Middleware Claims from JWT ::
            // DOC: https://www.wellingtonjhn.com/posts/obtendo-o-usu%C3%A1rio-logado-em-apis-asp.net-core/
            app.Use(async delegate (HttpContext httpContext, Func<Task> next)
            {
                if (httpContext.Request.Headers.Any())
                {
                    try
                    {
                        Global.UsuarioId = int.Parse(httpContext.Request.Headers["UsuarioId"]);
                        Global.EmpresaId = int.Parse(httpContext.Request.Headers["EmpresaId"]);
                        Global.GrupoUsuarioId = int.Parse(httpContext.Request.Headers["GrupoUsuarioId"]);
                    }
                    catch (Exception) { }
                }

                if (httpContext.User.Claims.Any())
                {
                    try
                    {
                        Global.UsuarioId = int.Parse(httpContext.User.Claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);
                    }
                    catch (Exception) { }
                }

                await next.Invoke();
            });
            #endregion

            app.UseCors(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
