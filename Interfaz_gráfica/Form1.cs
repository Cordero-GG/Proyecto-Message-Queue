using System;
using System.Windows.Forms;
using MQClient;

namespace Interfaz_gráfica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Método para conectarse al MQBroker usando MQClient
        // Los valores de IP y puerto se obtienen de los controles de la interfaz (con valores por defecto 127.0.0.1 y 5000)
        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = Text_IP.Text;
                int port = int.Parse(textBox1.Text);
                Guid appID = Guid.Parse(textBox2.Text);

                _mqClient?.Dispose(); // Cierra conexión previa si existe
                _mqClient = new MessageQueueClient(ip, port, appID);
                MessageBox.Show("Conectado al servidor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
            }
        }

        // Método para suscribirse a un tema
        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null || !_mqClient.IsConnected())
                    throw new InvalidOperationException("Conéctese al servidor primero");

                string tema = textBox3.Text;
                string response = _mqClient.SendRequest($"Subscribe|{_mqClient.AppID}|{tema}");
                MessageBox.Show(response.Contains("OK") ? "Suscrito" : "Error: " + response);
                if (response.Contains("OK"))
                {
                    ActualizarListBoxTemas(tema);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void ActualizarListBoxTemas(string nuevoTema)
        {
            if (string.IsNullOrWhiteSpace(nuevoTema))
                return;

            if (!listBox2.Items.Contains(nuevoTema))
            {
                listBox2.Items.Add(nuevoTema);
            }
        }



        // Método para desuscribirse de un tema
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string response = _mqClient.SendRequest($"Unsubscribe|{_mqClient.AppID}|{textBox3.Text}");
                MessageBox.Show(response.Contains("OK") ? "Desuscrito" : "Error: " + response);
                listBox2.Items.Remove(textBox3.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Método para publicar un mensaje
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string tema = listBox2.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(tema))
                {
                    MessageBox.Show("Selecciona un tema de la lista.");
                    return;
                }

                string contenido = textBox5.Text;
                string response = _mqClient.SendRequest($"Publish|{tema}|{contenido}");
                bool publicado = response.Contains("OK");
                MessageBox.Show(publicado ? "Mensaje publicado." : "Error al publicar.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Método para recibir mensajes reales del servidor 
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string tema = listBox2.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(tema))
                {
                    MessageBox.Show("Selecciona un tema.");
                    return;
                }

                // Enviar solicitud de recepción real al MQBroker
                string response = _mqClient.SendRequest($"Receive|{_mqClient.AppID}|{tema}");
                // Se asume que la respuesta es del tipo "OK|<mensaje>" o un mensaje de error
                if (response.StartsWith("OK|"))
                {
                    textBox6.Text = response.Substring(3); // Quitar el prefijo "OK|"
                }
                else
                {
                    MessageBox.Show("Error o " + response);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        


        // Al cerrar el formulario, se libera la conexión
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _mqClient?.Dispose();
            base.OnFormClosing(e);
        }

        // Método para manejar el clic en Label_Port (opcional)
        private void Label_Port_Click(object sender, EventArgs e)
        {
            // Se puede agregar lógica adicional aquí si se requiere
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío si no necesitas lógica adicional.
        }


        // Si hubiera otros eventos, se agregan aquí...
    }
}
