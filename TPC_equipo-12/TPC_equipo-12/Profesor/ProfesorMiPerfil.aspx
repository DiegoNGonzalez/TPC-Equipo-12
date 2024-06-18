<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorMiPerfil.aspx.cs" Inherits="TPC_equipo_12.ProfesorMiPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center">Mi Perfil</h2>
    
    <div class="container vh-100 d-flex flex-column justify-content-center">
        <!-- Fila para los campos de texto y la imagen de perfil -->
        <div class="row justify-content-center">
            <!-- Columna para los campos de texto -->
            <div class="col-md-4">
                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox  runat="server" CssClass="form-control" ID="txtEmail" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Nombre/s</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Apellido</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
                </div>
            </div>

            <!-- Columna para la imagen de perfil -->
            <div class="col-md-4">
                <div class="mb-3">
                    <label class="form-label">Imagen Perfil</label>
                    <input type="file" id="txtImagen" runat="server" class="form-control" />
                </div>
                <asp:Image ID="imgAvatar" runat="server" CssClass="img-fluid mb-3" />
            </div>
        </div>
        
        <!-- Fila para el botón de guardar y el enlace para regresar -->
        <div class="row justify-content-center mt-3">
            <div class="col-md-4 text-center">
                <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" />
                <a href="DefaultEstudiante.aspx" class="btn btn-secondary ms-2">Regresar</a>
            </div>
        </div>
    </div>
</asp:Content>

