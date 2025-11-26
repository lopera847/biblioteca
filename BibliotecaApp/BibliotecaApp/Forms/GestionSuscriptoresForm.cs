using BibliotecaApp.Data;
using BibliotecaApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace BibliotecaApp.Forms
{
    public partial class GestionSuscriptoresForm : Form
    {
        private BibliotecaRepository repository;
        private DataGridView dataGridView;
        private Button btnAgregar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnCerrar;
        private Button btnLimpiar;
        private Label label1;
        private TextBox txtDocumento;
        private TextBox txtNombre;
        private TextBox txtDireccion;
        private Label lblDocumento;
        private Label lblNombre;
        private Label lblDireccion;
        private GroupBox groupBoxDatos;

        public GestionSuscriptoresForm()
        {
            InitializeComponent();
            repository = new BibliotecaRepository();
            CargarSuscriptores();
            LimpiarFormulario();
        }

        private void InitializeComponent()
        {
            // Configurar el formulario principal
            this.SuspendLayout();
            this.Text = "Gestión de Suscriptores";
            this.Size = new Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // label1 - Título
            this.label1 = new Label();
            this.label1.Text = "GESTIÓN DE SUSCRIPTORES";
            this.label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.label1.ForeColor = Color.DarkBlue;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 20);
            this.label1.Size = new Size(300, 24);

            // groupBoxDatos
            this.groupBoxDatos = new GroupBox();
            this.groupBoxDatos.Text = "Datos del Suscriptor";
            this.groupBoxDatos.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.groupBoxDatos.Location = new Point(20, 60);
            this.groupBoxDatos.Size = new Size(800, 120);
            this.groupBoxDatos.BackColor = Color.White;

            // lblDocumento
            this.lblDocumento = new Label();
            this.lblDocumento.Text = "Documento:";
            this.lblDocumento.Location = new Point(20, 30);
            this.lblDocumento.Size = new Size(80, 20);
            this.lblDocumento.Font = new Font("Microsoft Sans Serif", 9F);

            // txtDocumento
            this.txtDocumento = new TextBox();
            this.txtDocumento.Location = new Point(100, 28);
            this.txtDocumento.Size = new Size(150, 20);
            this.txtDocumento.Font = new Font("Microsoft Sans Serif", 9F);

            // lblNombre
            this.lblNombre = new Label();
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new Point(270, 30);
            this.lblNombre.Size = new Size(60, 20);
            this.lblNombre.Font = new Font("Microsoft Sans Serif", 9F);

            // txtNombre
            this.txtNombre = new TextBox();
            this.txtNombre.Location = new Point(340, 28);
            this.txtNombre.Size = new Size(250, 20);
            this.txtNombre.Font = new Font("Microsoft Sans Serif", 9F);

            // lblDireccion
            this.lblDireccion = new Label();
            this.lblDireccion.Text = "Dirección:";
            this.lblDireccion.Location = new Point(20, 70);
            this.lblDireccion.Size = new Size(70, 20);
            this.lblDireccion.Font = new Font("Microsoft Sans Serif", 9F);

            // txtDireccion
            this.txtDireccion = new TextBox();
            this.txtDireccion.Location = new Point(100, 68);
            this.txtDireccion.Size = new Size(490, 20);
            this.txtDireccion.Font = new Font("Microsoft Sans Serif", 9F);

            // btnLimpiar
            this.btnLimpiar = new Button();
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Location = new Point(610, 25);
            this.btnLimpiar.Size = new Size(80, 30);
            this.btnLimpiar.BackColor = Color.LightGray;
            this.btnLimpiar.FlatStyle = FlatStyle.Flat;
            this.btnLimpiar.Click += new EventHandler(this.btnLimpiar_Click);

            // Agregar controles al groupBox
            this.groupBoxDatos.Controls.Add(this.lblDocumento);
            this.groupBoxDatos.Controls.Add(this.txtDocumento);
            this.groupBoxDatos.Controls.Add(this.lblNombre);
            this.groupBoxDatos.Controls.Add(this.txtNombre);
            this.groupBoxDatos.Controls.Add(this.lblDireccion);
            this.groupBoxDatos.Controls.Add(this.txtDireccion);
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

        private void CargarSuscriptores()
        {
            try
            {
                dataGridView.DataSource = repository.ObtenerSuscriptores();
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los suscriptores: {ex.Message}", "Error",
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
            txtDocumento.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtDocumento.Enabled = true;
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
                    Suscriptor suscriptor = new Suscriptor
                    {
                        documento = txtDocumento.Text.Trim(),
                        nombre = txtNombre.Text.Trim(),
                        direccion = txtDireccion.Text.Trim()
                    };

                    repository.AgregarSuscriptor(suscriptor);
                    CargarSuscriptores();
                    LimpiarFormulario();
                    MessageBox.Show("Suscriptor agregado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar suscriptor: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    Suscriptor suscriptor = new Suscriptor
                    {
                        documento = txtDocumento.Text.Trim(),
                        nombre = txtNombre.Text.Trim(),
                        direccion = txtDireccion.Text.Trim()
                    };

                    repository.ActualizarSuscriptor(suscriptor);
                    CargarSuscriptores();
                    LimpiarFormulario();
                    MessageBox.Show("Suscriptor actualizado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar suscriptor: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string documento = txtDocumento.Text.Trim();

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de eliminar al suscriptor: {txtNombre.Text}?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    repository.EliminarSuscriptor(documento);
                    CargarSuscriptores();
                    LimpiarFormulario();
                    MessageBox.Show("Suscriptor eliminado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar suscriptor: {ex.Message}", "Error",
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
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("El documento es obligatorio", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                txtDocumento.Text = row.Cells["documento"].Value.ToString();
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtDireccion.Text = row.Cells["direccion"].Value?.ToString() ?? "";

                txtDocumento.Enabled = false;
                btnAgregar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }
    }
}
