<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteInscripciones.aspx.cs" Inherits="TPC_equipo_12.EstudianteInscripciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h3 class="text-center mt-3 mb-3">Mis inscripciones</h3>
                <hr />
                <div class="col-md-12 text-center">
                <asp:Label ID="LabelNoHayInscripciones" runat="server" CssClass="display-7 mt-5" Visible="false"><b>No te has inscripto a ningun curso.</b></asp:Label>
                    </div>
                <asp:Panel ID="PanelMisInscripciones" runat="server">

                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Nro Inscripcion</th>
                            <th>Nombre</th>
                            <th>Apellido</th>
                            <th>Curso</th>
                            <th>Fecha Inscripcion</th>
                            <th>Estado</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptInscripciones" runat="server" OnItemDataBound="rptInscripciones_ItemDataBound">
                            <ItemTemplate>

                                <tr>
                                    <td><%# Eval("IdInscripcion") %></td>
                                    <td><%# Eval("Usuario.Nombre") %></td>
                                    <td><%# Eval("Usuario.Apellido") %></td>
                                    <td><%# Eval("Curso.Nombre") %></td>
                                    <td><%# Eval("FechaInscripcion") %></td>
                                    <td><%# 
                                            ((char)Eval("Estado")) == 'R' ? "Rechazada" : 
                                            ((char)Eval("Estado")) == 'P' ? "Pendiente" : ((char)Eval("Estado")=='C'? "Desuscripto": "Aceptada")
                                    %>

                                    </td>
                                    <td>
                                         <asp:Button ID="btnReinscribir" runat="server" CssClass="btn btn-secondary btn-sm" Text="Reinscribir" OnClick="btnReinscribir_Click" Visible="false" CommandArgument='<%# Eval("IdInscripcion") + "," + Eval("Curso.IDCurso") %>' />
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
