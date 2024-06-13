<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="AgregarMateriales.aspx.cs" Inherits="TPC_equipo_12.AgregarMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
                <div class="card w-50 my-5">
                    <div class="card-body">
                        <asp:Label ID="LabelAgregarMaterial" runat="server" CssClass="card-title d-flex justify-content-center align-items-center" Text="Agregar Material"></asp:Label>
                        <div class="mb-3">
                            <asp:Label ID="LabelNombreMaterial" runat="server" CssClass="form-label" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="TextBoxNombreMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="LabelDescripcionMaterial" runat="server" CssClass="form-label" Text="Descripción"></asp:Label>
                            <asp:TextBox ID="TextBoxDescripcionMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="LabelNumeroMaterial" runat="server" CssClass="form-label" Text="Número de Material"></asp:Label>
                            <asp:TextBox ID="TextBoxNumeroMaterial" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="LabelTipoMaterial" runat="server" CssClass="form-label" Text="Tipo de Material"></asp:Label>
                            <asp:DropDownList ID="DropDownListTipoMaterial" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Video" Value="Video"></asp:ListItem>
                                <asp:ListItem Text="Contenido" Value="Contenido"></asp:ListItem>
                            </asp:DropDownList>
                            <div class="mb-3">
                                <asp:Label ID="LabelURLMaterial" runat="server" CssClass="form-label" Text="URL Archivo"></asp:Label>
                                <asp:TextBox ID="TextBoxURLMaterial" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="d-flex justify-content-between align-items-center">
                                <asp:Button ID="ButtonCrearMaterial" runat="server" Text="Crear Material" CssClass="btn btn-primary" OnClick="ButtonCrearMaterial_Click" />
                                <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="ButtonVolver_Click" />
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
