using ABCRetail.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register your Table Storage service
builder.Services.AddSingleton<TableStorageService>();

//Register Blob Storage service 
builder.Services.AddSingleton<BlobStorageService>();

//Register Queue Storage service
builder.Services.AddSingleton<QueueStorageService>();

//Register File Storage service
builder.Services.AddSingleton<FileStorageService>();

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

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();