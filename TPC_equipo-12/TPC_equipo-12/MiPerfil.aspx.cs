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
       
            if (!IsPostBack)
            {
                Estudiante estudiante = (Estudiante)Session["estudiante"];

                
                txtEmail.Text = estudiante.Email;
                txtEmail.ReadOnly = true;
                txtNombre.Text = estudiante.Nombre;
                txtApellido.Text = estudiante.Apellido;
                if (estudiante != null && estudiante.ImagenPerfil.URL != null && !string.IsNullOrEmpty(estudiante.ImagenPerfil.URL))
                {
                    imgAvatar.ImageUrl = "~/Images/" + estudiante.ImagenPerfil.URL;
                }
                else
                {
                    imgAvatar.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s";
                }

                
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstudianteNegocio estudianteNegocio = new EstudianteNegocio();
            Estudiante estudiante = (Estudiante)Session["estudiante"];

            if (txtImagen.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("~/Images/");
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + estudiante.IDUsuario + ".jpg");
                estudiante.ImagenPerfil.URL = "perfil-" + estudiante.IDUsuario + ".jpg";
            }
            estudiante.Nombre = txtNombre.Text;
            estudiante.Apellido = txtApellido.Text;

            //Guardar datos de perfil
            estudianteNegocio.actualizar(estudiante);

            Image img = (Image)Master.FindControl("imgPerfil");
            img.ImageUrl = "~/Images/" + estudiante.ImagenPerfil.URL;
            

        }
    }
}