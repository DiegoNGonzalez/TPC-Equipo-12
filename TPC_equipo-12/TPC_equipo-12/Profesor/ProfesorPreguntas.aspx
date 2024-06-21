<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorPreguntas.aspx.cs" Inherits="TPC_equipo_12.ProfesorPreguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <<div class="container">
        <asp:Button ID="btnVolver" CssClass="btn btn-primary mb-3 mt-3" runat="server" Text="Volver" OnClick="btnVolver_Click" />
        <!-- Repeater para mostrar el comentario padre -->
        <asp:Repeater ID="rptComentarioPadre" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Comentario Principal</h5>
                        <p class="card-text"><strong><%# Eval("Nombre") %>:</strong> <%# Eval("CuerpoComentario") %></p>
                        <p class="card-text"><small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Repeater para mostrar las respuestas -->
        <asp:Repeater ID="rptRespuestas" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Respuesta</h5>
                        <p class="card-text"><strong><%# Eval("Nombre") %>:</strong> <%# Eval("CuerpoComentario") %></p>
                        <p class="card-text"><small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></small></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <!-- Formulario para agregar respuesta -->
        <div class="mt-5">
            <div class="card mt-3">
                <div class="card-body">
                    <asp:TextBox ID="txtRespuesta" CssClass="form-control mb-3" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Escribe tu respuesta aquí..."></asp:TextBox>
                    <asp:Button ID="btnResponder" CssClass="btn btn-primary" runat="server" Text="Responder" OnClick="btnResponder_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
