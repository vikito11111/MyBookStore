using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Configuration;
using MyBookStore.Data;
using MyBookStore.Models;
using MyBookStore.Services.Authors;
using MyBookStore.Services.Books;
using MyBookStore.Services.Carts;
using MyBookStore.Services.Comments;
using MyBookStore.Services.Forums;
using MyBookStore.Services.Genres;
using MyBookStore.Services.Publishers;
using MyBookStore.Services.SubGenres;
using MyBookStore.Services.Topics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MyBookStoreDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
	{
		options.SignIn.RequireConfirmedEmail = false;
		options.SignIn.RequireConfirmedPhoneNumber = false;
		options.Password.RequiredLength = 6;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
	})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyBookStoreDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ISubGenreService, SubGenreService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

builder.Services.AddTransient<IForumService, ForumService>();
builder.Services.AddTransient<ITopicService, TopicService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddTransient<IBookRatingService, BookRatingService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    RoleInitializer.EnsureRolesExist(serviceProvider).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
