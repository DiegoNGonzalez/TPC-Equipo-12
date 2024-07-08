<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="CrearCurso.aspx.cs" Inherits="TPC_equipo_12.CrearCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container d-flex justify-content-center align-items-center flex-column" style="min-height: 100vh;">
        <div class="card w-50 my-5">
            <div class="card-body">
                <asp:Label ID="LabelTitulo" runat="server" CssClass="card-title d-flex justify-content-center align-items-center h4" Text="">Crear Curso</asp:Label>
                <div class="mb-3">
                    <asp:Label ID="LabelNombreCurso" runat="server" CssClass="form-label" Text=""><b>Nombre</b></asp:Label>
                    <asp:TextBox ID="TextBoxNombreCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDescripcionCurso" runat="server" CssClass="form-label" Text=""><b>Descripcion</b></asp:Label>
                    <asp:TextBox ID="TextBoxDescripcionCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelEstrenoCurso" runat="server" CssClass="form-label" Text=""><b>Fecha de estreno</b></asp:Label>
                    <asp:TextBox ID="TextBoxEstrenoCurso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelCategoriaCurso" runat="server" CssClass="form-label" Text=""><b>Categoría</b></asp:Label>
                    <asp:DropDownList ID="DropDownListCategoriaCurso" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <asp:Label ID="LabelDuracionCurso" runat="server" CssClass="form-label" Text=""><b>Duración del curso (en HS)</b></asp:Label>
                    <asp:TextBox ID="TextBoxDuracionCurso" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:Label ID="LabelImagenCurso" runat="server" CssClass="form-label" Text=""><b>Imagen del curso</b></asp:Label>
                            <input type="file" id="fileImagenCurso" runat="server" class="form-control" />
                        </div>
                        <div class="mb-3 d-flex justify-content-center align-items-center">
                            <asp:Image ID="urlImagenCurso" runat="server" CssClass="img-fluid" Width="60%" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="d-flex justify-content-center align-items-center">
                    <asp:Button ID="ButtonCrearCurso" runat="server" Text="CrearCurso" CssClass="btn btn-primary mx-4" OnClick="ButtonCrearCurso_Click" />
                    <asp:Button ID="ButtonVolver" runat="server" Text="Volver" CssClass="btn btn-secondary mx-4" OnClick="ButtonVolver_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
