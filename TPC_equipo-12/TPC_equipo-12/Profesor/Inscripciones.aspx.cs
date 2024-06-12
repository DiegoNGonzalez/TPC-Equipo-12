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
    public partial class Inscripciones : System.Web.UI.Page
    {
        public List<InscripcionACurso> inscripciones = new List<InscripcionACurso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inscripciones= inscripcionNegocio.listarInscripciones();
                Session.Add("inscripciones", inscripciones);
                rptInscripciones.DataSource = inscripciones;
                rptInscripciones.DataBind();
            }
        }

        protected void btnAceptarInscripcion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idInscripcion = Convert.ToInt32(btn.CommandArgument); 
            InscripcionACurso aux = inscripcionNegocio.BuscarInscripcion(idInscripcion);
            inscripcionNegocio.ConfirmarInscripcion(aux);

            inscripciones = inscripcionNegocio.listarInscripciones();
            rptInscripciones.DataSource = inscripciones;
            rptInscripciones.DataBind();

            
        }

        protected void btnEliminarInscripcion_Click(object sender, EventArgs e)
        {

        }

       
    }
}