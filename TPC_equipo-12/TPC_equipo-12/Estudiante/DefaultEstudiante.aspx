<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="DefaultEstudiante.aspx.cs" Inherits="TPC_equipo_12.DefaultEstudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1 class="text-center">Bienvenido Estudiante</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 mb-5">
                <h2 class="text-center">Cursos Disponibles</h2>
            </div>
        </div>
        <div class="row mb-5 text-center justify-content-center">
            <div class="col-md-3">
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" Placeholder="Busqueda de cursos..." onkeydown="return handleEnter(event);"  />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="bg-primary rounded mb-5" style="width: 80px;">
            <asp:CheckBox ID="chkFiltrar" runat="server" CssClass="px-1 ms-1" OnCheckedChanged="chkFiltrar_CheckedChanged" AutoPostBack="True" />
            <asp:Label class="form-check-label text-white" runat="server" for="chkFiltrar">Filtrar</asp:Label>
        </div>
        <div class="row mb-5 text-center justify-content-center" id="Filtrado" runat="server">

            <div class="col-md-4">
                <asp:Label ID="lblCategoria" runat="server" Text="Categoria" CssClass="text-white text-decoration-underline"></asp:Label>
                <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select mb-1">
                    <asp:ListItem Text="Todas" Value="0" />
                </asp:DropDownList>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnLimpiarFiltro_Click" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanelCursos" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <asp:Repeater ID="rptCursos" runat="server">
                        <ItemTemplate>
                            <div class="card ms-5 mb-5" style="width: 24rem; min-height: 28rem;">
                                <asp:HiddenField ID="HiddenFieldIDCurso" runat="server" Value='<%# Eval("IDCurso") %>' />
                                <asp:Label ID="lblIDCurso" runat="server" Text='<%# Eval("IDCurso") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="LinkButtonEstudiante" runat="server" CommandArgument='<%# Eval("IDCurso") %>' OnCommand="LinkButtonEstudiante_Command" Style="text-decoration: none;">
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
                                    <li class="list-group-item">Categoria: <%# Eval("Categoria.Nombre") %>
                                
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
