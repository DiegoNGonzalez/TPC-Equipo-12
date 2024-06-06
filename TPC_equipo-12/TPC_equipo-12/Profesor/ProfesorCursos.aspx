<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorCursos.aspx.cs" Inherits="TPC_equipo_12.ProfesorCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <h1 class="text-center">Cursos Cargados</h1>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="rptProfesorCursos" runat="server">
                <ItemTemplate>
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
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
