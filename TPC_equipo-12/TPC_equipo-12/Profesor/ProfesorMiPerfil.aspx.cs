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
    public partial class ProfesorMiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Profesor profesor = (Profesor)Session["profesor"];


                txtEmail.Text = profesor.Email;
                txtEmail.ReadOnly = true;
                txtNombre.Text = profesor.Nombre;
                txtApellido.Text = profesor.Apellido;
                if (profesor != null && profesor.ImagenPerfil.URL != null && !string.IsNullOrEmpty(profesor.ImagenPerfil.URL))
                {
                    imgAvatar.ImageUrl = "~/Images/" + profesor.ImagenPerfil.URL;
                }
                else
                {
                    imgAvatar.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s";
                }


            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProfesorNegocio profesorNegocio = new ProfesorNegocio();
            Profesor profesor = (Profesor)Session["profesor"];

            if (txtImagen.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("~/Images/");
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + profesor.IDUsuario + ".jpg");
                profesor.ImagenPerfil.URL = "perfil-" + profesor.IDUsuario + ".jpg";
            }
            profesor.Nombre = txtNombre.Text;
            profesor.Apellido = txtApellido.Text;

            //Guardar datos de perfil
            profesorNegocio.actualizar(profesor);

            Image img = (Image)Master.FindControl("imgPerfil");
            img.ImageUrl = "~/Images/" + profesor.ImagenPerfil.URL;
        }
    }
}