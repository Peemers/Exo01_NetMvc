using ExoAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<TmdbService>(client =>
{
  client.BaseAddress = new Uri(builder.Configuration["TMDB:BaseUrl"] ?? "");
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
  name: "movie_genre",
  pattern: "movie/{genre}/{action=Index}/{id?}",
  defaults: new { Controller = "Movies" }
  );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Film}/{action=Index}/{id?}")
  .WithStaticAssets();


app.Run();