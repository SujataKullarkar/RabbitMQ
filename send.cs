using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    public class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "y";
                    while (message != "n")
                    {
                        Console.WriteLine("Enter message to send");
                        message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);
                        Console.WriteLine(" [x] Sent {0}", message);

                    }
                }
                Console.ReadKey();
            }
        }
    }
}
