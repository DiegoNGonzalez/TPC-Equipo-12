using System;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        
        public void VerificarMensaje()
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
        }
    }
}