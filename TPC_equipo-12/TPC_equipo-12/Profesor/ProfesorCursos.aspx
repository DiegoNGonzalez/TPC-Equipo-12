<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCursos.aspx.cs" Inherits="TPC_equipo_12.ProfesorCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container position-relative">
        <div class="row position-relative">
            <div class="col-md-12 mb-1 text-center mt-2">
                <h2 class="d-inline">Cursos Disponibles</h2>
                <div class="position-absolute top-0 end-0 d-flex mt-3 ">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control me-2" Placeholder="Buscar cursos..." onkeydown="return handleEnter(event);" />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary me-2" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiar_Click" />
                </div>
                <div class="mt-3">
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row position-relative">
            <div class="col-md-12">
                <div class="position-absolute top-0 end-0 d-flex">
                    <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select me-2">
                        <asp:ListItem Text="Todas" Value="0" />
                    </asp:DropDownList>
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary me-2" OnClick="btnFiltrar_Click" />
                    <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiarFiltro_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
        <ContentTemplate>
            <div class="row justify-content-center mt-5">
                <asp:Repeater ID="rptProfesorCursos" runat="server">
                    <ItemTemplate>
                        <div class="card ms-5 mb-5" style="width: 24rem; min-height: 28rem;">
                            <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                            <asp:LinkButton ID="LinkButtonCursoProf" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonCursoProf_Command" Style="text-decoration: none;">
                                    <div class="img-container mt-2" style="height: 200px; overflow: hidden; display: flex; align-items: center; justify-content: center;">
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
                                <li class="list-group-item">Categoria:  <%# Eval("Categoria.Nombre") %>
                                  
                                </li>
                            </ul>
                            <div class="d-flex justify-content-center align-items-center mt-2">
                                <asp:Button ID="ButtonDeshabilitarCurso" runat="server" Text="Deshabilitar Curso" CssClass="btn btn-danger btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonDeshabilitarCurso_Command" />
                                <asp:Button ID="ButtonFabrica" runat="server" Text="Volver a la Fabrica" CssClass="btn btn-primary btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonFabrica_Command" />
                                <asp:Button ID="ButtonResenias" runat="server" Text="Reseñas" CssClass="btn btn-secondary btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonResenias_Command" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>

         <div class="container position-relative">
             <div class="row position-relative">
                 <div class="col-md-12 mb-1 text-center mt-2">
                     <h2 class="d-inline">Cursos Deshabilitados</h2>
                     <div class="position-absolute top-0 end-0 d-flex mt-3">
                         <asp:TextBox ID="TextBoxInactivo" runat="server" CssClass="form-control me-2" Placeholder="Busqueda de cursos..." onkeydown="return handleEnter(event);" />
                         <asp:Button ID="btnBuscarInactivo" runat="server" Text="Buscar" CssClass="btn btn-primary me-2" OnClick="btnBuscarInactivo_Click" />
                         <asp:Button ID="btnLimpiarInactivo" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiarInactivo_Click" />
                     </div>
                     <div class="mt-3">
                         <asp:Label ID="Label2" runat="server" CssClass="text-danger"></asp:Label>
                     </div>
                 </div>
             </div>
             <div class="row position-relative">
                 <div class="col-md-12">
                     <div class="position-absolute top-0 end-0 d-flex">
                         <asp:DropDownList ID="ddlInactivo" runat="server" CssClass="form-select me-2">
                             <asp:ListItem Text="Todas" Value="0" />
                         </asp:DropDownList>
                         <asp:Button ID="btnFiltrarInactivo" runat="server" Text="Filtrar" CssClass="btn btn-primary me-2" OnClick="btnFiltrarInactivo_Click" />
                         <asp:Button ID="btnLimpiarFiltroInactivo" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiarFiltroInactivo_Click" />
                     </div>
                 </div>
             </div>
         </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMensajeInactivo" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanelCursosInactivos" runat="server">
        <ContentTemplate>
            <div class="row justify-content-center mt-5">
                <asp:Repeater ID="RepeaterCursosInactivos" runat="server">
                    <ItemTemplate>
                        <div class="card ms-5 mb-5 " style="width: 24rem; min-height: 28rem; opacity: 0.5;">
                            <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                            <div class="img-container mt-2" style="height: 200px; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                                <img src='<%# Eval("Imagen.URL") %>' class="card-img-top img-fluid" alt="..." style="max-height: 100%; width: auto;">
                            </div>
                            <div class="card-body text-center">
                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            </div>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item text-truncate" onclick="expandirDescripcion(this);">
                                    <%# Eval("Descripcion") %>
                                </li>
                                <li class="list-group-item">Duración: <%# Eval("Duracion") %> hs.</li>
                                <li class="list-group-item">Categoria: <%# Eval("Categoria.Nombre") %>
                                     
                                </li>
                            </ul>
                            <div class="d-flex justify-content-center align-items-center mt-2">
                                <asp:Button ID="ButtonHabilitar" runat="server" Text="Habilitar Curso" CssClass="btn btn-success btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonHabilitar_Command" />
                                <asp:Button ID="ButtonFabrica" runat="server" Text="Volver a la Fabrica" CssClass="btn btn-primary btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonFabrica_Command" />
                                <asp:Button ID="ButtonResenias" runat="server" Text="Reseñas" CssClass="btn btn-secondary btn-sm mx-2 mb-2" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="ButtonResenias_Command" />
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
        function handleEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();


                __doPostBack('<%= btnBuscar.UniqueID %>', '');

                return false;
            }
            return true;
        }
    </script>
</asp:Content>
