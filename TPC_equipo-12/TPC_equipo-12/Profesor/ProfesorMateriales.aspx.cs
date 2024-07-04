using AccesoDB;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorMateriales : System.Web.UI.Page
    {
        public List<MaterialLeccion> listaMateriales = new List<MaterialLeccion>();
        public MaterialNegocio materialNegocio = new MaterialNegocio();
        public List<Comentario> listaComentarios = new List<Comentario>();
        public ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                ProfesorMasterPage master = (ProfesorMasterPage)Page.Master;
                master.VerificarMensaje();
                int idleccion = Convert.ToInt32(Request.QueryString["idLeccion"]);
                if (idleccion != 0)
                {
                    Session.Add("IDLeccionProfesor", idleccion);
                }
                else
                {
                    idleccion = (int)Session["IDLeccionProfesor"];
                }
                if (idleccion != 0)
                {
                    listaMateriales = materialNegocio.ListarMateriales(idleccion);
                    listaComentarios = comentarioNegocio.cargarComentarios(idleccion);
                }
                Session.Add("ListaMaterialesProfesor", listaMateriales);
                rptMaterialesProf.DataSource = listaMateriales;
                rptMaterialesProf.DataBind();

                
                rptComentarios.DataSource = listaComentarios;
                rptComentarios.DataBind();

                if (Session["Home"] != null && (bool)Session["Home"])
                {
                    ButtonCrearMaterialProf.Visible = false;
                    ButtonEliminarMaterialProf.Visible = false;
                }
            }
        }

        protected void ButtonBackLeccionesProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorLecciones.aspx");
        }

        private string ExtractVideoId(string youtubeLink)
        {

            var regex = new Regex(@"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/\s]{11})");

            var match = regex.Match(youtubeLink);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {

                return null;
            }
        }
        private string CargarIframe(MaterialLeccion material)
        {
            string youtubeLink = material.URL;

            string videoId = ExtractVideoId(youtubeLink);

            string iframeHtml = $@"
        <iframe 
            class='w-100'
            height='600'
            src='https://www.youtube.com/embed/{videoId}' 
            title='YouTube video player' 
            frameborder='0' 
            allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' 
            allowfullscreen>
        </iframe>";
            return iframeHtml;
        }
        protected void rptMaterialesProf_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MaterialLeccion material = e.Item.DataItem as MaterialLeccion;
                if (material.TipoMaterial == "Video")
                {
                    Literal ltlYoutubeVideo = (Literal)e.Item.FindControl("ltlYoutubeVideo");
                    ltlYoutubeVideo.Text = CargarIframe(material);
                }
                else if (material.TipoMaterial == "Documento")
                {
                    Literal ltlDocumento = (Literal)e.Item.FindControl("ltlDocumento");
                    ltlDocumento.Text = $@"
                    <a href='{material.URL}' target='_blank' class='border p-2 m-2 d-inline-block mb-4'>
                        {material.Nombre}
                    </a>";
                }
            }

            Button btnModificar = (Button)e.Item.FindControl("ButtonModificarMaterialesProf");

            if (Session["Home"] != null && (bool)Session["Home"])
            {
                btnModificar.Visible = false;
            }
        }

        protected void ButtonCrearMaterialProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMateriales.aspx");
        }

        protected void ButtonEliminarMaterialProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("EliminarMateriales.aspx");
        }

        protected void ButtonModificarMaterialesProf_Command(object sender, CommandEventArgs e)
        {
            int IdMaterial = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDMaterialProfesor", IdMaterial);
            Response.Redirect("AgregarMateriales.aspx?idMaterial=" + IdMaterial);
        }

        
        protected void btnRespuesta_Click(object sender, EventArgs e)
        {
            string idComentarioPadre = ((Button)sender).CommandArgument;
            Session.Add("IDComentarioPadre", idComentarioPadre);
            Response.Redirect("ProfesorPreguntas.aspx");
        }

        public string GetEstadoText(object estado)
        {
            bool estadoBool = (bool)estado;
            return estadoBool ? "Habilitado" : "Deshabilitado";
        }
    }
}