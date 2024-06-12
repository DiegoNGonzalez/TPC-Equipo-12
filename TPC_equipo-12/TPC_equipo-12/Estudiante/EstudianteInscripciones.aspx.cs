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
    public partial class EstudianteInscripciones : System.Web.UI.Page
    {
        InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
        Estudiante Estudiante = new Estudiante();
        List <InscripcionACurso> listaInscripciones = new List<InscripcionACurso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Estudiante = (Estudiante)Session["Estudiante"];
                listaInscripciones = inscripcionNegocio.listarInscripcionesXEstudiante(Estudiante.IDUsuario);
                rptInscripciones.DataSource = listaInscripciones;
                rptInscripciones.DataBind();

            }
        }
    }
}