﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorMensajes : System.Web.UI.Page
    {
        public List<MensajeUsuario> mensajes = new List<MensajeUsuario>();
        public MensajeUsuarioNegocio mensajeUsuarioNegocio = new MensajeUsuarioNegocio();
        public List<MensajeUsuario> mensajesEnviados = new List<MensajeUsuario>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["MensajeExito"] != null)
                {
                    string msj = Session["MensajeExito"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $@"showMessage('{msj}', 'success');", true);
                    Session["MensajeExito"] = null;
                }
                if (Session["MensajeError"] != null)
                {
                    string msj = Session["MensajeError"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $@"showMessage('{msj}', 'error');", true);
                    Session["MensajeError"] = null;
                }
                if (Session["MensajeInfo"] != null)
                {
                    string msj = Session["MensajeInfo"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $@"showMessage('{msj}', 'info');", true);
                    Session["MensajeInfo"] = null;
                }
                Profesor profesor = (Profesor)Session["profesor"];
                mensajes = mensajeUsuarioNegocio.listarMensajes("recibidos",profesor.IDUsuario);
                if (mensajes.Count == 0)
                {
                    PanelMensajes.Visible = false;
                    LabelNoHayMensajes.Visible = true;
                }
                else
                {
                    Session.Add("mensajes", mensajes);
                    rptMensajes.DataSource = mensajes;
                    rptMensajes.DataBind();
                }
                mensajesEnviados = mensajeUsuarioNegocio.listarMensajes("enviados", profesor.IDUsuario);
                if (mensajesEnviados.Count == 0)
                {
                    PanelMensajesEnviados.Visible = false;
                    
                }
                else
                {
                    Session.Add("mensajesEnviados", mensajesEnviados);
                    rptMensajesEnviados.DataSource = mensajesEnviados;
                    rptMensajesEnviados.DataBind();
                }
            }
        }



        protected void btnVerMensaje_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);

            Response.Redirect("VerMensaje.aspx");
        }

        protected void btnVerMensaje_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);
        }

        protected void btnNuevoMensaje_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoMensaje.aspx");
        }

        protected void btnVerMensajeEnviado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);
            Response.Redirect("VerMensaje.aspx");
        }

        protected void btnVerMensajeEnviado_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);
        }

        protected void btnBorrarMensajeEnviado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            mensajeUsuarioNegocio.BorrarMensaje(idMensaje);
            Session["MensajeExito"] = "Mensaje eliminado con éxito.";
            Response.Redirect("ProfesorMensajes.aspx");

        }
    }
}