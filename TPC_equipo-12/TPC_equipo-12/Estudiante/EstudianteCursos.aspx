<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteCursos.aspx.cs" Inherits="TPC_equipo_12.EstudianteCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <h1>Mis Cursos</h1>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="rptCursos" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonCurso" runat="server" CommandArgument='<%# Eval("IDCurso") %>'
                        OnCommand="LinkButtonCurso_Command" Style="text-decoration: none;">
                        <div class="card ms-5 mb-5" style="width: 18rem;">
                            <img src='<%# Eval("Imagen.URL") %>' class="card-img-top mt-3" alt="...">
                            <div class="card-body text-center">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            </div>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item"><%# Eval("Descripcion") %></li>
                                <li class="list-group-item">Duracion: <%# Eval("Duracion") %> hs.</li>
                                <li class="list-group-item">A third item</li>
                            </ul>
                        </div>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
