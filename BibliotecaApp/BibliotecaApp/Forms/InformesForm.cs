using BibliotecaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Forms
{
    public partial class InformesForm : Form
    {
        private BibliotecaRepository repository;
        private DataGridView dataGridView;
        private Button btnSuscriptoresVigentes;
        private Button btnTodosPrestamos;
        private Label label1;
        private Label lblTitulo;
        private Button btnCerrar;

        public InformesForm()
        {
            InitializeComponent();
            repository = new BibliotecaRepository();
            CargarPrestamosCompletos();
        }

        private void CargarPrestamosCompletos()
        {
            try
            {
                dataGridView.DataSource = repository.ObtenerPrestamosCompletos();
                lblTitulo.Text = "Todos los Préstamos";
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los préstamos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuscriptoresVigentes_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.DataSource = repository.ObtenerSuscriptoresConPrestamosVigentes();
                lblTitulo.Text = "Suscriptores con Préstamos Vigentes";
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los suscriptores: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTodosPrestamos_Click(object sender, EventArgs e)
        {
            CargarPrestamosCompletos();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AjustarColumnasDataGridView()
        {
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Ajustar nombres de columnas para mejor presentación
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    switch (column.Name)
                    {
                        case "docsuscriptor":
                            column.HeaderText = "Documento Suscriptor";
                            break;
                        case "NombreSuscriptor":
                            column.HeaderText = "Nombre del Suscriptor";
                            break;
                        case "codigolibro":
                            column.HeaderText = "Código Libro";
                            break;
                        case "TituloLibro":
                            column.HeaderText = "Título del Libro";
                            break;
                        case "fechaprestamo":
                            column.HeaderText = "Fecha Préstamo";
                            break;
                        case "fechadevolucion":
                            column.HeaderText = "Fecha Devolución";
                            break;
                        case "nombre":
                            column.HeaderText = "Nombre del Suscriptor";
                            break;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.dataGridView = new DataGridView();
            this.btnSuscriptoresVigentes = new Button();
            this.btnTodosPrestamos = new Button();
            this.label1 = new Label();
            this.lblTitulo = new Label();
            this.btnCerrar = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();

            // dataGridView
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 80);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(760, 350);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnSuscriptoresVigentes
            this.btnSuscriptoresVigentes.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSuscriptoresVigentes.FlatStyle = FlatStyle.Flat;
            this.btnSuscriptoresVigentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuscriptoresVigentes.ForeColor = System.Drawing.Color.White;
            this.btnSuscriptoresVigentes.Location = new System.Drawing.Point(12, 440);
            this.btnSuscriptoresVigentes.Name = "btnSuscriptoresVigentes";
            this.btnSuscriptoresVigentes.Size = new System.Drawing.Size(220, 35);
            this.btnSuscriptoresVigentes.TabIndex = 1;
            this.btnSuscriptoresVigentes.Text = "Suscriptores con Préstamos Vigentes";
            this.btnSuscriptoresVigentes.UseVisualStyleBackColor = false;
            this.btnSuscriptoresVigentes.Click += new EventHandler(this.btnSuscriptoresVigentes_Click);

            // btnTodosPrestamos
            this.btnTodosPrestamos.BackColor = System.Drawing.Color.Green;
            this.btnTodosPrestamos.FlatStyle = FlatStyle.Flat;
            this.btnTodosPrestamos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTodosPrestamos.ForeColor = System.Drawing.Color.White;
            this.btnTodosPrestamos.Location = new System.Drawing.Point(240, 440);
            this.btnTodosPrestamos.Name = "btnTodosPrestamos";
            this.btnTodosPrestamos.Size = new System.Drawing.Size(180, 35);
            this.btnTodosPrestamos.TabIndex = 2;
            this.btnTodosPrestamos.Text = "Todos los Préstamos";
            this.btnTodosPrestamos.UseVisualStyleBackColor = false;
            this.btnTodosPrestamos.Click += new EventHandler(this.btnTodosPrestamos_Click);

            // btnCerrar
            this.btnCerrar.BackColor = System.Drawing.Color.Crimson;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(672, 440);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 35);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new EventHandler(this.btnCerrar_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Informes";

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular);
            this.lblTitulo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitulo.Location = new System.Drawing.Point(12, 50);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(140, 17);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "Todos los Préstamos";

            // InformesForm
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(784, 491);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnTodosPrestamos);
            this.Controls.Add(this.btnSuscriptoresVigentes);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformesForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Informes de Préstamos - Sistema de Biblioteca";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
