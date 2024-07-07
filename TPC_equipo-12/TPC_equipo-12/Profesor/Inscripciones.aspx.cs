using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class Inscripciones : System.Web.UI.Page
    {
        public List<InscripcionACurso> inscripciones = new List<InscripcionACurso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                ProfesorMasterPage master = (ProfesorMasterPage)Page.Master;
                master.VerificarMensaje();

                inscripciones = inscripcionNegocio.listarInscripciones();
                if (inscripciones.Count == 0)
                {
                    PanelInscripciones.Visible = false;
                    LabelNoHayInscripciones.Visible = true;
                }
                else
                {
                    PanelInscripciones.Visible = true;
                    LabelNoHayInscripciones.Visible = false;
                    Session.Add("inscripciones", inscripciones);
                    rptInscripciones.DataSource = inscripciones;
                    rptInscripciones.DataBind();
                }
            }
        }

        protected void btnAceptarInscripcion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idInscripcion = Convert.ToInt32(btn.CommandArgument);
            InscripcionACurso aux = inscripcionNegocio.BuscarInscripcion(idInscripcion);
            inscripcionNegocio.ConfirmarInscripcion(aux);
            int existeNotif=notificacionNegocio.buscarNotificacionXInscripcionXUsuario(aux.IDInscripcion, aux.Usuario.IDUsuario);
            if (existeNotif!=0)
            {
                notificacionNegocio.marcarComoNoLeidaYMensaje(existeNotif, "Reinscripción aceptada");
            }
            else
            {
                notificacionNegocio.NotificacionRespuestaInscripcion(aux, true);

            }

            inscripciones = inscripcionNegocio.listarInscripciones();
            Session.Add("inscripciones", inscripciones);
            Session["MensajeExito"] = "Inscripcion confirmada con exito";
            Response.Redirect("Inscripciones.aspx", false);

        }

        protected void btnEliminarInscripcion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idInscripcion = Convert.ToInt32(btn.CommandArgument);
            InscripcionACurso aux = inscripcionNegocio.BuscarInscripcion(idInscripcion);
            inscripcionNegocio.RechazarInscripcion(aux.IDInscripcion, 'R');
            notificacionNegocio.NotificacionRespuestaInscripcion(aux, false);
            inscripciones = inscripcionNegocio.listarInscripciones();
            Session.Add("inscripciones", inscripciones);
            Session["MensajeExito"] = "Inscripcion rechazada con exito";
            Response.Redirect("Inscripciones.aspx", false);
        }


    }
}