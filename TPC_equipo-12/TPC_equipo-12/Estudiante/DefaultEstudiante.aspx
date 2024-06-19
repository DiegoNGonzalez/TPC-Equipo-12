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
                        <asp:Label runat="server" ID="lblIdCurso" Text='<%# Eval ("IdCurso") %>' CssClass="d-none" />
                    </div>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item text-truncate" onclick="expandirDescripcion(this);">
                            <%# Eval("Descripcion") %>
                        </li>
                        <li class="list-group-item">Duracion: <%# Eval("Duracion") %> hs.</li>
                        <asp:Button Text="Inscribirse" runat="server" CssClass="btn btn-primary" ID="btnInscribirse" OnClick="btnInscribirse_Click" />
                    </ul>
                </div>
            </ItemTemplate>
        </asp:Repeater>
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
