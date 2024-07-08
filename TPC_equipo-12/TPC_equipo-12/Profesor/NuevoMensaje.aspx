<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="NuevoMensaje.aspx.cs" Inherits="TPC_equipo_12.NuevoMensaje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="container">
    <div class="row">
        <div class="col-md-12">
            <h3 class="text-center mt-3">Nuevo Mensaje</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblDestinatario" runat="server" Text=""><b>Destinatario:</b></asp:Label>
            <asp:DropDownList ID="ddlDestinatario" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblAsunto" runat="server" Text=""><b>Asunto:</b></asp:Label>
            <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" Text=""><b>Mensaje:</b></asp:Label>
            <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary mt-3" OnClick="btnEnviar_Click" />
        </div>
    </div>
</div>
</asp:Content>
