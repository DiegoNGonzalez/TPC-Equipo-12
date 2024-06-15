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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((Estudiante)Session["estudiante"]).ImagenPerfil.URL != null)
                //imgPerfil.ImageUrl = "~/Images/" + ((Estudiante)Session["estudiante"]).ImagenPerfil.URL;
                imgAvatar.ImageUrl = "~/Images/" + ((Estudiante)Session["estudiante"]).ImagenPerfil.URL;
            else
                imgAvatar.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Server.MapPath("./Images/");
                Usuario usuario = (Estudiante)Session["estudiante"];
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.IDUsuario + ".jpg");

                usuario.ImagenPerfil.URL = "perfil-" + usuario.IDUsuario + ".jpg";
                usuarioNegocio.actualizar(usuario);
                //leer img
                Image img = (Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Images/" + usuario.ImagenPerfil.URL;

                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}