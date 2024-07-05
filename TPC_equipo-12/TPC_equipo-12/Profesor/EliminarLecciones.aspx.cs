using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace TPC_equipo_12
{
    public partial class EliminarLecciones : System.Web.UI.Page
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
                listaLecciones = leccionNegocio.ListarLecciones((int)Session["IDUnidadProfesor"]);
                Session.Add("ListaLeccionesProfesor", listaLecciones);
                DropDownListNombreLeccion.DataSource = listaLecciones;
                DropDownListNombreLeccion.DataTextField = "Nombre";
                DropDownListNombreLeccion.DataValueField = "IDLeccion";
                DropDownListNombreLeccion.DataBind();
            }
        }
        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorLecciones.aspx", false);
        }

        protected void ButtonEstadoLeccion_Click(object sender, EventArgs e)
        {
            string estadoLeccion;
            try
            {
                estadoLeccion = leccionNegocio.visibilidadLeccion(Convert.ToInt32(DropDownListNombreLeccion.SelectedValue));
                Session["MensajeExito"] = "La leccion se ha " + estadoLeccion + " con exito";
                Response.Redirect("ProfesorLecciones.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorLecciones.aspx", false);
            }
        }
    }
}