<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="VerMensaje.aspx.cs" Inherits="TPC_equipo_12.VerMensaje1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="container">
             <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
    <div class="row">
        <div class="col-md-12">
            <h3>Ver Mensaje</h3>
            <hr />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <asp:Label ID="lblDe" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAsunto" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
            <hr />
            <div id="respuestasContainer"> 
                <asp:Literal ID="ltlRespuestas" runat="server"></asp:Literal>
            </div>

            <asp:Panel ID="pnlResponder" runat="server" Visible="false">
                <div class="form-group">
                    <label>Respuesta:</label>
                    <asp:TextBox ID="txtRespuesta" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnEnviarRespuesta" runat="server" Text="Enviar Respuesta" CssClass="btn btn-primary" OnClick="btnEnviarRespuesta_Click" />
                </div>
            </asp:Panel>

            <asp:Button ID="btnResponder" runat="server" Text="Responder" CssClass="btn btn-primary" OnClick="btnResponder_Click"/> 
        </div>
    </div>
</div>
</asp:Content>
