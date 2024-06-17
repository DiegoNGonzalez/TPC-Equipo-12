using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace TPC_equipo_12
{
    public partial class ProfesorEstudiantes : System.Web.UI.Page
    {
        public List<Estudiante> listaEstudiantes = new List<Estudiante>();
        public EstudianteNegocio estudianteNegocio = new EstudianteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                listaEstudiantes = estudianteNegocio.ListarEstudiantes();
                Session.Add("listaEstudiantes", listaEstudiantes);
                rptEstudiantes.DataSource = listaEstudiantes;
                rptEstudiantes.DataBind();

            }
        }
    }
}