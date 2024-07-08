<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorFabricaDeCursos.aspx.cs" Inherits="TPC_equipo_12.ProfesorFabricaDeCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center mb-5">
                <h3 class="text-center mt-3">¡Fabrica de Cursos!</h3>
                <hr />
                <asp:Label ID="LabelNoHayCursos" runat="server" CssClass="display-7 mt-5"><b>No hay Cursos incompletos!</b></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <asp:Repeater ID="rptProfesorCursos" runat="server">
                        <ItemTemplate>
                            <div class="card ms-5 mb-5" style="width: 24rem; min-height: 28rem;">
                                <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                                <asp:LinkButton ID="LinkButtonCursoProf" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonCursoProf_Command" Style="text-decoration: none;">
                                <div class="img-container mt-2" style="height: 200px; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                                    <asp:Image ID="ImagenCurso" runat="server" CssClass="card-img-top img-fluid" ImageUrl='<%# "~/Images/" + Eval("Imagen.URL") %>' />
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
                                    <li class="list-group-item">Categoria:  <%# Eval("Categoria.Nombre") %>
                                
                                    </li>
                                </ul>
                                <div class="d-flex justify-content-center align-items-center mt-2">
                                    <asp:Button ID="ButtonAltaCurso" runat="server" Text="Alta Curso" CssClass="btn btn-success btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonAltaCurso_Command" />
                                    <asp:Button ID="ButtonModificarCurso" runat="server" Text="Modificar Curso" CssClass="btn btn-secondary btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonModificarCurso_Command" />
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
