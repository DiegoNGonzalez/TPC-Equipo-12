<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPC_equipo_12.MiPerfil" %>

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
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Nombre/s</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
<%--                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El Nombre es obligatorio." CssClass="text-danger" />--%>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El Nombre solo debe contener letras." CssClass="text-danger" ValidationExpression="^[a-zA-Z\s]+$" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Apellido</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El Apellido solo debe contener letras." CssClass="text-danger" ValidationExpression="^[a-zA-Z\s]+$" />
<%--                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El Apellido es obligatorio." CssClass="text-danger" />--%>
                </div>
                <div class="mb-3">
                    <asp:Label Text="DNI" runat="server" />
                    <asp:TextBox type="number" ID="InputDNI" class="form-control" runat="server" />
<%--                    <asp:RequiredFieldValidator runat="server" ControlToValidate="InputDNI" ErrorMessage="El DNI es obligatorio." CssClass="text-danger" />--%>
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="InputDNI" ErrorMessage="El DNI debe ser un número entre 7 y 9 dígitos." CssClass="text-danger" ValidationExpression="^\d{7,9}$" />

                </div>
                <div class="mb-3">
                    <asp:Label Text="Genero" runat="server" />
                    <asp:DropDownList ID="dropGenero" runat="server" CssClass="btn btn-secondary dropdown-toggle" />
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





