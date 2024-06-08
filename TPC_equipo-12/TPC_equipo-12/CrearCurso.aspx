<%@ Page Title="" Language="C#" MasterPageFile="~/Profesor/ProfesorMasterPage.Master" AutoEventWireup="true" CodeBehind="CrearCurso.aspx.cs" Inherits="TPC_equipo_12.CrearCurso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
        <div class="card w-50 my-5">
            <div class="card-body">
                <h5 class="card-title d-flex justify-content-center align-items-center">Crear Curso</h5>
                <div class="mb-3">
                    <label for="InputNombreCurso" class="form-label">Nombre</label>
                    <input type="text" class="form-control" id="InputNombreCurso">
                </div>
                <div class="mb-3">
                    <label for="InputDescripcionCurso" class="form-label">Descripcion</label>
                    <input type="text" class="form-control" id="InputDescripcionCurso">
                </div>
                <div class="mb-3">
                    <label for="InputEstrenoCurso" class="form-label">Fecha de estreno</label>
                    <input type="number" class="form-control" id="InputEstrenoCurso">
                </div>
                <div class="mb-3">
                    <label for="InputDuracionCurso" class="form-label">Duracion del curso (en hs)</label>
                    <input type="text" class="form-control" id="InputDuracionCurso">
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <label for="InputImagenCurso" class="form-label">URL Imagen</label>
                            <asp:TextBox runat="server" ID="TextBoxUrlImagen" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBoxUrlImagen_TextChanged" />
                        </div>
                        <div class="mb-3 d-flex justify-content-center align-items-center">
                            <asp:Image ImageUrl="https://vilmanunez.com/wp-content/uploads/2016/04/VN-Como-crear-el-mejor-temario-de-tu-curso-online-Incluye-plantillas.png" ID="urlImagenCurso" width="60%" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="d-flex flex-column align-items-center">
                <div class="mb-3">
                    <button type="submit" class="btn btn-primary">Agregar Unidades</button>
                </div>
                <button type="submit" class="btn btn-primary">Crear Curso</button>
            </div>
                </div>
        </div>
    </div>
</asp:Content>
