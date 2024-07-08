<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="ProfesorEstudiantesXCurso.aspx.cs" Inherits="TPC_equipo_12.ProfesorEstudiantesXCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnVolver" CssClass="btn btn-primary mt-3" runat="server" Text="Volver" OnClick="btnVolver_Click" />
                <div class="col-md-12 text-center">
                <asp:Label ID="lblNoInscripciones" runat="server" Text="" Visible="false"><b>No hay inscripciones en este curso</b></asp:Label>
                    </div>
                <asp:Panel ID="pnlTablaInscripciones" runat="server">
                    <h3 class="text-center mb-3">Estudiantes Inscriptos</h3>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Nro Inscripcion</th>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Fecha Inscripcion</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptInscripciones" runat="server">
                                <ItemTemplate>

                                    <tr>
                                        <td><%# Eval("IdInscripcion") %></td>
                                        <td><%# Eval("Usuario.Nombre") %></td>
                                        <td><%# Eval("Usuario.Apellido") %></td>
                                        <td><%# Eval("FechaInscripcion") %></td>
                                        <td>
                                            <asp:Button ID="btnCancelarInscripcion" runat="server" Text="Cancelar Inscripcion" CssClass="btn btn-danger" OnClick="btnCancelarInscripcion_Click" CommandArgument='<%# Eval("IdInscripcion") %>' />
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
