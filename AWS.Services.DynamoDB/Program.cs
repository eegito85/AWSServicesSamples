using Amazon.DynamoDBv2;
using AutoMapper;
using AWS.Services.DynamoDB.Data.DataModels;
using AWS.Services.DynamoDB.Data.Repositories;
using AWS.Services.DynamoDB.Data.Repositories.Interfaces;
using AWS.Services.DynamoDB.Data.Services;
using AWS.Services.DynamoDB.Data.Services.Interfaces;
using AWS.Services.DynamoDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();

var mapConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<MessageModel, MessageModelDTO>();
});
var mapConfig1 = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<MessageModelDTO, MessageModel>();
});
IMapper mapper = mapConfig.CreateMapper();
IMapper mapper2 = mapConfig1.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton(mapper2);
builder.Services.AddSingleton<IDynamoDBRepository, DynamoDBRepository>();
builder.Services.AddSingleton<IDynamoDBService, DynamoDBService>();

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
