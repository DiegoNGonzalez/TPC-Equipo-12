<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCursos.aspx.cs" Inherits="TPC_equipo_12.ProfesorCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <h1 class="text-center">Cursos Cargados</h1>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <asp:Repeater ID="rptProfesorCursos" runat="server">
                        <ItemTemplate>
                            <div class="card ms-5 mb-5" style="width: 18rem; min-height: 24rem;">
                                <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                                <asp:LinkButton ID="LinkButtonCursoProf" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonCursoProf_Command" Style="text-decoration: none;">
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
                                <div class="d-flex justify-content-between align-items-center mt-2">
                                    <asp:Button ID="ButtonEliminarCurso" runat="server" Text="Eliminar Curso" CssClass="btn btn-danger btn-sm mx-2 mb-4" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonEliminarCurso_Command" />
                                    <asp:Button ID="ButtonModificarCurso" runat="server" Text="Modificar Curso" CssClass="btn btn-secondary btn-sm mx-2 mb-4" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonModificarCurso_Command" />
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
