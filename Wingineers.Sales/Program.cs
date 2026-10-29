using Wingineers.Sales.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SalesService>();
builder.Services.AddHttpClient<ISalesApiClient, SalesApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["SalesApi:BaseUrl"]!);

    client.DefaultRequestHeaders.Add("ApiKey", builder.Configuration["SalesApi:ApiKey"]);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
