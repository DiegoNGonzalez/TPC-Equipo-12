using Dominio;
using Negocio;
using System;
using System.Collections.Generic;


namespace TPC_equipo_12
{
    public partial class EstudianteInscripciones : System.Web.UI.Page
    {
        InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
        Estudiante Estudiante = new Estudiante();
        List<InscripcionACurso> listaInscripciones = new List<InscripcionACurso>();
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

                Estudiante = (Estudiante)Session["estudiante"];
                listaInscripciones = inscripcionNegocio.listarInscripcionesXEstudiante(Estudiante.IDUsuario);
                rptInscripciones.DataSource = listaInscripciones;
                rptInscripciones.DataBind();

            }
        }
    }
}