<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="VerMensaje.aspx.cs" Inherits="TPC_equipo_12.VerMensaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="container">
    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary mt-3 mb-3" OnClick="btnVolver_Click" />
    <div class="text-center mb-3">
        <asp:Label ID="MensajeTitulo" runat="server" CssClass="h3">Mensajes</asp:Label>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-3 rounded">
                <div class="card-body">
                    <div class="form-group">
                        <asp:Label ID="lblDe" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblAsunto" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div id="respuestasContainer" class="mb-3">
                <div class="card rounded">
                    <div class="card-body">
                        <asp:Literal ID="ltlRespuestas" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlResponder" runat="server" Visible="false">
                <div class="card mb-3 rounded">
                    <div class="card-body">
                        <div class="form-group">
                            <label><b>Respuesta:</b></label>
                            <asp:TextBox ID="txtRespuesta" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnEnviarRespuesta" runat="server" Text="Enviar Respuesta" CssClass="btn btn-primary mt-3" OnClick="btnEnviarRespuesta_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Button ID="btnResponder" runat="server" Text="Responder" CssClass="btn btn-primary mb-3" OnClick="btnResponder_Click"/> 
        </div>
    </div>
</div>

</asp:Content>
