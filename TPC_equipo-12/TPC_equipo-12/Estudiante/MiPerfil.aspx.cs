using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();

                Estudiante estudiante = (Estudiante)Session["estudiante"];
                txtEmail.Text = estudiante.Email;
                txtEmail.Enabled = false;
                txtNombre.Text = estudiante.Nombre;
                txtApellido.Text = estudiante.Apellido;
                InputDNI.Text = estudiante.DNI.ToString();
                
                dropGenero.Items.Add(new ListItem("Masculino", "M"));
                dropGenero.Items.Add(new ListItem("Femenino", "F"));
                dropGenero.Items.Add(new ListItem("No binario", "x"));
                dropGenero.Items.Add(new ListItem("No contesta", null));

                string genero = !string.IsNullOrEmpty(estudiante.Genero) ? estudiante.Genero : "No contesta";
                ListItem item = dropGenero.Items.FindByValue(genero);
                if (item != null)
                {
                    item.Selected = true;
                }

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
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            if (txtImagen.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("~/Images/");
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + estudiante.IDUsuario + ".jpg");
                estudiante.ImagenPerfil.URL = "perfil-" + estudiante.IDUsuario + ".jpg";
            }
            estudiante.Nombre = txtNombre.Text;
            estudiante.Apellido = txtApellido.Text;
            estudiante.DNI = Convert.ToInt32(InputDNI.Text);
            if (dropGenero.SelectedValue == "M")
            {
                estudiante.Genero = "M";
            }
            else if (dropGenero.SelectedValue == "F")
            {
                estudiante.Genero = "F";
            }
            else if (dropGenero.SelectedValue == "x")
            {
                estudiante.Genero = "x";
            }
            else
            {
                estudiante.Genero = null;
            }
            //Guardar datos de perfil
            estudianteNegocio.actualizar(estudiante);

            Image img = (Image)Master.FindControl("imgPerfil");
            if (!string.IsNullOrEmpty(estudiante.ImagenPerfil.URL))
                img.ImageUrl = "~/Images/" + estudiante.ImagenPerfil.URL;
            else
                img.ImageUrl = "~/Images/perfil-0.jpg";
            
            


        }
    }
}