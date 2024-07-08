using Dominio;
using Negocio;
using System;
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
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();

                Estudiante estudiante = (Estudiante)Session["estudiante"];
                if (estudiante == null)
                {
                    Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                    Response.Redirect("../LogIn.aspx");
                }
                txtEmail.Text = estudiante.Email;
                txtEmail.Enabled = false;
                txtNombre.Text = estudiante.Nombre;
                txtApellido.Text = estudiante.Apellido;
                InputDNI.Text = estudiante.DNI.ToString();
                
                dropGenero.Items.Add(new ListItem("Masculino", "M"));
                dropGenero.Items.Add(new ListItem("Femenino", "F"));
                dropGenero.Items.Add(new ListItem("No binario", "x"));

                string genero = estudiante.Genero;
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
                    imgAvatar.ImageUrl = "~/Images/perfil-0.jpg";
                }
                

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EstudianteNegocio estudianteNegocio = new EstudianteNegocio();
            Estudiante estudiante = (Estudiante)Session["estudiante"];
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
            estudianteNegocio.actualizar(estudiante);

            Image img = (Image)Master.FindControl("imgPerfil");
            if (!string.IsNullOrEmpty(estudiante.ImagenPerfil.URL))
                img.ImageUrl = "~/Images/" + estudiante.ImagenPerfil.URL;
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