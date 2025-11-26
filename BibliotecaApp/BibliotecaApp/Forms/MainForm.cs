using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Forms
{
    public partial class MainForm : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem gestionarToolStripMenuItem;
        private ToolStripMenuItem librosToolStripMenuItem;
        private ToolStripMenuItem suscriptoresToolStripMenuItem;
        private ToolStripMenuItem prestamosToolStripMenuItem;
        private ToolStripMenuItem informesToolStripMenuItem;
        private ToolStripMenuItem suscriptoresConPrestamosToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private Label lblTitulo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new MenuStrip();
            this.gestionarToolStripMenuItem = new ToolStripMenuItem();
            this.librosToolStripMenuItem = new ToolStripMenuItem();
            this.suscriptoresToolStripMenuItem = new ToolStripMenuItem();
            this.prestamosToolStripMenuItem = new ToolStripMenuItem();
            this.informesToolStripMenuItem = new ToolStripMenuItem();
            this.suscriptoresConPrestamosToolStripMenuItem = new ToolStripMenuItem();
            this.salirToolStripMenuItem = new ToolStripMenuItem();
            this.lblTitulo = new Label();

            // Configurar el formulario
            this.SuspendLayout();
            this.Text = "Sistema de Gestión de Biblioteca";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // lblTitulo
            this.lblTitulo.Text = "SISTEMA DE GESTIÓN DE BIBLIOTECA";
            this.lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.DarkBlue;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new Point(200, 200);
            this.lblTitulo.Size = new Size(400, 30);

            // menuStrip1
            this.menuStrip1.BackColor = Color.LightSteelBlue;

            // gestionarToolStripMenuItem
            this.gestionarToolStripMenuItem.Text = "Gestionar";
            this.gestionarToolStripMenuItem.Font = new Font("Microsoft Sans Serif", 10F);

            // librosToolStripMenuItem
            this.librosToolStripMenuItem.Text = "Libros";
            this.librosToolStripMenuItem.Click += new EventHandler(this.librosToolStripMenuItem_Click);

            // suscriptoresToolStripMenuItem
            this.suscriptoresToolStripMenuItem.Text = "Suscriptores";
            this.suscriptoresToolStripMenuItem.Click += new EventHandler(this.suscriptoresToolStripMenuItem_Click);

            // prestamosToolStripMenuItem
            this.prestamosToolStripMenuItem.Text = "Préstamos";
            this.prestamosToolStripMenuItem.Click += new EventHandler(this.prestamosToolStripMenuItem_Click);

            // Agregar opciones al menú Gestionar
            this.gestionarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.librosToolStripMenuItem,
                this.suscriptoresToolStripMenuItem,
                this.prestamosToolStripMenuItem});

            // informesToolStripMenuItem
            this.informesToolStripMenuItem.Text = "Informes";
            this.informesToolStripMenuItem.Font = new Font("Microsoft Sans Serif", 10F);

            // suscriptoresConPrestamosToolStripMenuItem
            this.suscriptoresConPrestamosToolStripMenuItem.Text = "Suscriptores con préstamos vigentes";
            this.suscriptoresConPrestamosToolStripMenuItem.Click += new EventHandler(this.suscriptoresConPrestamosToolStripMenuItem_Click);

            // Agregar opciones al menú Informes
            this.informesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.suscriptoresConPrestamosToolStripMenuItem});

            // salirToolStripMenuItem
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Font = new Font("Microsoft Sans Serif", 10F);
            this.salirToolStripMenuItem.Click += new EventHandler(this.salirToolStripMenuItem_Click);

            // Agregar menús principales
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.gestionarToolStripMenuItem,
                this.informesToolStripMenuItem,
                this.salirToolStripMenuItem});

            // Configurar MenuStrip
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(800, 28);
            this.menuStrip1.TabIndex = 0;

            // Agregar controles al formulario
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void librosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionLibrosForm form = new GestionLibrosForm();
            form.ShowDialog();
        }

        private void suscriptoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionSuscriptoresForm form = new GestionSuscriptoresForm();
            form.ShowDialog();
        }

        private void prestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionPrestamosForm form = new GestionPrestamosForm();
            form.ShowDialog();
        }

        private void suscriptoresConPrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformesForm form = new InformesForm();
            form.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
