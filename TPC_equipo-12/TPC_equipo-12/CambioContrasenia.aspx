<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CambioContrasenia.aspx.cs" Inherits="TPC_equipo_12.CambioContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">Cambio de Contraseña</h2>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="mb-3">
                    <asp:Label runat="server" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox runat="server" ID="txtNuevaContraseña" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" Text="Confirmar Nueva Contraseña"></asp:Label>
                    <asp:TextBox runat="server" ID="txtConfirmarContraseña" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <asp:CompareValidator runat="server" ControlToValidate="txtConfirmarContraseña" ControlToCompare="txtNuevaContraseña" ErrorMessage="Las contraseñas no coinciden." CssClass="text-danger"></asp:CompareValidator>
                </div>
                <asp:Button runat="server" ID="btnActualizarContraseña" Text="Actualizar Contraseña" OnClick="btnActualizarContraseña_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
