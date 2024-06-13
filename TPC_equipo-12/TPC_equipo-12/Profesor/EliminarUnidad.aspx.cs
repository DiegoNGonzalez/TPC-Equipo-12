using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

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
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");
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
    }
}