using BibliotecaApp.Data;
using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Forms
{
    public partial class GestionPrestamosForm : Form
    {
        private BibliotecaRepository repository;
        private DataGridView dataGridView;
        private Button btnAgregar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnCerrar;
        private Label label1;
        private ComboBox cmbSuscriptor;
        private ComboBox cmbLibro;
        private DateTimePicker dtpFechaPrestamo;
        private DateTimePicker dtpFechaDevolucion;
        private CheckBox chkDevuelto;
        private Label lblSuscriptor;
        private Label lblLibro;
        private Label lblFechaPrestamo;
        private Label lblFechaDevolucion;
        private GroupBox groupBoxDatos;
        private Button btnLimpiar;

        public GestionPrestamosForm()
        {
            InitializeComponent();
            repository = new BibliotecaRepository();
            CargarPrestamos();
            CargarCombos();
            LimpiarFormulario();
        }

        private void CargarPrestamos()
        {
            try
            {
                dataGridView.DataSource = repository.ObtenerPrestamos();
                AjustarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los préstamos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar suscriptores
                var suscriptores = repository.ObtenerSuscriptores();
                cmbSuscriptor.DataSource = suscriptores;
                cmbSuscriptor.DisplayMember = "nombre";
                cmbSuscriptor.ValueMember = "documento";

                // Cargar libros
                var libros = repository.ObtenerLibros();
                cmbLibro.DataSource = libros;
                cmbLibro.DisplayMember = "titulo";
                cmbLibro.ValueMember = "codigo";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarColumnasDataGridView()
        {
            if (dataGridView.Columns.Count > 0)
            {
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    switch (column.Name)
                    {
                        case "docsuscriptor":
                            column.HeaderText = "Documento Suscriptor";
                            break;
                        case "codigolibro":
                            column.HeaderText = "Código Libro";
                            break;
                        case "fechaprestamo":
                            column.HeaderText = "Fecha Préstamo";
                            break;
                        case "fechadevolucion":
                            column.HeaderText = "Fecha Devolución";
                            break;
                    }
                }
            }
        }

        private void LimpiarFormulario()
        {
            if (cmbSuscriptor.Items.Count > 0) cmbSuscriptor.SelectedIndex = 0;
            if (cmbLibro.Items.Count > 0) cmbLibro.SelectedIndex = 0;
            dtpFechaPrestamo.Value = DateTime.Today;
            dtpFechaDevolucion.Value = DateTime.Today;
            chkDevuelto.Checked = false;
            dtpFechaDevolucion.Enabled = false;

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
                    Prestamo prestamo = new Prestamo
                    {
                        docsuscriptor = cmbSuscriptor.SelectedValue.ToString(),
                        codigolibro = (int)cmbLibro.SelectedValue,
                        fechaprestamo = dtpFechaPrestamo.Value,
                        fechadevolucion = chkDevuelto.Checked ? dtpFechaDevolucion.Value : (DateTime?)null
                    };

                    repository.AgregarPrestamo(prestamo);
                    CargarPrestamos();
                    LimpiarFormulario();
                    MessageBox.Show("Préstamo agregado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar préstamo: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0 && ValidarDatos())
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];

                    Prestamo prestamo = new Prestamo
                    {
                        docsuscriptor = cmbSuscriptor.SelectedValue.ToString(),
                        codigolibro = (int)cmbLibro.SelectedValue,
                        fechaprestamo = dtpFechaPrestamo.Value,
                        fechadevolucion = chkDevuelto.Checked ? dtpFechaDevolucion.Value : (DateTime?)null
                    };

                    repository.ActualizarPrestamo(prestamo);
                    CargarPrestamos();
                    LimpiarFormulario();
                    MessageBox.Show("Préstamo actualizado correctamente", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar préstamo: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    string docSuscriptor = row.Cells["docsuscriptor"].Value.ToString();
                    int codigoLibro = (int)row.Cells["codigolibro"].Value;
                    DateTime fechaPrestamo = Convert.ToDateTime(row.Cells["fechaprestamo"].Value);

                    DialogResult result = MessageBox.Show(
                        "¿Está seguro de eliminar este préstamo?",
                        "Confirmar Eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        repository.EliminarPrestamo(docSuscriptor, codigoLibro, fechaPrestamo);
                        CargarPrestamos();
                        LimpiarFormulario();
                        MessageBox.Show("Préstamo eliminado correctamente", "Éxito",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar préstamo: {ex.Message}", "Error",
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
            if (cmbSuscriptor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un suscriptor", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSuscriptor.Focus();
                return false;
            }

            if (cmbLibro.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un libro", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLibro.Focus();
                return false;
            }

            if (chkDevuelto.Checked && dtpFechaDevolucion.Value < dtpFechaPrestamo.Value)
            {
                MessageBox.Show("La fecha de devolución no puede ser anterior a la fecha de préstamo",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaDevolucion.Focus();
                return false;
            }

            return true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];

                // Seleccionar suscriptor
                foreach (DataRowView item in cmbSuscriptor.Items)
                {
                    if (item["documento"].ToString() == row.Cells["docsuscriptor"].Value.ToString())
                    {
                        cmbSuscriptor.SelectedItem = item;
                        break;
                    }
                }

                // Seleccionar libro
                foreach (DataRowView item in cmbLibro.Items)
                {
                    if (Convert.ToInt32(item["codigo"]) == (int)row.Cells["codigolibro"].Value)
                    {
                        cmbLibro.SelectedItem = item;
                        break;
                    }
                }

                dtpFechaPrestamo.Value = Convert.ToDateTime(row.Cells["fechaprestamo"].Value);

                if (row.Cells["fechadevolucion"].Value != DBNull.Value)
                {
                    chkDevuelto.Checked = true;
                    dtpFechaDevolucion.Value = Convert.ToDateTime(row.Cells["fechadevolucion"].Value);
                }
                else
                {
                    chkDevuelto.Checked = false;
                    dtpFechaDevolucion.Value = DateTime.Today;
                }

                btnAgregar.Enabled = false;
                btnActualizar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void chkDevuelto_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaDevolucion.Enabled = chkDevuelto.Checked;
        }

        private void InitializeComponent()
        {
            this.dataGridView = new DataGridView();
            this.btnAgregar = new Button();
            this.btnActualizar = new Button();
            this.btnEliminar = new Button();
            this.btnCerrar = new Button();
            this.label1 = new Label();
            this.groupBoxDatos = new GroupBox();
            this.chkDevuelto = new CheckBox();
            this.dtpFechaDevolucion = new DateTimePicker();
            this.dtpFechaPrestamo = new DateTimePicker();
            this.cmbLibro = new ComboBox();
            this.cmbSuscriptor = new ComboBox();
            this.lblFechaDevolucion = new Label();
            this.lblFechaPrestamo = new Label();
            this.lblLibro = new Label();
            this.lblSuscriptor = new Label();
            this.btnLimpiar = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBoxDatos.SuspendLayout();
            this.SuspendLayout();

            // dataGridView
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 200);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(760, 230);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.SelectionChanged += new EventHandler(this.dataGridView_SelectionChanged);

            // groupBoxDatos
            this.groupBoxDatos.Controls.Add(this.btnLimpiar);
            this.groupBoxDatos.Controls.Add(this.chkDevuelto);
            this.groupBoxDatos.Controls.Add(this.dtpFechaDevolucion);
            this.groupBoxDatos.Controls.Add(this.dtpFechaPrestamo);
            this.groupBoxDatos.Controls.Add(this.cmbLibro);
            this.groupBoxDatos.Controls.Add(this.cmbSuscriptor);
            this.groupBoxDatos.Controls.Add(this.lblFechaDevolucion);
            this.groupBoxDatos.Controls.Add(this.lblFechaPrestamo);
            this.groupBoxDatos.Controls.Add(this.lblLibro);
            this.groupBoxDatos.Controls.Add(this.lblSuscriptor);
            this.groupBoxDatos.Location = new System.Drawing.Point(12, 40);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Size = new System.Drawing.Size(760, 150);
            this.groupBoxDatos.TabIndex = 0;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "Datos del Préstamo";

            // lblSuscriptor
            this.lblSuscriptor.AutoSize = true;
            this.lblSuscriptor.Location = new System.Drawing.Point(20, 30);
            this.lblSuscriptor.Name = "lblSuscriptor";
            this.lblSuscriptor.Size = new System.Drawing.Size(57, 13);
            this.lblSuscriptor.Text = "Suscriptor:";

            // cmbSuscriptor
            this.cmbSuscriptor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSuscriptor.FormattingEnabled = true;
            this.cmbSuscriptor.Location = new System.Drawing.Point(90, 27);
            this.cmbSuscriptor.Name = "cmbSuscriptor";
            this.cmbSuscriptor.Size = new System.Drawing.Size(250, 21);
            this.cmbSuscriptor.TabIndex = 0;

            // lblLibro
            this.lblLibro.AutoSize = true;
            this.lblLibro.Location = new System.Drawing.Point(360, 30);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.Size = new System.Drawing.Size(33, 13);
            this.lblLibro.Text = "Libro:";

            // cmbLibro
            this.cmbLibro.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLibro.FormattingEnabled = true;
            this.cmbLibro.Location = new System.Drawing.Point(400, 27);
            this.cmbLibro.Name = "cmbLibro";
            this.cmbLibro.Size = new System.Drawing.Size(250, 21);
            this.cmbLibro.TabIndex = 1;

            // lblFechaPrestamo
            this.lblFechaPrestamo.AutoSize = true;
            this.lblFechaPrestamo.Location = new System.Drawing.Point(20, 70);
            this.lblFechaPrestamo.Name = "lblFechaPrestamo";
            this.lblFechaPrestamo.Size = new System.Drawing.Size(98, 13);
            this.lblFechaPrestamo.Text = "Fecha de Préstamo:";

            // dtpFechaPrestamo
            this.dtpFechaPrestamo.Format = DateTimePickerFormat.Short;
            this.dtpFechaPrestamo.Location = new System.Drawing.Point(130, 67);
            this.dtpFechaPrestamo.Name = "dtpFechaPrestamo";
            this.dtpFechaPrestamo.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaPrestamo.TabIndex = 2;

            // lblFechaDevolucion
            this.lblFechaDevolucion.AutoSize = true;
            this.lblFechaDevolucion.Location = new System.Drawing.Point(270, 70);
            this.lblFechaDevolucion.Name = "lblFechaDevolucion";
            this.lblFechaDevolucion.Size = new System.Drawing.Size(108, 13);
            this.lblFechaDevolucion.Text = "Fecha de Devolución:";

            // dtpFechaDevolucion
            this.dtpFechaDevolucion.Enabled = false;
            this.dtpFechaDevolucion.Format = DateTimePickerFormat.Short;
            this.dtpFechaDevolucion.Location = new System.Drawing.Point(390, 67);
            this.dtpFechaDevolucion.Name = "dtpFechaDevolucion";
            this.dtpFechaDevolucion.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaDevolucion.TabIndex = 3;

            // chkDevuelto
            this.chkDevuelto.AutoSize = true;
            this.chkDevuelto.Location = new System.Drawing.Point(530, 70);
            this.chkDevuelto.Name = "chkDevuelto";
            this.chkDevuelto.Size = new System.Drawing.Size(70, 17);
            this.chkDevuelto.TabIndex = 4;
            this.chkDevuelto.Text = "Devuelto";
            this.chkDevuelto.UseVisualStyleBackColor = true;
            this.chkDevuelto.CheckedChanged += new EventHandler(this.chkDevuelto_CheckedChanged);

            // btnLimpiar
            this.btnLimpiar.Location = new System.Drawing.Point(620, 25);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new EventHandler(this.btnLimpiar_Click);

            // btnAgregar
            this.btnAgregar.BackColor = System.Drawing.Color.Green;
            this.btnAgregar.FlatStyle = FlatStyle.Flat;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(12, 440);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 35);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new EventHandler(this.btnAgregar_Click);

            // btnActualizar
            this.btnActualizar.BackColor = System.Drawing.Color.Blue;
            this.btnActualizar.FlatStyle = FlatStyle.Flat;
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(120, 440);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 35);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new EventHandler(this.btnActualizar_Click);

            // btnEliminar
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatStyle = FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(228, 440);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 35);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new EventHandler(this.btnEliminar_Click);

            // btnCerrar
            this.btnCerrar.BackColor = System.Drawing.Color.Gray;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(672, 440);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 35);
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new EventHandler(this.btnCerrar_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Gestión de Préstamos";

            // GestionPrestamosForm
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(784, 491);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBoxDatos);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GestionPrestamosForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Préstamos - Sistema de Biblioteca";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBoxDatos.ResumeLayout(false);
            this.groupBoxDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
