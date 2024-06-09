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
                        <h5 class="card-title d-flex justify-content-center align-items-center">Agregar Materiales</h5>
                        <div class="mb-3">
                            <asp:Label ID="LabelQueCurso" runat="server" CssClass="form-label" Text="Elige el curso"></asp:Label>
                            <asp:DropDownList ID="DropDownListCursos" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCursos_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="LabelQueUnidad" runat="server" CssClass="form-label" Text="Elige una unidad."></asp:Label>
                            <asp:DropDownList ID="DropDownListUnidades" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownListUnidades_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="LabelQueLeccion" runat="server" CssClass="form-label" Text="Elige la leccion a la cual quieres agregar un material de estudio."></asp:Label>
                            <asp:DropDownList ID="DropDownListLecciones" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>

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
                            <asp:Label ID="LabelImagenCurso" runat="server" CssClass="form-label" Text="URL Archivo"></asp:Label>
                            <asp:TextBox ID="TextBoxImagenCurso" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:Button ID="ButtonHechoMateriales" runat="server" Text="Hecho" CssClass="btn btn-primary" OnClick="ButtonHechoMateriales_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
