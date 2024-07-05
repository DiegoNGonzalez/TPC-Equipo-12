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
                txtEmail.Enabled = false;
                txtNombre.Text = profesor.Nombre;
                txtApellido.Text = profesor.Apellido;
                InputDNI.Text = profesor.DNI.ToString();

                dropGenero.Items.Add(new ListItem("Masculino", "M"));
                dropGenero.Items.Add(new ListItem("Femenino", "F"));
                dropGenero.Items.Add(new ListItem("No binario", "x"));
                dropGenero.Items.Add(new ListItem("No contesta", null));

                string genero = !string.IsNullOrEmpty(profesor.Genero) ? profesor.Genero : "No contesta";
                ListItem item = dropGenero.Items.FindByValue(genero);
                if (item != null)
                {
                    item.Selected = true;
                }

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
            if (!ValidarFormulario())
            {
                return;
            }
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            if (txtImagen.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("~/Images/");
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + profesor.IDUsuario + ".jpg");
                profesor.ImagenPerfil.URL = "perfil-" + profesor.IDUsuario + ".jpg";
            }
            profesor.Nombre = txtNombre.Text;
            profesor.Apellido = txtApellido.Text;
            profesor.DNI = Convert.ToInt32(InputDNI.Text);
            if (dropGenero.SelectedValue == "M")
            {
                profesor.Genero = "M";
            }
            else if (dropGenero.SelectedValue == "F")
            {
                profesor.Genero = "F";
            }
            else if (dropGenero.SelectedValue == "x")
            {
               profesor.Genero = "x";
            }
            else
            {
                profesor.Genero = null;
            }

            //Guardar datos de perfil
            profesorNegocio.actualizar(profesor);

            Image img = (Image)Master.FindControl("imgPerfil");
            if (!string.IsNullOrEmpty(profesor.ImagenPerfil.URL))
            
                img.ImageUrl = "~/Images/" + profesor.ImagenPerfil.URL;
            else
                img.ImageUrl = "~/Images/perfil-0.jpg";
            
        }
        protected bool ValidarFormulario()
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" || InputDNI.Text == "" || txtEmail.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}