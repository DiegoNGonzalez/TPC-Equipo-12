<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorMateriales.aspx.cs" Inherits="TPC_equipo_12.ProfesorMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:Button ID="ButtonBackLeccionesProf" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver a Lecciones" OnClick="ButtonBackLeccionesProf_Click" />
        <h1 class="text-center">Materiales de la Lección</h1>
        <asp:Repeater ID="rptMaterialesProf" runat="server" OnItemDataBound="rptMaterialesProf_ItemDataBound">
            <ItemTemplate>
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    </div>
                    <div class="card-body">
                        <asp:Literal ID="ltlYoutubeVideo" runat="server"></asp:Literal>
                        <asp:Literal ID="ltlDocumento" runat="server"></asp:Literal>
                        <div class="card mb-3">
                            <div class="card-body">
                                <p class="card-text"><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
