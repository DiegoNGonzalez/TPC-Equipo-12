<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteCursos.aspx.cs" Inherits="TPC_equipo_12.EstudianteCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Custom/styles.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <h1 class="text-center">Mis Cursos</h1>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <asp:Repeater ID="rptCursos" runat="server">
                        <ItemTemplate>
                            <div class="card ms-5 mb-5 <%# Eval("Completo") != null && !(bool)Eval("Completo") ? "curso-incompleto" : "" %>" style="width: 18rem; min-height: 24rem;">
                                <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                                <asp:LinkButton ID="LinkButtonCurso" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonCurso_Command" Style="text-decoration: none;">
                                    <div class="img-container" style="height: 200px; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                                        <img src='<%# Eval("Imagen.URL") %>' class="card-img-top img-fluid" alt="..." style="max-height: 100%; width: auto;">
                                    </div>
                                    <div class="card-body text-center">
                                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                    </div>
                                </asp:LinkButton>
                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item text-truncate" onclick="expandirDescripcion(this);">
                                        <%# Eval("Descripcion") %>
                                    </li>
                                    <li class="list-group-item">Duración: <%# Eval("Duracion") %> hs.</li>
                                    <li class="list-group-item">Categoria:
                                        <asp:Label ID="LabelCategoriaCurso" runat="server"></asp:Label>
                                    </li>
                                </ul>
                                <div class="overlay-text <%# Eval("Completo") != null && !(bool)Eval("Completo") ? "" : "d-none" %>">
                                    Por el momento este curso no esta completo.<br /> Consultar al profesor por mensaje.
                                </div>
                                <div class="d-flex justify-content-center align-items-center mt-2">
                                    <asp:Button ID="ButtonDesinscribirse" runat="server" Text="Desinscribirse" CssClass="btn btn-danger btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonDesinscribirse_Command" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
