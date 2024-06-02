<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TPC_equipo_12.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <form>
            <div class="mb-3">
                <label for="InputNombres" class="form-label">Nombres</label>
                <input type="text" class="form-control" id="InputNombres">
            </div>
            <div class="mb-3">
                <label for="InputApellidos" class="form-label">Apellidos</label>
                <input type="text" class="form-control" id="InputApellidos">
            </div>
            <div class="mb-3">
                <label for="InputDNI" class="form-label">DNI</label>
                <input type="number" class="form-control" id="InputDNI">
            </div>
            <div class="mb-3">
                <label for="InputGenero" class="form-label">Genero</label>
                <input type="text" class="form-control" id="InputGenero">
            </div>
            <div class="mb-3">
                <label for="InputCorreo" class="form-label">Correo</label>
                <input type="email" class="form-control" id="InputCorreo">
            </div>
            <div class="mb-3">
                <label for="InputContraseña" class="form-label">Contraseña</label>
                <input type="password" class="form-control" id="InputContraseña">
            </div>
            <button type="submit" class="btn btn-primary">Submit</button>
        </form>
    </div>
</asp:Content>
