<%@ Page Title="" Language="C#" MasterPageFile="~/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteCursos.aspx.cs" Inherits="TPC_equipo_12.EstudianteCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Mis Cursos</h1>
                <asp:Repeater ID="rptCursos" runat="server">
                    <itemtemplate>
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
                    </itemtemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
