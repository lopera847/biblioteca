using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaApp.Data;

namespace BibliotecaApp.Forms
{
    public partial class LoginForm : Form
    {
        private BibliotecaRepository repository;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Button btnLogin;
        private Button btnCancelar;
        private Label lblUsuario;
        private Label lblContraseña;

        public LoginForm()
        {
            InitializeComponent();
            repository = new BibliotecaRepository();
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new TextBox();
            this.txtContraseña = new TextBox();
            this.btnLogin = new Button();
            this.btnCancelar = new Button();
            this.lblUsuario = new Label();
            this.lblContraseña = new Label();

            // Configurar el formulario
            this.SuspendLayout();
            this.Text = "Iniciar Sesión - Sistema de Biblioteca";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // lblUsuario
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Location = new Point(50, 40);
            this.lblUsuario.Size = new Size(80, 20);
            this.lblUsuario.Font = new Font("Microsoft Sans Serif", 10F);

            // txtUsuario
            this.txtUsuario.Location = new Point(130, 40);
            this.txtUsuario.Size = new Size(150, 20);
            this.txtUsuario.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtUsuario.TabIndex = 0;

            // lblContraseña
            this.lblContraseña.Text = "Contraseña:";
            this.lblContraseña.Location = new Point(50, 80);
            this.lblContraseña.Size = new Size(80, 20);
            this.lblContraseña.Font = new Font("Microsoft Sans Serif", 10F);

            // txtContraseña
            this.txtContraseña.Location = new Point(130, 80);
            this.txtContraseña.Size = new Size(150, 20);
            this.txtContraseña.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtContraseña.PasswordChar = '*';
            this.txtContraseña.TabIndex = 1;

            // btnLogin
            this.btnLogin.Text = "Ingresar";
            this.btnLogin.Location = new Point(80, 130);
            this.btnLogin.Size = new Size(80, 30);
            this.btnLogin.BackColor = Color.SteelBlue;
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // btnCancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(180, 130);
            this.btnCancelar.Size = new Size(80, 30);
            this.btnCancelar.BackColor = Color.Gray;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Click += new EventHandler(this.btnCancelar_Click);

            // Agregar controles al formulario
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancelar);

            this.ResumeLayout(false);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (repository.ValidarUsuario(usuario, contraseña))
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContraseña.Clear();
                txtUsuario.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
