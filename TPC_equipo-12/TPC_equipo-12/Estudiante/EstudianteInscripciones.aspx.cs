using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;


namespace TPC_equipo_12
{
    public partial class EstudianteInscripciones : System.Web.UI.Page
    {
        InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
        Estudiante Estudiante = new Estudiante();
        List<InscripcionACurso> listaInscripciones = new List<InscripcionACurso>();
        NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
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
        protected void rptInscripciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                InscripcionACurso inscripcion = (InscripcionACurso)e.Item.DataItem;
                Button btnReinscribir = (Button)e.Item.FindControl("btnReinscribir");

                if (inscripcion.Estado == 'R')
                {
                    btnReinscribir.Visible = true;
                }
            }
        }
        protected void btnReinscribir_Click(object sender, EventArgs e)
        {
            Button btnReinscribir = (Button)sender;
            string[] args = btnReinscribir.CommandArgument.Split(',');

            int idInscripcion = Convert.ToInt32(args[0]);
            int idCurso = Convert.ToInt32(args[1]);
            
            inscripcionNegocio.reinscribir(idInscripcion);
            notificacionNegocio.AgregarNotificacionXInscripcion(idInscripcion, idCurso);
            Response.Redirect("EstudianteInscripciones.aspx");
            Session["MensajeError"] = "Reinscripción enviada.";
        }
       
    }
}