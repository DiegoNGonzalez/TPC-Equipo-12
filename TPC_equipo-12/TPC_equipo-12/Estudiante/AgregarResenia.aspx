<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarResenia.aspx.cs" Inherits="TPC_equipo_12.AgregarResenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Agregar Reseña</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblCalificacion" runat="server" Text="Calificación"></asp:Label>
            </div>
        <div class="row">
            <div class="col-md-12">
                <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblMensaje" runat="server" Text="Reseña"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:TextBox ID="txtResenia" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnAgregarResenia" runat="server" Text="Agregar Reseña" CssClass="btn btn-primary" OnClick="btnAgregarResenia_Click" />
            </div>
        </div>
    </div>


</asp:Content>
