<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorMensajes.aspx.cs" Inherits="TPC_equipo_12.ProfesorMensajes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <h1>Mensajes Recibidos</h1>
                <hr />
            </div>
        </div>
        <asp:Button ID="btnNuevoMensaje" runat="server" Text="Nuevo Mensaje" CssClass="btn btn-primary" OnClick="btnNuevoMensaje_Click" />
        <asp:Panel ID="PanelMensajes" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Fecha</th>
                                    <th>De</th>
                                    <th>Asunto</th>
                                    <th>Leido</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptMensajes" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("FechaHora") %></td>
                                            <td><%# Eval("UsuarioEmisor.Nombre") %> <%# Eval("UsuarioEmisor.Apellido")  %></td>
                                            <td><%# Eval("asunto") %></td>

                                            <td>
                                                <%# 
                                                (bool)Eval("Leido")?"Si":"No" 
    
                                                %>

                                            </td>
                                            <td>
                                                <asp:Button ID="btnVerMensaje" runat="server" Text="Ver" CommandArgument='<%# Eval("IdMensaje") %>' CssClass="btn btn-primary" OnClick="btnVerMensaje_Click" OnCommand="btnVerMensaje_Command" />
                                                <asp:LinkButton ID="btnBorrarMensaje" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdMensaje") %>' CssClass="btn btn-danger" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            </asp:Panel>
            <div class="row">
    <div class="col-md-12">
        <h1>Mensajes Enviados</h1>
        <hr />
    </div>
</div>
            <asp:Panel ID="PanelMensajesEnviados" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>Fecha</th>
                                        <th>Para</th>
                                        <th>Asunto</th>
                                        <th>Leido</th>
                                        <th>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptMensajesEnviados" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("FechaHora") %></td>
                                                <td><%# Eval("UsuarioReceptor.Nombre") %> <%# Eval("UsuarioReceptor.Apellido")  %></td>
                                                <td><%# Eval("asunto") %></td>

                                                <td>
                                                    <%# 
                                                    (bool)Eval("Leido")?"Si":"No" 
        
                                                    %>

                                                </td>
                                                <td>
                                                    <asp:Button ID="btnVerMensajeEnviado" runat="server" Text="Ver" CommandArgument='<%# Eval("IdMensaje") %>' CssClass="btn btn-primary" OnClick="btnVerMensajeEnviado_Click" OnCommand="btnVerMensajeEnviado_Command"/>
                                                    <asp:Button ID="btnBorrarMensajeEnviado" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdMensaje") %>' CssClass="btn btn-danger" onclick="btnBorrarMensajeEnviado_Click"/>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
        </asp:Panel>
        <div class="d-flex justify-content-center align-items-start">
            <asp:Label ID="LabelNoHayMensajes" runat="server" CssClass="display-4 font-weight-bold mt-5" Visible="false">No recibiste Mensajes aun!</asp:Label>
        </div>
        </div>
</asp:Content>
