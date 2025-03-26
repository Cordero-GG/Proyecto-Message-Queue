// Form1.Designer.cs
using System.Windows.Forms;
using MQClient;
// Asegúrate de usar el espacio de nombres correcto

namespace Interfaz_gráfica
{
    partial class Form1
    {
        // En Form1.Designer.cs - Fragmento corregido
        private MQClient.MessageQueueClient _mqClient;

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

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null || !_mqClient.IsConnected()) // Añade método IsConnected() en MQClient
                    throw new InvalidOperationException("Conéctese al servidor primero");

                string tema = textBox3.Text;
                string response = _mqClient.SendRequest($"Subscribe|{_mqClient.AppID}|{tema}");
                MessageBox.Show(response.Contains("OK") ? "Suscrito" : "Error: " + response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Al cerrar el formulario
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _mqClient?.Dispose(); // Cierra la conexión al salir
            base.OnFormClosing(e);
        }

        // Add this method to the Form1 class
        private void Label_Port_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar el código que deseas ejecutar cuando se haga clic en Label_Port
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string response = _mqClient.SendRequest($"Unsubscribe|{textBox2.Text}|{textBox3.Text}");
                MessageBox.Show(response.Contains("OK") ? "Desuscrito" : "Error: " + response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string tema = textBox3.Text;
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mqClient == null)
                {
                    MessageBox.Show("Primero debes conectarte al servidor.");
                    return;
                }

                string tema = listBox2.SelectedItem?.ToString(); // Asume que el tema seleccionado está en el ListBox
                if (string.IsNullOrEmpty(tema))
                {
                    MessageBox.Show("Selecciona un tema.");
                    return;
                }

