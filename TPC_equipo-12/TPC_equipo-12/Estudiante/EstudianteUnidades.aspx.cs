using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteUnidades : System.Web.UI.Page
    {
        public List<Unidad> listaUnidades = new List<Unidad>();
        public UnidadNegocio unidadNegocio = new UnidadNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();
                listaUnidades = unidadNegocio.ListarUnidades((int)Session["IDCurso"]);
                listaUnidades = listaUnidades.FindAll(m => m.Estado);
                Session.Add("ListaUnidades", listaUnidades);
                rptUnidades.DataSource = listaUnidades;
                rptUnidades.DataBind();

                bool hayUnidadesActivas = listaUnidades.Any(m => m.Estado);
                //lblMensajeInactivo.Visible = !hayUnidadesActivas;
            }
        }

        protected void ButtonVerLecciones_Command(object sender, CommandEventArgs e)
        {
            int IdUnidad = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDUnidad", IdUnidad);
            Response.Redirect("EstudianteLecciones.aspx");
        }

        protected void ButtonBackCursos_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstudianteCursos.aspx");
        }

        protected void btnReseniaCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarResenia.aspx");
        }
    }
}