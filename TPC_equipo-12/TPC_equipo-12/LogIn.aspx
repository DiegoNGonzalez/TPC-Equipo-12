<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TPC_equipo_12.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="height: 100vh;">
        <div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Iniciar Sesion</h5>
                <form>
                    <div class="mb-3">
                        <label for="InputCorreoLogIn" class="form-label">Email</label>
                        <input type="email" class="form-control" id="InputCorreoLogIn" aria-describedby="emailHelp">
                    </div>
                    <div class="mb-3">
                        <label for="InputContraseñaLogIn" class="form-label">Contraseña</label>
                        <input type="password" class="form-control" id="InputContraseñaLogIn">
                        <div id="emailHelp" class="form-text">No la compartas con nadie.</div>
                    </div>
                    <button type="submit" class="btn btn-primary">Acceder</button>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
