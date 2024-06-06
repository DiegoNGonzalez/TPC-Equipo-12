﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Estudiante/EstudianteMasterPage.Master" AutoEventWireup="true" CodeBehind="EstudianteMateriales.aspx.cs" Inherits="TPC_equipo_12.EstudianteMateriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div class="container mt-5">
        <asp:Repeater ID="rptMateriales" runat="server" OnItemDataBound="rptMateriales_ItemDataBound">
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