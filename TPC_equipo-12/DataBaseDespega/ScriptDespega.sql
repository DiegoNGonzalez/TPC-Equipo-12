Create Database DB_DESPEGAv2
GO
Use DB_DESPEGAv2
GO
Create Table Imagenes(
	IDImagenes int not null primary key identity(1, 1),
	URLIMG varchar(500) not null
)
GO
Create Table Usuarios(
	IDUsuario int not null primary key identity(1, 1),
	Nombre varchar(100) not null,
	Apellido varchar(100) not null,
	DNI int not null unique,
	Genero char null check(Genero='M' or Genero='F' or Genero='X'),
	Email varchar(100) not null unique,
	EsProfesor bit not null default 0,
	IDImagen int null Foreign Key References Imagenes(IDImagenes),
	ContraseniaHash VARCHAR(255) NOT NULL, 
    ContraseniaSalt VARCHAR(255) NOT NULL
)
GO
Create Table Categorias(
	IDCategoria int not null Primary Key Identity(1,1),
	Nombre varchar(100) not null unique
)
GO
Create Table Cursos(
	IDCurso int not null Primary Key Identity(1,1),
	Nombre varchar(200) not null,
	Descripcion varchar(500) not null,
	IDImagen int null Foreign Key References Imagenes(IDImagenes),
	Estreno DateTime not null,
	Duracion int not null,
)
GO
Create Table Inscripciones(
	IDInscripcion int not null unique Identity(1, 1),
	IDusuario int not null Foreign Key References Usuarios(IDUsuario),
	IDCurso int not null Foreign Key References Cursos(IDCurso),
	Estado char(1) not null Default 'P' check (Estado= 'A' or Estado='P' or Estado = 'R'),
	FechaInscripcion DATETIME not null default getdate(),
	Primary Key (IDUsuario, IDCurso)
)
GO
Create Table Profesor(
	IDProfesor int not null primary key Foreign Key References Usuarios(IDUsuario),
)
GO
Create Table ProfesorXCursos(
	IDProfesor int not null Foreign Key References Profesor(IDProfesor),
	IDCurso int not null Foreign Key References Cursos(IDCurso)
	Primary Key (IDProfesor, IDCurso)
)
GO
Create Table Estudiantes(
	IDEstudiante int not null Primary Key Foreign Key References Usuarios(IDUsuario),
	Estado bit not null 
)
GO
Create Table EstudiantesXCursos(
	IDEstudiante int not null Foreign Key References Estudiantes(IDEstudiante),
	IDCurso int not null Foreign Key References Cursos(IDCurso),
	Completado bit not null Default 0,
	Primary Key (IDEstudiante, IDCurso)
)
GO
Create Table CategoriasXCurso(
	IDCurso int not null Foreign Key References Cursos(IDCurso),
	IDCategoria int not null Foreign Key References Categorias(IDCategoria)
	Primary Key (IDCurso, IDCategoria)
)
GO
Create Table Materiales(
	IDMaterial int not null Primary Key Identity(1,1),
	Nombre varchar(100) not null,
	TipoMaterial varchar(100) not null,
	URLMaterial varchar(200),
	Descripcion varchar(500),
	NroMaterial int not null
)
GO
Create Table Lecciones(
	IDLeccion int not null Primary Key Identity(1,1),
	NroLeccion int not null,
	Nombre varchar(100) not null,
	Descripcion varchar(500) not null,
)
GO
Create Table Comentarios(
	IDComentario int not null PRIMARY KEY IDENTITY(1,1),
	IDComentarioPadre int null,
	IDLeccion int not null Foreign Key References Lecciones(IDLeccion),
	IDUsuarioEmisor int not null Foreign Key References Usuarios(IDUsuario),
	CuerpoComentario varchar(500) not null,
	FechaCreacion datetime not null,
	IDImagen INT NULL FOREIGN KEY REFERENCES Imagenes(IDImagenes),
	Estado bit not null Default 1
 )
 GO  
 
