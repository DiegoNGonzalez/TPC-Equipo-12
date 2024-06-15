<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TPC_equipo_12.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="height: 110vh;">
        <div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Registrarse</h5>

                <div class="mb-3">

                    <asp:Label Text="Nombre/s" runat="server" ID="lblNombres" />
                    <asp:TextBox type="text" ID="InputNombres" CssClass="form-control" runat="server" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Apellido/s" runat="server" />
                    <asp:TextBox type="text" ID="InputApellidos" class="form-control" runat="server" />

                </div>
                <div class="mb-3">
                    <asp:Label Text="DNI" runat="server" />
                    <asp:TextBox type="number" ID="InputDNI" class="form-control" runat="server" />

                </div>
                <div class="mb-3">
                    <asp:Label Text="Genero" runat="server" />
                    <asp:DropDownList ID="dropGenero" runat="server" CssClass="btn btn-secondary dropdown-toggle" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Email" runat="server" />
                    <asp:TextBox type="email" ID="InputEmail" class="form-control" runat="server" />
                </div>
                <div class="mb-3">
                    <asp:Label Text="Contraseña" runat="server" />
                    <asp:TextBox type="password" ID="InputPassword" class="form-control" runat="server" />
                </div>
                <asp:Button ID="btnSignUp" runat="server" class="btn btn-primary" Text="Registrarse" OnClick="btnSignUp_Click" />

            </div>
        </div>
    </div>
</asp:Content>
