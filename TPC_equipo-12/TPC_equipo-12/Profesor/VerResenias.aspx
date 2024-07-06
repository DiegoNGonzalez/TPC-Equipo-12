<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="VerResenias.aspx.cs" Inherits="TPC_equipo_12.VerResenias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
        <div class="row">
            <div class="col-12">
                <h5 class="mt-4" id="lblResenia" runat="server">Reseñas:</h5>
                <asp:Label ID="LabelNoHayResenias" runat="server" Visible="false"></asp:Label>
                <asp:Repeater ID="RepeaterResenias" runat="server">
                    <ItemTemplate>
                        <ul class="list-group mb-4">
                            <li class="list-group-item d-flex justify-content-between align-items-start">
                                <span class="badge bg-success rounded-pill"><%# Eval("Calificacion") %></span>
                                <div class="ms-2 me-auto">
                                    <div class="fw-bold"><%# Eval("Estudiante.NombreCompleto") %></div>
                                    <%# Eval("Comentario") %>.
                                </div>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


    </div>
</asp:Content>
