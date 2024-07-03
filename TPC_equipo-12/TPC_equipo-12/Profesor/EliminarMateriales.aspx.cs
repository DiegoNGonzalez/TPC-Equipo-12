using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace TPC_equipo_12
{
    public partial class EliminarMateriales : System.Web.UI.Page
    {
        public List<MaterialLeccion> listaMateriales = new List<MaterialLeccion>();
        public MaterialNegocio materialNegocio = new MaterialNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                listaMateriales = materialNegocio.ListarMateriales((int)Session["IDLeccionProfesor"]);
                Session.Add("ListaMaterialesProfesor", listaMateriales);
                DropDownListNombreMaterial.DataSource = listaMateriales;
                DropDownListNombreMaterial.DataTextField = "Nombre";
                DropDownListNombreMaterial.DataValueField = "IDMaterial";
                DropDownListNombreMaterial.DataBind();
            }
        }

        protected void ButtonEstadoMaterial_Click(object sender, EventArgs e)
        {
            string estadoMaterial;
            try
            {
                estadoMaterial = materialNegocio.visibilidadMaterial(Convert.ToInt32(DropDownListNombreMaterial.SelectedValue));
                Session["MensajeExito"] = "Se ha " + estadoMaterial + " con exito.";
                Response.Redirect("ProfesorMateriales.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorMateriales.aspx", false);
            }
        }
        //protected void ButtonEliminarMaterial_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        materialNegocio.EliminarMaterial(Convert.ToInt32(DropDownListNombreMaterial.SelectedValue));
        //        Session["MensajeExito"] = "Material eliminado con éxito.";
        //        Response.Redirect("ProfesorMateriales.aspx", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["MensajeError"] = ex.ToString();
        //        Response.Redirect("ProfesorMateriales.aspx", false);
        //    }
        //}

        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorMateriales.aspx", false);
        }

    }
}