                // Simula la recepción de un mensaje
                MQClient.Message mensaje = SimulateReceive(new MQClient.Topic(tema));
                if (mensaje != null)
                {
                    textBox6.Text = mensaje.Contenido; // Muestra el mensaje en el TextBox
                }
                else
                {
                    MessageBox.Show("No hay mensajes disponibles.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Método simulado para recibir mensajes
        private MQClient.Message SimulateReceive(MQClient.Topic topic)
        {
            // Aquí puedes agregar la lógica para simular la recepción de un mensaje
            return new MQClient.Message("Mensaje simulado");
        }

        private void ActualizarListBoxTemas()
        {
            listBox2.Items.Clear();
            // Suponiendo que tienes una lista de temas en el cliente o servidor
            // Ejemplo ficticio:
            listBox2.Items.Add("Tema1");
            listBox2.Items.Add("Tema2");
        }

        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Label_IP = new Label();
            Label_Port = new Label();
            Label_AppID = new Label();
            label1 = new Label();
            Text_IP = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            textBox4 = new TextBox();
            button3 = new Button();
            textBox5 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            listBox2 = new ListBox();
            label5 = new Label();
            label6 = new Label();
            button4 = new Button();
            textBox6 = new TextBox();
            SuspendLayout();
            // 
            // Label_IP
            // 
            Label_IP.BackColor = Color.FromArgb(108, 91, 123);
            Label_IP.FlatStyle = FlatStyle.Flat;
            Label_IP.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label_IP.ForeColor = Color.FromArgb(248, 177, 149);
            Label_IP.Location = new Point(35, 41);
            Label_IP.Name = "Label_IP";
            Label_IP.Size = new Size(163, 36);
            Label_IP.TabIndex = 0;
            Label_IP.Text = "MQ Broker IP";
            Label_IP.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label_Port
            // 
            Label_Port.BackColor = Color.FromArgb(108, 91, 123);
            Label_Port.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label_Port.ForeColor = Color.FromArgb(248, 177, 149);
            Label_Port.Location = new Point(35, 94);
            Label_Port.Name = "Label_Port";
            Label_Port.Size = new Size(163, 36);
            Label_Port.TabIndex = 1;
            Label_Port.Text = "MQ Broker Port";
            Label_Port.TextAlign = ContentAlignment.MiddleCenter;
            Label_Port.Click += Label_Port_Click;
            // 
            // Label_AppID
            // 
            Label_AppID.BackColor = Color.FromArgb(108, 91, 123);
            Label_AppID.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Label_AppID.ForeColor = Color.FromArgb(248, 177, 149);
            Label_AppID.Location = new Point(35, 146);
            Label_AppID.Name = "Label_AppID";
            Label_AppID.Size = new Size(163, 36);
            Label_AppID.TabIndex = 2;
            Label_AppID.Text = "AppID";
            Label_AppID.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(108, 91, 123);
            label1.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(248, 177, 149);
            label1.Location = new Point(35, 199);
            label1.Name = "label1";
            label1.Size = new Size(163, 36);
            label1.TabIndex = 3;
            label1.Text = "Tema";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Text_IP
            // 
            Text_IP.Location = new Point(204, 50);
            Text_IP.Name = "Text_IP";
            Text_IP.Size = new Size(193, 27);
            Text_IP.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(204, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(193, 27);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(204, 155);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(193, 27);
            textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(204, 208);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(193, 27);
            textBox3.TabIndex = 7;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(246, 114, 128);
            button1.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(248, 177, 149);
            button1.Location = new Point(230, 253);
            button1.Name = "button1";
            button1.Size = new Size(194, 37);
            button1.TabIndex = 8;
            button1.Text = "SUBSCRIBE";
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(246, 114, 128);
            button2.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.FromArgb(248, 177, 149);
            button2.Location = new Point(30, 253);
            button2.Name = "button2";
            button2.Size = new Size(194, 37);
            button2.TabIndex = 9;
            button2.Text = "UNSUBSCRIBE";
            button2.UseVisualStyleBackColor = false;
            button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(108, 91, 123);
            label2.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(248, 177, 149);
            label2.Location = new Point(126, 422);
            label2.Name = "label2";
            label2.Size = new Size(163, 36);
            label2.TabIndex = 10;
            label2.Text = "AppID";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(90, 473);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(238, 27);
            textBox4.TabIndex = 11;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(246, 114, 128);
            button3.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.FromArgb(248, 177, 149);
            button3.Location = new Point(780, 242);
            button3.Name = "button3";
            button3.Size = new Size(194, 37);
            button3.TabIndex = 13;
            button3.Text = "POST";
            button3.UseVisualStyleBackColor = false;
            button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox5
            // 
            textBox5.AcceptsTab = true;
            textBox5.Location = new Point(617, 76);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.Size = new Size(526, 160);
            textBox5.TabIndex = 14;
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(108, 91, 123);
            label3.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(248, 177, 149);
            label3.Location = new Point(722, 36);
            label3.Name = "label3";
            label3.Size = new Size(320, 36);
            label3.TabIndex = 15;
            label3.Text = "Contenido de la publicación";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(108, 91, 123);
            label4.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(248, 177, 149);
            label4.Location = new Point(722, 317);
            label4.Name = "label4";
            label4.Size = new Size(320, 36);
            label4.TabIndex = 16;
            label4.Text = "Mensajes recibido";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBox2
            // 
            listBox2.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 23;
            listBox2.Items.AddRange(new object[] { "" });
            listBox2.Location = new Point(525, 405);
            listBox2.Name = "listBox2";
            listBox2.ScrollAlwaysVisible = true;
            listBox2.Size = new Size(210, 165);
            listBox2.TabIndex = 19;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(108, 91, 123);
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(248, 177, 149);
            label5.Location = new Point(525, 366);
            label5.Name = "label5";
            label5.Size = new Size(210, 36);
            label5.TabIndex = 20;
            label5.Text = "Tema";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(108, 91, 123);
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(248, 177, 149);
            label6.Location = new Point(726, 366);
            label6.Name = "label6";
            label6.Size = new Size(457, 36);
            label6.TabIndex = 21;
            label6.Text = "Contenido";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(246, 114, 128);
            button4.Font = new Font("Baskerville Old Face", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.FromArgb(248, 177, 149);
            button4.Location = new Point(525, 588);
            button4.Name = "button4";
            button4.Size = new Size(210, 37);
            button4.TabIndex = 22;
            button4.Text = "RECEIVE";
            button4.UseVisualStyleBackColor = false;
            button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox6.Location = new Point(741, 405);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.ScrollBars = ScrollBars.Both;
            textBox6.Size = new Size(442, 165);
            textBox6.TabIndex = 23;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(53, 92, 125);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1282, 673);
            Controls.Add(textBox6);
            Controls.Add(button4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(listBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox5);
            Controls.Add(button3);
            Controls.Add(textBox4);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(Text_IP);
            Controls.Add(label1);
            Controls.Add(Label_AppID);
            Controls.Add(Label_Port);
            Controls.Add(Label_IP);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hola";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Label_IP;
        private System.Windows.Forms.Label Label_Port;
        private System.Windows.Forms.Label Label_AppID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Text_IP;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox6;
    }
}
