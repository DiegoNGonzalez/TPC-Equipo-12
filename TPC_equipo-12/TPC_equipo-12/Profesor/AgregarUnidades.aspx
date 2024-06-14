<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarUnidades.aspx.cs" Inherits="TPC_equipo_12.AgregarUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
                <div class="card w-50 my-5">
                    <div class="card-body">
                        <asp:Label ID="LabelAgregarUnidad" runat="server" class="card-title d-flex justify-content-center align-items-center" Text="Crear Unidad"></asp:Label>
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
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Button ID="ButtonCrearUnidades" runat="server" Text="Crear Unidad" CssClass="btn btn-primary" OnClick="ButtonCrearUnidades_Click" />
                            <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="ButtonVolver_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
