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
    public partial class AgregarResenia : System.Web.UI.Page
    {
        public List <int> calificacion = new List<int> {0, 1, 2, 3, 4, 5 };
        public ReseniaNegocio reseniaNegocio = new ReseniaNegocio();
        
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
                ddlCalificacion.DataSource = calificacion;
                ddlCalificacion.DataBind();
            }
        }

        protected void btnAgregarResenia_Click(object sender, EventArgs e)
        {
            Resenia resenia = new Resenia();
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            resenia.IDCurso = (int)Session["IDCurso"];
            resenia.Estudiante = estudiante;
            resenia.Calificacion = Convert.ToInt32(ddlCalificacion.SelectedValue);
            resenia.Comentario = txtResenia.Text;
            resenia.FechaCreacion = DateTime.Now;
            reseniaNegocio.CrearResenia(resenia);
            Session["MensajeExito"] = "Reseña agregada con éxito!";
            Response.Redirect("EstudianteCursos.aspx");


        }
    }
}