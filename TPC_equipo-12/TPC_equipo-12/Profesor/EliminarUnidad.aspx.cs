using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace TPC_equipo_12
{
    public partial class EliminarUnidad : System.Web.UI.Page
    {
        public List<Unidad> listaUnidades = new List<Unidad>();
        public UnidadNegocio unidadNegocio = new UnidadNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                listaUnidades = unidadNegocio.ListarUnidades((int)Session["IDCursoProfesor"]);
                Session.Add("ListaUnidadesProfesor", listaUnidades);
                DropDownListNombreUnidad.DataSource = listaUnidades;
                DropDownListNombreUnidad.DataTextField = "Nombre";
                DropDownListNombreUnidad.DataValueField = "IDUnidad";
                DropDownListNombreUnidad.DataBind();
            }
        }

        protected void ButtonEliminarUnidad_Click(object sender, EventArgs e)
        {
            try
            {
                unidadNegocio.EliminarUnidad(Convert.ToInt32(DropDownListNombreUnidad.SelectedValue));
                Session["MensajeExito"] = "Unidad eliminada con éxito.";
                Response.Redirect("ProfesorUnidades.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorUnidades.aspx", false);
            }
        }

        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx", false);
        }

        protected void ButtonEstadoUnidad_Click(object sender, EventArgs e)
        {
            string estadoUnidad;
            try
            {
                estadoUnidad = unidadNegocio.visibilidadUnidad(Convert.ToInt32(DropDownListNombreUnidad.SelectedValue));
                Session["MensajeExito"] = "La unidad se ha " + estadoUnidad + " con exito.";
                Response.Redirect("ProfesorUnidades.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorUnidades.aspx", false);
            }
        }
    }
}