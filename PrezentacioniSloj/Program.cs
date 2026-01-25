using AplikacioniSloj;
using DomenskiSloj;
using SlojPodataka.Interfejsi;
using SlojPodataka.Repozitorijum;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Connection string iz appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Dependency Injection - Repozitorijumi
builder.Services.AddScoped<IKorisnikRepo>(provider => new KorisnikRepo(connectionString));
builder.Services.AddScoped<IVozacRepo>(provider => new VozacRepo(connectionString));
builder.Services.AddScoped<IKamionRepo>(provider => new KamionRepo(connectionString));
builder.Services.AddScoped<ITransportniNalogRepo>(provider => new TransportniNalogRepo(connectionString));

// Dependency Injection - Domain Layer
builder.Services.AddScoped<PoslovnaPravila>();

// Dependency Injection - Application Layer (Servisi)
builder.Services.AddScoped<KorisnikServis>();
builder.Services.AddScoped<VozacServis>();
builder.Services.AddScoped<KamionServis>();
builder.Services.AddScoped<TransportniNalogServis>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// SESSION - mora biti posle UseRouting, a pre UseAuthorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();