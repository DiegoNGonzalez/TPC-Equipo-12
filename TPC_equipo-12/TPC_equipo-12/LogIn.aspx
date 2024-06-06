<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TPC_equipo_12.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="height: 100vh;">
        <div class="card" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Iniciar Sesion</h5>
                <asp:Label ID="LabelErrorLogIn" Style="color: red" runat="server" Text=""></asp:Label>
                <div class="mb-3">
                    <asp:Label ID="Label1" runat="server" Text="Correo"></asp:Label>
                    <asp:TextBox ID="InputEmailLogIn" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
                    <asp:TextBox type="password" ID="InputContraseñaLogIn" runat="server" CssClass="form-control password"></asp:TextBox>
                    <div id="emailHelp" class="form-text">No la compartas con nadie.</div>
                </div>
                <div class="d-flex justify-content-center align-items-center">

                    <asp:Button ID="ButtonLogIn" runat="server" Text="Iniciar Sesion" CssClass="btn btn-primary " OnClick="ButtonLogIn_Click" />
                </div>
                <div class="d-flex justify-content-center align-items-center">
                    <asp:Label ID="LabelErrorRegistro" runat="server" Text=""></asp:Label>
                </div>
                <div class="d-flex justify-content-center align-items-center">
                    <asp:Button ID="ButtonErrorRegistro" runat="server" Text="Registrarme!" CssClass="btn btn-primary" OnClick="ButtonErrorRegistro_Click" />
                </div>

            </div>
        </div>


        <div class="card ms-5" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Profesor (Desarrollo)</h5>
                <p class="text-center">Inicias sesion como: hola@maxiprograma.com </p>
                <div class="d-flex justify-content-center align-items-center">
                <asp:Button ID="ButtonLogInFastProf" runat="server" Text="Iniciar Sesion" CssClass="btn btn-primary" OnClick="ButtonLogInFastProf_Click" />
                    </div>
            </div>
        </div>


        <div class="card ms-5" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Estudiante (Desarrollo)</h5>
                <p class="text-center">Inicias sesion como: tomas.caceres2@alumnos.frgp.utn.edu.ar </p>
                <div class="d-flex justify-content-center align-items-center">
                <asp:Button ID="ButtonLogInFastEst" runat="server" Text="Iniciar Sesion" CssClass="btn btn-primary" OnClick="ButtonLogInFastEst_Click" />
                    </div>
            </div>
        </div>

    </div>
</asp:Content>
