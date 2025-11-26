using BibliotecaApp.Data;
using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Forms
{
    public partial class GestionLibrosForm : Form
    {
        private BibliotecaRepository repository;
        private DataGridView dataGridView;
        private Button btnAgregar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnCerrar;
        private Button btnLimpiar;
        private Label label1;
        private TextBox txtCodigo;
        private TextBox txtTitulo;
        private TextBox txtAutor;
        private Label lblCodigo;
        private Label lblTitulo;
        private Label lblAutor;
        private GroupBox groupBoxDatos;

        public GestionLibrosForm()
        {
            InitializeComponent();
            repository = new BibliotecaRepository();
            CargarLibros();
            LimpiarFormulario();
        }

        private void InitializeComponent()
        {
            // Configurar el formulario principal
            this.SuspendLayout();
            this.Text = "Gestión de Libros";
            this.Size = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // label1 - Título
            this.label1 = new Label();
            this.label1.Text = "GESTIÓN DE LIBROS";
            this.label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.label1.ForeColor = Color.DarkBlue;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 20);
            this.label1.Size = new Size(300, 24);

            // groupBoxDatos
            this.groupBoxDatos = new GroupBox();
            this.groupBoxDatos.Text = "Datos del Libro";
            this.groupBoxDatos.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.groupBoxDatos.Location = new Point(20, 60);
            this.groupBoxDatos.Size = new Size(800, 120);
            this.groupBoxDatos.BackColor = Color.White;

            // lblCodigo
            this.lblCodigo = new Label();
            this.lblCodigo.Text = "Código:";
            this.lblCodigo.Location = new Point(20, 30);
            this.lblCodigo.Size = new Size(60, 20);
            this.lblCodigo.Font = new Font("Microsoft Sans Serif", 9F);

            // txtCodigo
            this.txtCodigo = new TextBox();
            this.txtCodigo.Location = new Point(80, 28);
            this.txtCodigo.Size = new Size(100, 20);
            this.txtCodigo.Font = new Font("Microsoft Sans Serif", 9F);

            // lblTitulo
            this.lblTitulo = new Label();
            this.lblTitulo.Text = "Título:";
            this.lblTitulo.Location = new Point(200, 30);
            this.lblTitulo.Size = new Size(50, 20);
            this.lblTitulo.Font = new Font("Microsoft Sans Serif", 9F);

            // txtTitulo
            this.txtTitulo = new TextBox();
            this.txtTitulo.Location = new Point(260, 28);
            this.txtTitulo.Size = new Size(300, 20);
            this.txtTitulo.Font = new Font("Microsoft Sans Serif", 9F);

            // lblAutor
            this.lblAutor = new Label();
            this.lblAutor.Text = "Autor:";
            this.lblAutor.Location = new Point(20, 70);
            this.lblAutor.Size = new Size(50, 20);
            this.lblAutor.Font = new Font("Microsoft Sans Serif", 9F);

            // txtAutor
            this.txtAutor = new TextBox();
            this.txtAutor.Location = new Point(80, 68);
            this.txtAutor.Size = new Size(480, 20);
            this.txtAutor.Font = new Font("Microsoft Sans Serif", 9F);

            // btnLimpiar
            this.btnLimpiar = new Button();
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Location = new Point(610, 25);
            this.btnLimpiar.Size = new Size(80, 30);
            this.btnLimpiar.BackColor = Color.LightGray;
            this.btnLimpiar.FlatStyle = FlatStyle.Flat;
            this.btnLimpiar.Click += new EventHandler(this.btnLimpiar_Click);

            // Agregar controles al groupBox
            this.groupBoxDatos.Controls.Add(this.lblCodigo);
            this.groupBoxDatos.Controls.Add(this.txtCodigo);
            this.groupBoxDatos.Controls.Add(this.lblTitulo);
            this.groupBoxDatos.Controls.Add(this.txtTitulo);
            this.groupBoxDatos.Controls.Add(this.lblAutor);
            this.groupBoxDatos.Controls.Add(this.txtAutor);
            this.groupBoxDatos.Controls.Add(this.btnLimpiar);

            // dataGridView
            this.dataGridView = new DataGridView();
            this.dataGridView.Location = new Point(20, 200);
            this.dataGridView.Size = new Size(800, 250);
            this.dataGridView.BackgroundColor = Color.White;
            this.dataGridView.BorderStyle = BorderStyle.Fixed3D;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.SelectionChanged += new EventHandler(this.dataGridView_SelectionChanged);

            // btnAgregar
            this.btnAgregar = new Button();
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Location = new Point(20, 470);
            this.btnAgregar.Size = new Size(100, 35);
            this.btnAgregar.BackColor = Color.Green;
            this.btnAgregar.ForeColor = Color.White;
            this.btnAgregar.FlatStyle = FlatStyle.Flat;
            this.btnAgregar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);

            // btnActualizar
            this.btnActualizar = new Button();
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Location = new Point(130, 470);
            this.btnActualizar.Size = new Size(100, 35);
            this.btnActualizar.BackColor = Color.Blue;
            this.btnActualizar.ForeColor = Color.White;
            this.btnActualizar.FlatStyle = FlatStyle.Flat;
            this.btnActualizar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnActualizar.Click += new EventHandler(this.btnActualizar_Click);

            // btnEliminar
            this.btnEliminar = new Button();
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new Point(240, 470);
            this.btnEliminar.Size = new Size(100, 35);
            this.btnEliminar.BackColor = Color.Red;
            this.btnEliminar.ForeColor = Color.White;
            this.btnEliminar.FlatStyle = FlatStyle.Flat;
            this.btnEliminar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);

            // btnCerrar
            this.btnCerrar = new Button();
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Location = new Point(720, 470);
            this.btnCerrar.Size = new Size(100, 35);
            this.btnCerrar.BackColor = Color.Gray;
            this.btnCerrar.ForeColor = Color.White;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.btnCerrar.Click += new EventHandler(this.btnCerrar_Click);

            // Agregar controles al formulario
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxDatos);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCerrar);

            this.ResumeLayout(false);
        }

        private void CargarLibros()
        {
            try
            {
                dataGridView.DataSource = repository.ObtenerLibros();
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los libros: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColumnasDataGridView()
        {
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtCodigo.Enabled = true;
            btnAgregar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    Libro libro = new Libro
                    {
                        codigo = int.Parse(txtCodigo.Text.Trim()),
                        titulo = txtTitulo.Text.Trim(),
                        autor = txtAutor.Text.Trim()
                    };

                    repository.AgregarLibro(libro);
                    CargarLibros();
                    LimpiarFormulario();
                    MessageBox.Show("Libro agregado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar libro: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    Libro libro = new Libro
                    {
                        codigo = int.Parse(txtCodigo.Text.Trim()),
                        titulo = txtTitulo.Text.Trim(),
                        autor = txtAutor.Text.Trim()
                    };

                    repository.ActualizarLibro(libro);
                    CargarLibros();
                    LimpiarFormulario();
                    MessageBox.Show("Libro actualizado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar libro: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(txtCodigo.Text.Trim());

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de eliminar el libro: {txtTitulo.Text}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    repository.EliminarLibro(codigo);
                    CargarLibros();
                    LimpiarFormulario();
                    MessageBox.Show("Libro eliminado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar libro: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private bool ValidarDatos()
        {
            // Validar código
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El código es obligatorio", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (!int.TryParse(txtCodigo.Text, out int codigo))
            {
                MessageBox.Show("El código debe ser un número válido", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            // Validar título
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("El título es obligatorio", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                return false;
            }

            // Validar autor
            if (string.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("El autor es obligatorio", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAutor.Focus();
                return false;
            }

            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                txtCodigo.Text = row.Cells["codigo"].Value.ToString();
                txtTitulo.Text = row.Cells["titulo"].Value.ToString();
                txtAutor.Text = row.Cells["autor"].Value?.ToString() ?? "";

                txtCodigo.Enabled = false;
                btnAgregar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }
    }
}
