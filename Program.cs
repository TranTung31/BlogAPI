using BlogAPI.Data;
using BlogAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDB"));
});

builder.Services.AddAutoMapper(typeof(Program));

// Life cycle DI
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IAuthor, AuthorService>();
builder.Services.AddScoped<IBlog, BlogService>();

// Add CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
