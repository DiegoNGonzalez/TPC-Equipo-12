<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="CrearCurso.aspx.cs" Inherits="TPC_equipo_12.CrearCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container d-flex justify-content-center align-items-center flex-column" style="min-height: 100vh;">
        <div class="card w-50 my-5">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Crear Curso</h5>
                <div class="mb-3">
                    <asp:Label ID="LabelNombreCurso" runat="server" CssClass="form-label" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TextBoxNombreCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDescripcionCurso" runat="server" CssClass="form-label" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="TextBoxDescripcionCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelEstrenoCurso" runat="server" CssClass="form-label" Text="Fecha de estreno"></asp:Label>
                    <asp:TextBox ID="TextBoxEstrenoCurso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDuracionCurso" runat="server" CssClass="form-label" Text="Duración del curso (en hs)"></asp:Label>
                    <asp:TextBox ID="TextBoxDuracionCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:Label ID="LabelImagenCurso" runat="server" CssClass="form-label" Text="URL Imagen"></asp:Label>
                            <asp:TextBox runat="server" ID="TextBoxUrlImagen" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBoxUrlImagen_TextChanged" />
                        </div>
                        <div class="mb-3 d-flex justify-content-center align-items-center">
                            <asp:Image ImageUrl="https://vilmanunez.com/wp-content/uploads/2016/04/VN-Como-crear-el-mejor-temario-de-tu-curso-online-Incluye-plantillas.png" ID="urlImagenCurso" Width="60%" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="d-flex flex-column align-items-center">
                    <asp:Button ID="ButtonCrearCurso" runat="server" Text="CrearCurso" CssClass="btn btn-primary" OnClick="ButtonCrearCurso_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
