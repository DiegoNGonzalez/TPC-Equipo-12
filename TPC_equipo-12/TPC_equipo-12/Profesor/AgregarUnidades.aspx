<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarUnidades.aspx.cs" Inherits="TPC_equipo_12.AgregarUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
        <div class="card w-50 my-5">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Agregar Unidades</h5>
                <div class="mb-3">
                    <asp:Label ID="LabelQueCurso" runat="server" CssClass="form-label" Text="Elige el curso al cual quieres agregar una unidad."></asp:Label>
                    <asp:DropDownList ID="DropDownListCursos" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelNombreUnidad" runat="server" CssClass="form-label" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TextBoxNombreUnidad" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDescripcionUnidad" runat="server" CssClass="form-label" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="TextBoxDescripcionUnidad" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelNumeroUnidad" runat="server" CssClass="form-label" Text="Numero de unidad"></asp:Label>
                    <asp:TextBox ID="TextBoxNumeroUnidad" runat="server" CssClass="form-control" TextMode="number"></asp:TextBox>
                </div>
                <asp:Button ID="ButtonCrearUnidades" runat="server" Text="Crear Unidad" CssClass="btn btn-primary" OnClick="ButtonCrearUnidades_Click" />
            </div>
        </div>
    </div>
</asp:Content>
