using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteLecciones : System.Web.UI.Page
    {
        public List<Leccion> listaLecciones = new List<Leccion>();
        public LeccionNegocio leccionNegocio = new LeccionNegocio();
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

                listaLecciones = leccionNegocio.ListarLecciones((int)Session["IDUnidad"]);
                listaLecciones = listaLecciones.FindAll(m => m.Estado);
                Session.Add("ListaLecciones", listaLecciones);
                rptLecciones.DataSource = listaLecciones;
                rptLecciones.DataBind();


            }

        }
        protected void ButtonVerMateriales_Command(object sender, CommandEventArgs e)
        {
            int IdLeccion = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDLeccion", IdLeccion);
            Response.Redirect("EstudianteMateriales.aspx");
        }

        protected void ButtonBackUnidad_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstudianteUnidades.aspx");
        }
    }
}