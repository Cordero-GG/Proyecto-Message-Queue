using System.Windows.Forms;
using MQClient; // Asegúrate de usar el espacio de nombres correcto

namespace Interfaz_gráfica
{
    partial class Form1
    {
        // Variable del MQClient para la conexión con MQBroker
        private MQClient.MessageQueueClient _mqClient;

        // Declaración de controles
        private Label Label_IP;
        private Label Label_Port;
        private Label Label_AppID;
        private Label label1;
        private TextBox Text_IP;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button btnConectar;
        private Button button1;       // SUBSCRIBE
        private Button button2;       // UNSUBSCRIBE
        private Label label2;
        private TextBox textBox4;
        private Button button3;       // POST (Publicar)
        private TextBox textBox5;
        private Label label3;
        private Label label4;
        private ListBox listBox2;
        private Label label5;
        private Label label6;
        private Button button4;       // RECEIVE
        private TextBox textBox6;

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
            btnConectar = new Button();
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

            // btnConectar
            btnConectar = new Button();
            btnConectar.Location = new System.Drawing.Point(35, 300); // Ajusta la posición según convenga
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new System.Drawing.Size(163, 36);
            btnConectar.TabIndex = 24; // Asegúrate de asignar un índice de tabulación adecuado
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            this.Controls.Add(btnConectar);

