using AutoMapper;
using ToDoList;
using ToDoList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IToDoTaskRepository, ToDoTaskDBRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryDBRepository>();

var config = AutoMapperConfig.Configure();
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{id?}");

app.Run();
