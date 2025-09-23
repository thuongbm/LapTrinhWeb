var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

// Route riêng cho Admin/Student
/*app.MapControllerRoute(
    name: "admin_students",
    pattern: "Admin/Student/{action=List}/{id?}",
    defaults: new { controller = "Students" }
);*/

app.MapControllerRoute(
    name: "add_student",
    pattern: "Admin/Student/{action=Add}/{id?}",
    defaults: new { controller = "Students" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
