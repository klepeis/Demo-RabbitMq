using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
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
            using(var channel = connection.CreateModel())
            {
                // Declare the queue
                channel.QueueDeclare("BasicTest", false, false, false, null);

                // Create a message
                string message = "Getting started with .Net Core RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                // Publish the message
                channel.BasicPublish("", "BasicTest", null, body);
                Console.WriteLine($"Sent message {message}...");
            }

            Console.WriteLine("Press [enter] to exit the Sender App...");
            Console.ReadLine();
        }
    }
}