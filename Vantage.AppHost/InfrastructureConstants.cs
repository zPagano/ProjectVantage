namespace Vantage.AppHost
{
    internal static class ResourceNames
    {
        public const string SqlServer = "sql";
        public const string Redis = "redis";
        public const string RabbitMq = "messaging";
    }

    internal static class VolumeNames
    {
        public const string SqlServerData = "vantage-sql-data";
        public const string RedisData = "vantage-redis-data";
        public const string RabbitMqData = "vantage-rmq-data";
    }
}
