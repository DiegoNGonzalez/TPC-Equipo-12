<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasenia.aspx.cs" Inherits="TPC_equipo_12.RecuperarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container vh-100 d-flex justify-content-center align-items-center">
        <div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title text-center">Cambiar Contraseña</h5>
                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
