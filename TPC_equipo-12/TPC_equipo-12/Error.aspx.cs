using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = Session["error"].ToString();

            }
            catch (Exception)
            {
                Response.Redirect("Default.aspx");
            }
            finally
            {
                Session["error"] = null;
            }

        }
    }
}