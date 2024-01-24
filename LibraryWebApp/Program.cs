using LibraryWebApp.Abstraction;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var libraryApiHost = builder.Configuration.GetSection("LIBRARY_API_HOST").Value ?? string.Empty;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRefitClient<IBookClient>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri(libraryApiHost);
});
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
