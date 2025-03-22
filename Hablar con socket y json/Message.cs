namespace MQBroker
{
    public class Message
    {
        public string Contenido { get; }

        public Message(string contenido)
        {
            Contenido = contenido;
        }
    }
}