using Confluent.Kafka;
using FitTrack.Models.Serialization;
using Microsoft.Extensions.Hosting;

namespace FitTrack.DL.Kafka.KafkaCache
{
    public class KafkaCache<TKey, TValue> : BackgroundService
    {
        private readonly ConsumerConfig _config;

        public KafkaCache()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "d101n6b9s3jfi4njpu2g.any.eu-central-1.mpx.prd.cloud.redpanda.com:9092",
                GroupId = $"KafkaChat-{Guid.NewGuid}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "user",
                SaslPassword = "s5pHU1Mz4klanQm4INhd1wVZ6zR872"
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);

            return Task.CompletedTask;
        }

        private void ConsumeMessages(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<TKey, TValue>(_config)
              .SetValueDeserializer(new MessagePackDeserializer<TValue>())
              .Build())
            {
                consumer.Subscribe("user_cache");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }

                    if (consumeResult != null)
                    {
                        Console.WriteLine($"Error: {consumeResult.Message.Key}");
                    }
                }
            }
        }
    }
}
