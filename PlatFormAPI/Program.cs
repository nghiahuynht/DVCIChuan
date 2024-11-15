using GM_DAL.IServices;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using GM_DAL.Services;
using GM_DAL;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<GMDbContext>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<ICaptionTeamService, CaptionTeamService>();
builder.Services.AddScoped<IRouteSaleService, RouteSaleService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISaleOrderService, SaleOrderService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "ComCode",
    Type = SecuritySchemeType.ApiKey,
    In = ParameterLocation.Header,
    Description = "Please input ComCode",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "ComCode"
            }
        },
        Array.Empty<string>()
    }
};

var contact = new OpenApiContact()
{
    Name = "nghiaht",
    Email = "huynhnghia911@gmail.com",
    Url = new Uri("http://www.gamanjsc.com")
};

var license = new OpenApiLicense()
{
    Name = "GM",
    Url = new Uri("http://www.gamanjsc.com")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "ComCode",
    Description = "Input ComCode to identity your Com",
    TermsOfService = new Uri("http://www.example.com"),
    Contact = contact,
    License = license
};


builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("ComCode", securityScheme);
    o.AddSecurityRequirement(securityReq);
    //o.CustomSchemaIds(i => i.FullName?.Replace("+", "."));
    o.UseInlineDefinitionsForEnums();
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle








var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
