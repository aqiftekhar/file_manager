using FileManagerBackend.Repositories.FileRepository;
using FileManagerBackend.Repositories.TagAssignmentRepository;
using FileManagerBackend.Repositories.TagRepository;
using FileManagerBackend.Repositories.VolumeRepository;
using FileManagerBackend.Services.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure DbContext
builder.Services.AddDbContext<FileManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure repositories
builder.Services.AddScoped<IVolumeRepository, VolumeRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagAssignmentRepository, TagAssignmentRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
