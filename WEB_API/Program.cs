using DLL.Services.Implementation;
using DLL.Services.Interface;
using Web_APIS.Repository;
using Web_APIS.Repository.Implementaion;
using Web_APIS.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICollectionCenterRepository, CollectionCenterRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();


builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICollectionCenterService, CollectionCenterService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();

builder.Services.AddScoped<IPatientServices, PatientServices>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<ITestService, TestServices>();
builder.Services.AddScoped<ITestRepository, TestRepository>();

builder.Services.AddScoped<IRateServices, RateServices>();
builder.Services.AddScoped<IRateRepository, RateRepository>();



var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
