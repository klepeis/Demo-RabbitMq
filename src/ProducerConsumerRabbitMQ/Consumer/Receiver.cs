using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            // Create a Factory
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            // Create a Connection
            // Open a Channel
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                // Declare the queue
                channel.QueueDeclare("BasicTest", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received message {message}...");
                };

                channel.BasicConsume("BasicTest", true, consumer);
            }
                       
            Console.WriteLine("Press [enter] to exit the Consumer App...");
            Console.ReadLine();
        }
    }
}