Create Table LeccionesXEstudiantes(
	IDEstudiante int not null Foreign Key References Estudiantes(IDEstudiante),
	IDLeccion int not null Foreign Key References Lecciones(IDLeccion),
	Completado bit not null Default 0,
	Primary Key (IDEstudiante, IDLeccion)
)
GO
Create Table MaterialesXLecciones(
	IDMaterial int not null Foreign Key References Materiales(IDMaterial),
	IDLeccion int not null Foreign Key References Lecciones(IDLeccion),
	Primary Key (IDMaterial, IDLeccion)
)
GO
Create Table Unidades(
	IDUnidad int not null Primary Key Identity(1, 1),
	NroUnidad int not null,
	Nombre varchar(100) not null,
	Descripcion varchar(100) not null,
)
GO
Create Table LeccionesXUnidades(
	IDUnidad int not null Foreign Key References Unidades(IDUnidad),
	IDLeccion int not null Foreign Key References Lecciones(IDLeccion),
	Primary Key (IDUnidad, IDLeccion)
)
GO
Create Table UnidadesXCurso(
	IDUnidad int not null Foreign Key References Unidades(IDUnidad),
	IDCurso int not null Foreign Key References Cursos(IDCurso),
	Primary Key (IDUnidad, IDCurso)
)
GO
Create Table Mensajes(
	IDMensaje int not null Primary Key identity(1, 1),
	Mensaje varchar(500) not null,
	IDEmisor int not null Foreign Key References Usuarios(IDusuario),
	IDReceptor int not null Foreign Key References Usuarios(IDusuario),
	FechaHora datetime not null default(getdate()),
	Asunto varchar(100) null,
	Leido bit not null DEFAULT 0
)
GO
Create Table MensajesXUsuario(
	IDMensaje int not null Foreign Key References Mensajes(IDMensaje),
	IDUsuario int not null Foreign Key References Usuarios(IDUsuario),
	Primary Key (IDMensaje, IDUsuario)
)
GO
CREATE TABLE Respuestas (
    IDRespuesta INT PRIMARY KEY IDENTITY,
    IDMensaje INT FOREIGN KEY REFERENCES Mensajes(IDMensaje) ON DELETE CASCADE,
    Respuesta varchar(5000) not null,
    FechaHora DATETIME not null default getdate(),
    IDEmisor  int not null FOREIGN key REFERENCES Usuarios(IDusuario)
)
GO
Create Table Notificaciones(
	IDNotificacion int not null Primary Key Identity(1, 1),
	Mensaje varchar(200) not null,
	Tipo varchar(100) not null check(Tipo='INSCRIPCION' or Tipo='MENSAJE' or Tipo='RESPUESTA'),
	Fecha datetime not null default(getdate()),
	Leido bit not null default 0,
	IDInscripcion int Foreign Key References Inscripciones(IDInscripcion),
	IDMensaje int Foreign Key References Mensajes(IDMensaje) ,
	IDRespuesta int FOREIGN KEY REFERENCES Respuestas(IDRespuesta) 
)
GO
Create Table Resenias(
	IDResenia int not null Primary Key Identity(1, 1),
	IDEstudiante int not null Foreign Key References Estudiantes(IDEstudiante),
	Resenia varchar(200) not null,
	Calificacion int not null check(Calificacion between 1 and 10),
	Fecha datetime not null default(getdate())
)
GO
Create Table ReseniasXCurso(
	IDCurso int not null Foreign Key References Cursos(IDCurso),
	IDResenia int not null Foreign Key References Resenias(IDResenia),
	Primary Key (IDCurso, IDResenia)
)
GO
Create Table NotificacionesXUsuario(
		IDNotificacion int not null FOREIGN KEY REFERENCES Notificaciones (IDNotificacion),
		IDUsuario int not null Foreign Key References Usuarios (IDUsuario) 
		Primary Key (IDNotificacion, IDUsuario)
)
GO