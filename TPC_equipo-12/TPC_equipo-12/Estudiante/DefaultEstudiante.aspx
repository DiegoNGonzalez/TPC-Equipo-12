<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="DefaultEstudiante.aspx.cs" Inherits="TPC_equipo_12.DefaultEstudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
        <ContentTemplate>
            <div class="row justify-content-center">
                <asp:Repeater ID="rptCursos" runat="server">
                    <ItemTemplate>
                        <div class="card ms-5 mb-5" style="width: 18rem; min-height: 24rem;">
                            <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                            <asp:Label ID="lblIDCurso" runat="server" Text='<%# Eval("IDCurso") %>' Visible="false"></asp:Label>
                            <asp:LinkButton ID="LinkButtonEstudiante" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonEstudiante_Command" Style="text-decoration: none;">
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
                            <div class="d-flex justify-content-center align-items-center mt-2">
                                <asp:Button Text="Inscribirse" runat="server" CssClass="btn btn-success btn-sm mx-2 mb-2" ID="btnInscribirse" OnClick="btnInscribirse_Click" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
