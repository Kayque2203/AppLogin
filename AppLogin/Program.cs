using AppLogin.Libraries.Login;
using AppLogin.Repository;
using AppLogin.Repository.Contract;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AppLogin.Libraries.Sessao.Sessao>();

builder.Services.AddScoped<AppLogin.Libraries.Sessao.Sessao>();
builder.Services.AddScoped<LoginCliente>();
//builder.Services.AddScoped<LoginColaborador>();

// Corrigir problema com TEMPDATA
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Definir um tempo para duração. 
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    // Mostrar para o navegador que o cookie e essencial   
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc().AddSessionStateTempDataProvider();



var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCookiePolicy();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
