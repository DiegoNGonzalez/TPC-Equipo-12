﻿using System;

namespace TPC_equipo_12
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            //Session["MensajeError"] = exc.Message;
            //Session.Add("Error", exc.ToString());
            //Server.Transfer("Error.aspx");
        }
    }
}