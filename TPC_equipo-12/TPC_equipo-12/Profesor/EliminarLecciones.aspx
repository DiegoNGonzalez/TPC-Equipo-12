<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="EliminarLecciones.aspx.cs" Inherits="TPC_equipo_12.EliminarLecciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
            <div class="card w-50 my-5">
                <div class="card-body">
                    <h5 class="card-title d-flex justify-content-center align-items-center">Elige el nombre de la leccion a eliminar</h5>
                    <div class="mb-3">
                        <asp:Label ID="LabelNombreLeccion" runat="server" CssClass="form-label" Text="Nombre de la Leccion"></asp:Label>
                        <asp:DropDownList ID="DropDownListNombreLeccion" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="ButtonEliminarLeccion" runat="server" Text="Eliminar Leccion" CssClass="btn btn-danger" OnClick="ButtonEliminarLeccion_Click" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
