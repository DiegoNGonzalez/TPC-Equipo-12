<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_equipo_12.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>¡Bienvenido a la plataforma de cursos!</h1>
                <h1>Cursos Disponibles</h1>
            </div>
        </div>
        <asp:Repeater ID="rptCursos" runat="server">
            <ItemTemplate>
                <div class="card" style="width: 18rem;">
                    <img src='<%# Eval("Imagen.URL") %>' class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                        <p class="card-text"><%# Eval("Descripcion") %></p>
                    </div>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item"><%# Eval("Duracion") %></li>
                        <li class="list-group-item">A second item</li>
                        <li class="list-group-item">A third item</li>
                    </ul>
                    <div class="card-body">
                        <a href="#" class="card-link">Card link</a>
                        <a href="#" class="card-link">Another link</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>
