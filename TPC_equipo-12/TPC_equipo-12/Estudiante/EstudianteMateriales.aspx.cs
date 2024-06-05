using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_equipo_12
{
    public partial class EstudianteMateriales : System.Web.UI.Page
    {
        public List<MaterialLeccion> listaMateriales = new List<MaterialLeccion>();
        public MaterialNegocio materialNegocio = new MaterialNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaMateriales = materialNegocio.ListarMateriales((int)Session["IDLeccion"]);
                Session.Add("ListaMateriales", listaMateriales);
                rptMateriales.DataSource = listaMateriales;
                rptMateriales.DataBind();
            }
        }
    }
}