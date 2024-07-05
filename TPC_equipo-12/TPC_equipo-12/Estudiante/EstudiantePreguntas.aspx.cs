using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudiantePreguntas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["IDComentarioPadre"] != null)
                {
                    int idComentarioPadre = Convert.ToInt32(Session["IDComentarioPadre"]);
                    cargarComentarioPrincipalYrespuestas(idComentarioPadre);
                }
            }

        }

        public void cargarComentarioPrincipalYrespuestas(int idComentarioPadre)
        {
            Datos datos = new Datos();
            DataTable dtPadre = new DataTable();
            DataTable dtRespuestas = new DataTable();

            try
            {
                // Consulta para cargar el comentario padre con la imagen de perfil
                datos.SetearConsulta("SELECT C.IDComentario, C.IDUsuarioEmisor, U.Nombre, U.IDImagen, C.CuerpoComentario, C.FechaCreacion FROM Comentarios C INNER JOIN Usuarios U ON C.IDUsuarioEmisor = U.IDUsuario WHERE C.IDComentario = @IdComentarioPadre");
                datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
                datos.EjecutarLectura();
                dtPadre.Load(datos.Lector);
                datos.CerrarConexion();

                // Limpiar parámetros antes de la siguiente consulta
                datos.LimpiarParametros();

                // Consulta para cargar las respuestas con las imágenes de perfil
                datos.SetearConsulta("SELECT C.IDComentario, C.IDUsuarioEmisor, U.Nombre, U.IDImagen, C.CuerpoComentario, C.FechaCreacion FROM Comentarios C INNER JOIN Usuarios U ON C.IDUsuarioEmisor = U.IDUsuario WHERE C.IDComentarioPadre = @IdComentarioPadre");
                datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
                datos.EjecutarLectura();
                dtRespuestas.Load(datos.Lector);
                datos.CerrarConexion();

                // Asignar datos al Repeater
                rptComentarioPadre.DataSource = dtPadre;
                rptComentarioPadre.DataBind();

                rptRespuestas.DataSource = dtRespuestas;
                rptRespuestas.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        protected void btnResponder_Click(object sender, EventArgs e)
        {
            if (Session["IDComentarioPadre"] != null)
            {
                int idComentarioPadre = Convert.ToInt32(Session["IDComentarioPadre"]);
                Usuario usuarioActual = Session["estudiante"] != null ? (Usuario)Session["estudiante"] : (Usuario)Session["profesor"]; // Suponiendo que tienes una función para obtener el usuario actual
                string cuerpoRespuesta = txtRespuesta.Text;

                if (!string.IsNullOrEmpty(cuerpoRespuesta))
                {
                    Datos datos = new Datos();
                    try
                    {
                        datos.SetearConsulta("INSERT INTO Comentarios (IDComentarioPadre, IDLeccion, IDUsuarioEmisor, CuerpoComentario, FechaCreacion, IDImagen, Estado) VALUES (@IDComentarioPadre, @IDLeccion, @IDUsuarioEmisor, @CuerpoComentario, @FechaCreacion, @IdImagen, @Estado)");
                        datos.SetearParametro("@IDComentarioPadre", idComentarioPadre);
                        datos.SetearParametro("@IDLeccion", Session["IDLeccion"]); // Suponiendo que tienes una función para obtener el ID de la lección actual
                        datos.SetearParametro("@IDUsuarioEmisor", usuarioActual.IDUsuario);
                        datos.SetearParametro("@CuerpoComentario", cuerpoRespuesta);
                        datos.SetearParametro("@FechaCreacion", DateTime.Now);
                        datos.SetearParametro("@idImagen", usuarioActual.ImagenPerfil == null ? (object)DBNull.Value : usuarioActual.ImagenPerfil.IDImagen);
                        datos.SetearParametro("@Estado", true);
                        datos.EjecutarAccion();
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción
                        throw new Exception("Error al guardar la respuesta", ex);
                    }
                    finally
                    {
                        datos.CerrarConexion();
                    }

                    // Recargar los comentarios después de insertar la respuesta
                    txtRespuesta.Text = "";
                    cargarComentarioPrincipalYrespuestas(idComentarioPadre);
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstudianteMateriales.aspx", false);
        }


    }
}