<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCambiarContrasenia.aspx.cs" Inherits="TPC_equipo_12.ProfesorCambiarContrasenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">Cambiar Contraseña</h2>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:Label runat="server" Text="Contraseña Actual"></asp:Label>
                    <asp:TextBox runat="server" ID="txtContraseniaActual" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mt-3">
                    <asp:Label runat="server" ID="lblError" Text="La contraseña actual es incorrecta." CssClass="text-danger" Visible="false"></asp:Label>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox runat="server" ID="txtNuevaContrasenia" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" Text="Confirmar Nueva Contraseña"></asp:Label>
                    <asp:TextBox runat="server" ID="txtConfirmarContrasenia" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:CompareValidator runat="server" ControlToValidate="txtConfirmarContrasenia" ControlToCompare="txtNuevaContrasenia" ErrorMessage="Las contraseñas no coinciden." CssClass="text-danger"></asp:CompareValidator>
                </div>
                <asp:Button runat="server" ID="btnActualizarContrasena" Text="Actualizar Contraseña" OnClick="btnActualizarContrasena_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
