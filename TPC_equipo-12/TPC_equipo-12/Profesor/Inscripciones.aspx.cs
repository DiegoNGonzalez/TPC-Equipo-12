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
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["MensajeExito"] != null)
                {
                    string msj = Session["MensajeExito"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $@"showMessage('{msj}', 'success');", true);
                    Session["MensajeExito"] = null;
                }
                if (Session["MensajeError"] != null)
                {
                    string msj = Session["MensajeError"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $@"showMessage('{msj}', 'error');", true);
                    Session["MensajeError"] = null;
                }
                if (Session["MensajeInfo"] != null)
                {
                    string msj = Session["MensajeInfo"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $@"showMessage('{msj}', 'info');", true);
                    Session["MensajeInfo"] = null;
                }

                inscripciones = inscripcionNegocio.listarInscripciones();
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
            Session.Add("inscripciones", inscripciones);
            Session["MensajeExito"] = "Inscripcion confirmada con exito";
            Response.Redirect("Inscripciones.aspx", false);
            //rptInscripciones.DataSource = inscripciones;
            //rptInscripciones.DataBind();




        }

        protected void btnEliminarInscripcion_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", @"<script>
            //            showMessage('Verifique su información, El usuario ya esta registrado!', 'success');
            //            setTimeout(function() {
            //            window.location.href = 'Inscripciones.aspx'; 
            //            }, 1500); 
            //            </script>", false); falta implementar funcionaldiad a este boton y script
        }

       
    }
}