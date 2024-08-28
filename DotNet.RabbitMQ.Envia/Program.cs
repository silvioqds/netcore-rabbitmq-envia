using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

//Crio uma factory de conexão com nome do host
var factory = new ConnectionFactory { HostName = "localhost" }; 

using var connection = factory.CreateConnection(); // Crio a conexão
using var channel = connection.CreateModel(); // Crio um canal

//Crio uma fila no meu canal com nome de hello
channel.QueueDeclare(queue: "hello",
                     durable: false, 
                     exclusive: false, 
                     autoDelete: false, 
                     arguments: null);

//Console.WriteLine("Digite sua mensagem");

//while (true)
//{
    //string? message = Console.ReadLine();

    //if (string.IsNullOrEmpty(message))
    //    break;

    var aluno = new Aluno() { Id = 1, Name = "Silvio" };
    string message = JsonSerializer.Serialize<Aluno>(aluno);
    
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,
                         routingKey:"hello", 
                         basicProperties: null,
                         body: body);

    Console.WriteLine("Mensagem enviada");
    Console.ReadKey();
//}

class Aluno
{
    public int Id { get; set; }
    public string? Name { get; set; }
}