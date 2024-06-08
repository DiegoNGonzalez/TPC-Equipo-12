<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TPC_equipo_12.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="height: 110vh;">
        <div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Registrarse</h5>
                
                    <div class="mb-3">
                        
                        <asp:Label Text="Nombre/s" runat="server"  ID="lblNombres"/>
                        <asp:TextBox type="text" id="InputNombres" CssClass="form-control" runat="server" />
                    </div>
                    <div class="mb-3">
                        <asp:Label Text="Apellido/s" runat="server" />
                        <asp:TextBox type="text" id="InputApellidos" class="form-control" runat="server" />
                        
                    </div>
                    <div class="mb-3">
                        <asp:Label Text="DNI" runat="server" />
                        <asp:TextBox type="number" id="InputDNI" class="form-control" runat="server" />
                        
                    </div>
                    <div class="mb-3">
                        <asp:Label Text="Genero" runat="server" />
                        <asp:DropDownList ID="dropGenero" runat="server" CssClass="btn btn-secondary dropdown-toggle" />
                    </div>
                    <div class="mb-3">
                        <asp:Label text="Email" runat="server"/>
                       <asp:textbox type="email" id="InputEmail" class="form-control" runat="server"/>
                    </div>
                    <div class="mb-3">
                        <asp:Label text="Contraseña" runat="server"/>
                        <asp:TextBox type="password" id="InputPassword" class="form-control" runat="server"/>
                    </div>
                     <asp:Button id="btnSignUp" runat="server" class="btn btn-primary" Text="Registrarse" OnClick="btnSignUp_Click"/>
                
            </div>
        </div>
    </div>
</asp:Content>
