using System;
using System.Threading.Tasks;
using Hablar_con_socket_y_json;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sistema Pub/Sub Continuo\n");

        // Iniciar Subscriber en segundo plano
        var subscriberTask = Task.Run(Subscriber.Start);

        // Esperar 1 segundo para que el Subscriber inicie
        await Task.Delay(1000);

        // Iniciar Publisher interactivo
        await Publisher.Start();

        await subscriberTask; // Mantener la app activa
    }
}