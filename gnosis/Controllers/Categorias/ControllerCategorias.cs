using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gnosis.Models.DAO;
using gnosis.Views.Categorias;

namespace gnosis.Controllers.Categorias
{
    internal class ControllerCategorias
    {
        ViewCategorias ObjVista;

        public ControllerCategorias(ViewCategorias vista)
        {
            ObjVista = vista;
            vista.Load += new EventHandler(CargaInicial);
            vista.btnGuardar.Click += new EventHandler(GuardarCategoria);
            vista.dgvCategorias.CellClick += new DataGridViewCellEventHandler(SeleccionarCategoria);
            vista.btnActualizar.Click += new EventHandler(ActualizarCategoria);
            vista.btnEliminar.Click += new EventHandler(EliminarCategoria);

        }

        void CargaInicial(object sender, EventArgs e)
        {
            LlenarDataGridCategorias();
        }

        void LlenarDataGridCategorias()
        {
            DAOCategorias daoCat = new DAOCategorias();
            DataSet ds = daoCat.ObtenerCategorias();
            ObjVista.dgvCategorias.DataSource = ds.Tables["tbCategory"];
        }


        void GuardarCategoria(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(ObjVista.txtNombre.Text.Trim())))
            {
                DAOCategorias DaoInsert = new DAOCategorias();
                DaoInsert.CategoryName = ObjVista.txtNombre.Text.Trim();
                DaoInsert.Description = ObjVista.txtDescripcion.Text.Trim();
                int retorno = DaoInsert.RegistrarCategoria();
                if (retorno == 1)
                {
                    MessageBox.Show("La categoria fue registrado exitosamente", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarDataGridCategorias();
                    LimpiarCampos();
                }
                else if (retorno == 0)
                {
                    MessageBox.Show("La categoria no pudo ser registrado", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Datos faltantes, complete el formulario con la información requerida", "Datos faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        void SeleccionarCategoria(Object sender, DataGridViewCellEventArgs e)
        {
             int pos = ObjVista.dgvCategorias.CurrentRow.Index;
             ObjVista.txtIdCategoria.Text = ObjVista.dgvCategorias[0, pos].Value.ToString();
             ObjVista.txtNombre.Text = ObjVista.dgvCategorias[1, pos].Value.ToString();
             ObjVista.txtDescripcion.Text = ObjVista.dgvCategorias[2, pos].Value.ToString();
        }

        void EliminarCategoria(object sender, EventArgs e)
        {
            int pos = ObjVista.dgvCategorias.CurrentRow.Index;
            DAOCategorias DaoDelete = new DAOCategorias();
            DaoDelete.CategoryId = int.Parse(ObjVista.txtIdCategoria.Text.Trim());
            int retorno = DaoDelete.EliminarRegistro();
            if (retorno == 1)
            {
                MessageBox.Show("La Categoria seleccionado fue eliminado", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarDataGridCategorias();
                LimpiarCampos();

            }
            else
            {
                MessageBox.Show("La Categoria seleccionado no pudo ser eliminado", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void LimpiarCampos()
        {
            ObjVista.txtNombre.Text = string.Empty;
            ObjVista.txtDescripcion.Text = string.Empty;
            ObjVista.txtIdCategoria.Text = string.Empty;
        }

        void ActualizarCategoria(object sender, EventArgs e)
        {

            DAOCategorias DaoUpdate = new DAOCategorias();
            DaoUpdate.CategoryId = int.Parse(ObjVista.txtIdCategoria.Text.Trim());
            DaoUpdate.CategoryName = ObjVista.txtNombre.Text.Trim();
            DaoUpdate.Description = ObjVista.txtDescripcion.Text.Trim();
            int retorno = DaoUpdate.ActualizarRegistro();
            if (retorno == 1)
            {
                MessageBox.Show("La categoria seleccionado fue actualizado", "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LlenarDataGridCategorias();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("La categoria seleccionado no pudo ser actualizado", "Proceso incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }


}
