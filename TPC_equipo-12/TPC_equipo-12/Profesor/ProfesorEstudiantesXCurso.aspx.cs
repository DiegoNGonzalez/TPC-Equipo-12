using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorEstudiantesXCurso : System.Web.UI.Page
    {
        public List<InscripcionACurso> inscripciones = new List<InscripcionACurso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                inscripciones = inscripcionNegocio.listarInscripcionesXCurso((int)Session["IDCursoProfesor"]);
                if (inscripciones.Count == 0)
                {
                    Session["MensajeInfo"] = "No hay inscripciones en este curso.";
                    Response.Redirect("ProfesorUnidades.aspx");
                }
                else
                {
                    Session.Add("inscripciones", inscripciones);
                    rptInscripciones.DataSource = inscripciones;
                    rptInscripciones.DataBind();

                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx");
        }
    }
}