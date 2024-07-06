using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class VerResenias : System.Web.UI.Page
    {
        ReseniaNegocio reseniaNegocio = new ReseniaNegocio();
        List<Resenia> resenias = new List<Resenia>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                resenias = reseniaNegocio.ListarReseniasXCurso(idCurso);
                if (resenias.Count == 0)
                {
                    
                    lblResenia.Style.Add("display", "none");

                    LabelNoHayResenias.Visible = true;
                    LabelNoHayResenias.Text = "No hay reseñas de este curso.";
                }
                else
                {
                    RepeaterResenias.DataSource = resenias;
                    RepeaterResenias.DataBind();

                }



            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorCursos.aspx",false);
        }
    }
}