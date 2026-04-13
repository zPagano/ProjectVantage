#region Usings
using Vantage.AppHost;
#endregion

var builder = DistributedApplication.CreateBuilder(args);

#region Infrastructure Orchestration

// Relational Database for persistent entity state
var sql = builder.AddSqlServer(ResourceNames.SqlServer)
    .WithDataVolume(VolumeNames.SqlServerData);

// Distributed Cache and SignalR Backplane
var redis = builder.AddRedis(ResourceNames.Redis)
    .WithDataVolume(VolumeNames.RedisData);

// Message Broker for asynchronous event-driven communication via MassTransit
var messaging = builder.AddRabbitMQ(ResourceNames.RabbitMq)
    .WithDataVolume(VolumeNames.RabbitMqData);

#endregion

builder.Build().Run();