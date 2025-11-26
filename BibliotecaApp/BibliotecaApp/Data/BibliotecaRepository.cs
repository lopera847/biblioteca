using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class BibliotecaRepository
    {
        private string connectionString;

        public BibliotecaRepository()
        {
            connectionString = @"Data Source=.;Initial Catalog=Biblioteca;Integrated Security=True";
        }

        // Métodos para Usuarios
        public bool ValidarUsuario(string usuario, string contraseña)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM usuarios WHERE nombre = @Usuario AND password = @Contraseña";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Contraseña", contraseña);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Métodos para Libros
        public void AgregarLibro(Libro libro)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO libros (codigo, titulo, autor) 
                               VALUES (@Codigo, @Titulo, @Autor)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", libro.codigo);
                    command.Parameters.AddWithValue("@Titulo", libro.titulo);
                    command.Parameters.AddWithValue("@Autor", libro.autor);
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerLibros()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM libros ORDER BY codigo";
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public void ActualizarLibro(Libro libro)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE libros SET titulo=@Titulo, autor=@Autor 
                               WHERE codigo=@Codigo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", libro.codigo);
                    command.Parameters.AddWithValue("@Titulo", libro.titulo);
                    command.Parameters.AddWithValue("@Autor", libro.autor);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarLibro(int codigo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM libros WHERE codigo=@Codigo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Métodos para Suscriptores
        public void AgregarSuscriptor(Suscriptor suscriptor)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO suscriptores (documento, nombre, direccion) 
                               VALUES (@Documento, @Nombre, @Direccion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Documento", suscriptor.documento);
                    command.Parameters.AddWithValue("@Nombre", suscriptor.nombre);
                    command.Parameters.AddWithValue("@Direccion", suscriptor.direccion);
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerSuscriptores()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM suscriptores ORDER BY nombre";
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public void ActualizarSuscriptor(Suscriptor suscriptor)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE suscriptores SET nombre=@Nombre, direccion=@Direccion 
                               WHERE documento=@Documento";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Documento", suscriptor.documento);
                    command.Parameters.AddWithValue("@Nombre", suscriptor.nombre);
                    command.Parameters.AddWithValue("@Direccion", suscriptor.direccion);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarSuscriptor(string documento)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM suscriptores WHERE documento=@Documento";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Documento", documento);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Métodos para Préstamos
        public void AgregarPrestamo(Prestamo prestamo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO prestamos (docsuscriptor, codigolibro, fechaprestamo, fechadevolucion) 
                               VALUES (@DocSuscriptor, @CodigoLibro, @FechaPrestamo, @FechaDevolucion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocSuscriptor", prestamo.docsuscriptor);
                    command.Parameters.AddWithValue("@CodigoLibro", prestamo.codigolibro);
                    command.Parameters.AddWithValue("@FechaPrestamo", prestamo.fechaprestamo);
                    command.Parameters.AddWithValue("@FechaDevolucion",
                        prestamo.fechadevolucion.HasValue ? (object)prestamo.fechadevolucion.Value : DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerPrestamosCompletos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT p.docsuscriptor, s.nombre as NombreSuscriptor, 
                           p.codigolibro, l.titulo as TituloLibro, 
                           p.fechaprestamo, p.fechadevolucion
                    FROM prestamos p
                    INNER JOIN suscriptores s ON p.docsuscriptor = s.documento
                    INNER JOIN libros l ON p.codigolibro = l.codigo
                    ORDER BY p.fechaprestamo DESC";

                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable ObtenerSuscriptoresConPrestamosVigentes()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT DISTINCT s.nombre
                    FROM suscriptores s
                    INNER JOIN prestamos p ON s.documento = p.docsuscriptor
                    WHERE p.fechadevolucion IS NULL";

                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable ObtenerPrestamos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM prestamos ORDER BY fechaprestamo DESC";
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public void ActualizarPrestamo(Prestamo prestamo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"UPDATE prestamos SET fechadevolucion=@FechaDevolucion 
                               WHERE docsuscriptor=@DocSuscriptor AND codigolibro=@CodigoLibro 
                               AND fechaprestamo=@FechaPrestamo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocSuscriptor", prestamo.docsuscriptor);
                    command.Parameters.AddWithValue("@CodigoLibro", prestamo.codigolibro);
                    command.Parameters.AddWithValue("@FechaPrestamo", prestamo.fechaprestamo);
                    command.Parameters.AddWithValue("@FechaDevolucion",
                        prestamo.fechadevolucion.HasValue ? (object)prestamo.fechadevolucion.Value : DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EliminarPrestamo(string docSuscriptor, int codigoLibro, DateTime fechaPrestamo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM prestamos 
                               WHERE docsuscriptor=@DocSuscriptor AND codigolibro=@CodigoLibro 
                               AND fechaprestamo=@FechaPrestamo";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocSuscriptor", docSuscriptor);
                    command.Parameters.AddWithValue("@CodigoLibro", codigoLibro);
                    command.Parameters.AddWithValue("@FechaPrestamo", fechaPrestamo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool ProbarConexion()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
               

                System.Diagnostics.Debug.WriteLine($"Error de conexión: {ex.Message}");
                return false;
            }
        }
    }
}
