<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarResenia.aspx.cs" Inherits="TPC_equipo_12.AgregarResenia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-primary mb-4" OnClick="ButtonVolver_Click" />
        
        <div class="card">
            <div class="card-header text-center">
                <h3>Agregar Reseña</h3>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <asp:Label ID="lblCalificacion" runat="server" Text="" CssClass="form-label"><b>Calificación:</b></asp:Label>
                    <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                
                <div class="mb-3">
                    <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="form-label"><b>Reseña:</b></asp:Label>
                    <asp:TextBox ID="txtResenia" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
                
                <div class="text-center">
                    <asp:Button ID="btnAgregarResenia" runat="server" Text="Agregar Reseña" CssClass="btn btn-primary mt-3" OnClick="btnAgregarResenia_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