            // 
            // Label_IP
            // 
            Label_IP.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            Label_IP.FlatStyle = FlatStyle.Flat;
            Label_IP.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            Label_IP.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            Label_IP.Location = new System.Drawing.Point(35, 41);
            Label_IP.Name = "Label_IP";
            Label_IP.Size = new System.Drawing.Size(163, 36);
            Label_IP.TabIndex = 0;
            Label_IP.Text = "MQ Broker IP";
            Label_IP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Port
            // 
            Label_Port.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            Label_Port.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            Label_Port.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            Label_Port.Location = new System.Drawing.Point(35, 94);
            Label_Port.Name = "Label_Port";
            Label_Port.Size = new System.Drawing.Size(163, 36);
            Label_Port.TabIndex = 1;
            Label_Port.Text = "MQ Broker Port";
            Label_Port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Label_Port.Click += new System.EventHandler(this.Label_Port_Click);
            // 
            // Label_AppID
            // 
            Label_AppID.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            Label_AppID.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            Label_AppID.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            Label_AppID.Location = new System.Drawing.Point(35, 146);
            Label_AppID.Name = "Label_AppID";
            Label_AppID.Size = new System.Drawing.Size(163, 36);
            Label_AppID.TabIndex = 2;
            Label_AppID.Text = "AppID";
            Label_AppID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label1.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label1.Location = new System.Drawing.Point(35, 199);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(163, 36);
            label1.TabIndex = 3;
            label1.Text = "Tema";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Text_IP
            // 
            Text_IP.Location = new System.Drawing.Point(204, 50);
            Text_IP.Name = "Text_IP";
            Text_IP.Size = new System.Drawing.Size(193, 27);
            Text_IP.TabIndex = 4;
            Text_IP.Text = "127.0.0.1"; // Valor por defecto
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(204, 103);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(193, 27);
            textBox1.TabIndex = 5;
            textBox1.Text = "5000"; // Valor por defecto
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(204, 155);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(193, 27);
            textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(204, 208);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(193, 27);
            textBox3.TabIndex = 7;
            // 
            // button1 (SUBSCRIBE)
            // 
            button1.BackColor = System.Drawing.Color.FromArgb(246, 114, 128);
            button1.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            button1.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            button1.Location = new System.Drawing.Point(230, 253);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(194, 37);
            button1.TabIndex = 8;
            button1.Text = "SUBSCRIBE";
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // button2 (UNSUBSCRIBE)
            // 
            button2.BackColor = System.Drawing.Color.FromArgb(246, 114, 128);
            button2.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            button2.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            button2.Location = new System.Drawing.Point(30, 253);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(194, 37);
            button2.TabIndex = 9;
            button2.Text = "UNSUBSCRIBE";
            button2.UseVisualStyleBackColor = false;
            button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2 (AppID)
            // 
            label2.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label2.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label2.Location = new System.Drawing.Point(126, 422);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(163, 36);
            label2.TabIndex = 10;
            label2.Text = "AppID";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            textBox4.Location = new System.Drawing.Point(90, 473);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(238, 27);
            textBox4.TabIndex = 11;
            // 
            // button3 (POST)
            // 
            button3.BackColor = System.Drawing.Color.FromArgb(246, 114, 128);
            button3.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            button3.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            button3.Location = new System.Drawing.Point(780, 242);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(194, 37);
            button3.TabIndex = 13;
            button3.Text = "POST";
            button3.UseVisualStyleBackColor = false;
            button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox5 (Contenido de la publicación)
            // 
            textBox5.AcceptsTab = true;
            textBox5.Location = new System.Drawing.Point(617, 76);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.Size = new System.Drawing.Size(526, 160);
            textBox5.TabIndex = 14;
            textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label3
            // 
            label3.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label3.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label3.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label3.Location = new System.Drawing.Point(722, 36);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(320, 36);
            label3.TabIndex = 15;
            label3.Text = "Contenido de la publicación";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4 (Mensajes recibido)
            // 
            label4.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label4.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label4.Location = new System.Drawing.Point(722, 317);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(320, 36);
            label4.TabIndex = 16;
            label4.Text = "Mensajes recibido";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBox2 (Lista de Temas)
            // 
            listBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 23;
            listBox2.Location = new System.Drawing.Point(525, 405);
            listBox2.Name = "listBox2";
            listBox2.ScrollAlwaysVisible = true;
            listBox2.Size = new System.Drawing.Size(210, 165);
            listBox2.TabIndex = 19;
            // 
            // label5 (Tema)
            // 
            label5.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label5.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label5.Location = new System.Drawing.Point(525, 366);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(210, 36);
            label5.TabIndex = 20;
            label5.Text = "Tema";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6 (Contenido)
            // 
            label6.BackColor = System.Drawing.Color.FromArgb(108, 91, 123);
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            label6.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            label6.Location = new System.Drawing.Point(726, 366);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(457, 36);
            label6.TabIndex = 21;
            label6.Text = "Contenido";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4 (RECEIVE)
            // 
            button4.BackColor = System.Drawing.Color.FromArgb(246, 114, 128);
            button4.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold);
            button4.ForeColor = System.Drawing.Color.FromArgb(248, 177, 149);
            button4.Location = new System.Drawing.Point(525, 588);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(210, 37);
            button4.TabIndex = 22;
            button4.Text = "RECEIVE";
            button4.UseVisualStyleBackColor = false;
            button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox6 (Área para mostrar mensajes recibidos)
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            textBox6.Location = new System.Drawing.Point(741, 405);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.ScrollBars = ScrollBars.Both;
            textBox6.Size = new System.Drawing.Size(442, 165);
            textBox6.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(53, 92, 125);
            this.BackgroundImageLayout = ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1282, 673);
            this.Controls.Add(textBox6);
            this.Controls.Add(button4);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(listBox2);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(textBox5);
            this.Controls.Add(button3);
            this.Controls.Add(textBox4);
            this.Controls.Add(label2);
            this.Controls.Add(btnConectar);
            this.Controls.Add(button2);
            this.Controls.Add(button1);
            this.Controls.Add(textBox3);
            this.Controls.Add(textBox2);
            this.Controls.Add(textBox1);
            this.Controls.Add(Text_IP);
            this.Controls.Add(label1);
            this.Controls.Add(Label_AppID);
            this.Controls.Add(Label_Port);
            this.Controls.Add(Label_IP);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hola";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        // Fin de la declaración de controles
    }
}
