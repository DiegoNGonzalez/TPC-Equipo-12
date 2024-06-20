<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerDetalleCurso.aspx.cs" Inherits="TPC_equipo_12.VerDetalleCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Detalle del Curso</h1>
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="RepeaterVerDetalleCurso" runat="server" OnItemDataBound="RepeaterVerDetalleCurso_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <h2><%# Eval("Nombre") %></h2>
                                <p>Descripcion: <%# Eval("Descripcion") %></p>
                                <p>Duracion: <%# Eval("Duracion") %> hs.</p>
                                <p>Fecha estreno: <%# Eval("Estreno") %></p>
                                <asp:Repeater ID="RepeaterUnidades" runat="server">
                                    <ItemTemplate>
                                        <ul>
                                            <li>
                                                <p><%# Eval("Nombre") %></p>
                                                <p><%# Eval("Descripcion") %></p>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <p>Categoria: <%# Eval("Categoria") %></p>
                                <img src='<%# Eval("Imagen.URL") %>'>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
