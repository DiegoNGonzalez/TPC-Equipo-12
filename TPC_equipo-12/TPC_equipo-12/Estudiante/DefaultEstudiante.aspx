<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="DefaultEstudiante.aspx.cs" Inherits="TPC_equipo_12.DefaultEstudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h1 class="text-center">Bienvenido Estudiante</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-center">Cursos Disponibles</h2>
        </div>
    </div>
    <div class="row justify-content-center">
        <asp:Repeater ID="rptCursos" runat="server">
            <ItemTemplate>
                <div class="card ms-5 mb-5 " style="width: 18rem;">
                    <img src='<%# Eval("Imagen.URL") %>' class="card-img-top mt-3" alt="...">
                    <div class="card-body text-center">
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    </div>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item"><%# Eval("Descripcion") %></li>
                        <li class="list-group-item">Duracion: <%# Eval("Duracion") %> hs.</li>
                        <asp:Button Text="Detalles" runat="server" CssClass="btn btn-primary" ID="btnInscribirse" OnClick="btnDetalles_Click" />
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
