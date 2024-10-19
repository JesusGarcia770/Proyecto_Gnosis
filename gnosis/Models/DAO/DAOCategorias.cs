using gnosis.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gnosis.Models.DAO
{
    internal class DAOCategorias:DTOCategorias
    {
        SqlCommand command = new SqlCommand();

        public int RegistrarCategoria()
        {
            try
            {
                command.Connection = getConnection();

                string QueryInsert = "INSERT INTO tbCategory VALUES (@param1, @param2)";
                SqlCommand cmdInsert = new SqlCommand(QueryInsert, command.Connection);
                cmdInsert.Parameters.AddWithValue("param1", CategoryName);
                cmdInsert.Parameters.AddWithValue("param2", Description);
                return cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"{ex.Message} No se pudo registrar la Categoria, verifique su conexión a internet o que los servicios esten activos", "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                command.Connection.Close();
            }
        }
       
        public DataSet ObtenerCategorias()
        {
            try
            {
                command.Connection = getConnection();
                string Query = "select * From tbCategory";
                SqlCommand cmdSelect = new SqlCommand(Query, command.Connection);
                cmdSelect.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmdSelect);
                DataSet ds = new DataSet();
                adp.Fill(ds, "tbCategory");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} Error al obtener la nomina de Categorias, verifique su conexión a internet o que el acceso al servidor o base de datos esten activos", "Error de ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public int EliminarRegistro()
        {
            try
            {
                command.Connection = getConnection();

                string QueryDelete = "DELETE tbCategory WHERE categoryId = @param1";
                SqlCommand cmdDelete = new SqlCommand(QueryDelete, command.Connection);
                cmdDelete.Parameters.AddWithValue("param1", CategoryId);
                return cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} No se pudo eliminar la información de la categoria, verifique su conexión a internet o que los servicios esten activos", "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public int ActualizarRegistro()
        {
            try
            {
                command.Connection = getConnection();
                string QueryUpdate = "Update tbCategory Set categoryName = @param2, Description = @param3 Where categoryId = @param1";
                SqlCommand cmdUpdate = new SqlCommand(QueryUpdate, command.Connection);
                cmdUpdate.Parameters.AddWithValue("param1", CategoryId);
                cmdUpdate.Parameters.AddWithValue("param2", CategoryName);
                cmdUpdate.Parameters.AddWithValue("param3", Description);
                return cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} No se pudo actualizar la información de la categoria, verifique su conexión a internet o que los servicios esten activos", "Error de inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally { command.Connection.Close(); }
        }
    }
}
