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
        <div class="row justify-content-center">
            <asp:Repeater ID="rptCursos" runat="server">
                <ItemTemplate>
                    <div class="card ms-5 mb-5" style="width: 18rem;">
                        <asp:LinkButton ID="LinkButtonCurso" runat="server" CommandArgument='<%# Eval("IDCurso") %>'
                            OnCommand="LinkButtonCurso_Command" Style="text-decoration: none;">
                            <img src='<%# Eval("Imagen.URL") %>' class="card-img-top mt-3" alt="...">
                            <div class="card-body text-center">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            </div>
                        </asp:LinkButton>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item text-truncate" onclick="expandirDescripcion(this);">
                                <%# Eval("Descripcion") %>
                            </li>
                            <li class="list-group-item">Duracion: <%# Eval("Duracion") %> hs.</li>
                        </ul>
                        <div class="d-flex justify-content-center align-items-center mt-2">
                            <asp:Button ID="ButtonDesinscribirse" runat="server" Text="Desinscribirse" CssClass="btn btn-primary btn-sm mx-2 mb-4" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonDesinscribirse_Command" />
                        </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script>
        function expandirDescripcion(element) {
            const EstaTruncado = element.classList.contains("text-truncate");
            if (EstaTruncado) {
                element.classList.remove("text-truncate");
            } else {
                element.classList.add("text-truncate");
            }
        }
    </script>
</asp:Content>
