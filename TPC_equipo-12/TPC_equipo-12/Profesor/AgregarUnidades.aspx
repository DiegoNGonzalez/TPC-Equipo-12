<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarUnidades.aspx.cs" Inherits="TPC_equipo_12.AgregarUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
        <div class="card w-50 my-5">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Agregar Unidades</h5>
                <div class="mb-3">
                    <asp:Label ID="LabelNombreUnidad" runat="server" CssClass="form-label" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TextBoxNombreUnidad" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDescripcionUnidad" runat="server" CssClass="form-label" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="TextBoxDescripcionUnidad" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelNumeroUnidad" runat="server" CssClass="form-label" Text="Fecha de estreno"></asp:Label>
                    <asp:TextBox ID="TextBoxNumeroUnidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Button ID="ButtonAgregarLecciones" runat="server" Text="Agregar Lecciones" CssClass="btn btn-primary" OnClick="ButtonAgregarLecciones_Click" />
                </div>
                <asp:Button ID="ButtonHechoUnidades" runat="server" Text="Hecho" CssClass="btn btn-primary" OnClick="ButtonHechoUnidades_Click" />
            </div>
        </div>
    </div>
</asp:Content>
