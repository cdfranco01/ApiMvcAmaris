using AmarisApi.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar EmployeeApiService
builder.Services.AddScoped<EmployeeApiService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<EmployeeApiService>(); // Agrega `HttpClient` al servicio
builder.Services.AddHttpClient<EmployeeApiService>(client =>
{
    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
