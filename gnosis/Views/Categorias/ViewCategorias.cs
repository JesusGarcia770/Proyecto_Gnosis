using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gnosis.Controllers.Categorias;

namespace gnosis.Views.Categorias
{
    public partial class ViewCategorias : Form
    {
        public ViewCategorias()
        {
            InitializeComponent();
            ControllerCategorias control = new ControllerCategorias(this);
        }
    }
}
