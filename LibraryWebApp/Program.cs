using LibraryWebApp.Models;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var libraryApiHost = builder.Configuration.GetSection("LIBRARY_API_HOST").Value ?? string.Empty;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("library_api", c =>
{
    c.BaseAddress = new Uri(libraryApiHost);
});

builder.Services.AddCors();

//DI, Ilogger, BookService [X]
//http client factory [X], refit 
//IConfiguration (remove reference to environment variables) [X]
// Refactor connection strings [X]
// One Example of EntityFramework

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
