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
    public partial class ProfesorLecciones : System.Web.UI.Page
    {
        public List<Leccion> listaLecciones = new List<Leccion>();
        public LeccionNegocio leccionNegocio = new LeccionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["MensajeExito"] != null)
                {
                    string msj = Session["MensajeExito"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $@"showMessage('{msj}', 'success');", true);
                    Session["MensajeExito"] = null;
                }
                if (Session["MensajeError"] != null)
                {
                    string msj = Session["MensajeError"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $@"showMessage('{msj}', 'error');", true);
                    Session["MensajeError"] = null;
                }
                if (Session["MensajeInfo"] != null)
                {
                    string msj = Session["MensajeInfo"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $@"showMessage('{msj}', 'info');", true);
                    Session["MensajeInfo"] = null;
                }

                listaLecciones = leccionNegocio.ListarLecciones((int)Session["IDUnidadProfesor"]);
                Session.Add("ListaLeccionesProfesor", listaLecciones);
                rptLeccionesProf.DataSource = listaLecciones;
                rptLeccionesProf.DataBind();
            }
        }

        protected void ButtonBackUnidadProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx");
        }

        protected void ButtonVerMaterialesProf_Command(object sender, CommandEventArgs e)
        {
            int IdLeccion = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDLeccionProfesor", IdLeccion);
            Response.Redirect("ProfesorMateriales.aspx");
        }

        protected void ButtonCrearLeccionProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarLecciones.aspx");
        }

        protected void ButtonEliminarLeccionProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("EliminarLecciones.aspx");
        }

        protected void ButtonModificarLeccionProf_Command(object sender, CommandEventArgs e)
        {
            int IdLeccion = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDLeccionProfesor", IdLeccion);
            Response.Redirect("AgregarLecciones.aspx?IdLeccion=" + IdLeccion);
        }
    }
}