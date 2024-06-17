USE DB_DESPEGAv2
go
-- Insertar imagenes
INSERT INTO Imagenes (URLIMG) VALUES ('https://i.ytimg.com/vi/p73DM4ueE7s/maxresdefault.jpg');
INSERT INTO Imagenes (URLIMG) VALUES ('https://i.ytimg.com/vi/VnbKPJA2ykU/maxresdefault.jpg');
INSERT INTO IMagenes(URLIMG) VALUES ('https://www.shutterstock.com/image-vector/blank-avatar-photo-place-holder-600nw-1095249842.jpg');
INSERT INTO Imagenes (URLIMG) VALUES ('https://i.ytimg.com/vi/7TKY-jksHRQ/maxresdefault.jpg');
INSERT INTO Imagenes (URLIMG) VALUES ('https://cdn.goconqr.com/uploads/media/image/10324463/desktop_cb3541ed-ab38-450f-9403-e800ac7d09f5.jpg');


go
-- Insertar usuarios
INSERT INTO Usuarios (Nombre, Apellido, DNI, Genero, Email, EsProfesor, IDImagen, Contrasenia)
VALUES ('Maxi', 'Programa', 12345678, 'M', 'hola@maxiprograma.com', 1, 3, 'contraseniaSegura123');
go
INSERT INTO Usuarios (Nombre, Apellido, DNI, Genero, Email, EsProfesor, IDImagen, Contrasenia)
VALUES ('Diego', 'Gonzalez', 87654321, 'M', 'diego.gonzalez2@alumnos.frgp.utn.edu.ar', 0, 3, 'contraseniaSegura456');
go
INSERT INTO Usuarios (Nombre, Apellido, DNI, Genero, Email, EsProfesor, IDImagen, Contrasenia)
VALUES ('Tomas', 'Caceres', 23456789, 'M', 'tomas.caceres2@alumnos.frgp.utn.edu.ar', 0, 3, 'contraseniaSegura789');
go
INSERT INTO Usuarios (Nombre, Apellido, DNI, Genero, Email, EsProfesor, IDImagen, Contrasenia)
VALUES ('Sebastian', 'Scollo', 98765432, 'M', 'sebastian.scollo@alumnos.frgp.utn.edu.ar', 0, 3, 'contraseniaSegura012');
go
-- Insertar profesor
INSERT INTO Profesor (IDProfesor) VALUES (1);
go
-- Insertar estudiantes
INSERT INTO Estudiantes (IDEstudiante, Estado) VALUES (2, 1);
INSERT INTO Estudiantes (IDEstudiante, Estado) VALUES (3, 1);
INSERT INTO Estudiantes (IDEstudiante, Estado) VALUES (4, 1);
go
-- Insertar categorias
INSERT INTO Categorias (Nombre) VALUES ('Matematicas');
INSERT INTO Categorias (Nombre) VALUES ('Ciencia');
INSERT INTO Categorias (Nombre) VALUES ('Programación');
INSERT INTO Categorias (Nombre) VALUES ('Historia');
go
-- Insertar cursos
INSERT INTO Cursos (Nombre, Descripcion, IDImagen, Estreno, Duracion)
VALUES ('Curso de Algebra', 'Un curso completo sobre algebra.', 1, '2023-01-01 10:00:00', 120);
go
INSERT INTO Cursos (Nombre, Descripcion, IDImagen, Estreno, Duracion)
VALUES ('Curso de Fisica', 'Un curso completo sobre fisica.', 2, '2023-02-01 10:00:00', 150);
go
INSERT INTO Cursos (Nombre, Descripcion, IDImagen, Estreno, Duracion)
VALUES ('Curso de Programación', 'Un curso completo sobre programación.', 4, '2023-03-01 10:00:00', 200);
go
INSERT INTO Cursos (Nombre, Descripcion, IDImagen, Estreno, Duracion)
VALUES ('Curso de Historia', 'Un curso completo sobre historia.', 5, '2023-04-01 10:00:00', 180);
go
-- Insertar profesor por curso
INSERT INTO ProfesorXCursos (IDProfesor, IDCurso) VALUES (1, 1);
INSERT INTO ProfesorXCursos (IDProfesor, IDCurso) VALUES (1, 2);
INSERT INTO ProfesorXCursos (IDProfesor, IDCurso) VALUES (1, 3);
INSERT INTO ProfesorXCursos (IDProfesor, IDCurso) VALUES (1, 4);
go
-- Insertar inscripciones
INSERT INTO Inscripciones (IDusuario, IDCurso, Estado) VALUES (2, 1, 'A');
INSERT INTO Inscripciones (IDusuario, IDCurso, Estado) VALUES (2, 2, 'A');
INSERT INTO Inscripciones (IDusuario, IDCurso, Estado) VALUES (3, 3, 'A');
INSERT INTO Inscripciones (IDusuario, IDCurso, Estado) VALUES (4, 4, 'A');
go
-- Insertar estudiantes por curso
INSERT INTO EstudiantesXCursos (IDEstudiante, IDCurso, Completado) VALUES (2, 1, 1);
INSERT INTO EstudiantesXCursos (IDEstudiante, IDCurso, Completado) VALUES (2, 2, 0);
INSERT INTO EstudiantesXCursos (IDEstudiante, IDCurso, Completado) VALUES (3, 3, 0);
INSERT INTO EstudiantesXCursos (IDEstudiante, IDCurso, Completado) VALUES (4, 4, 0);
go
-- Insertar categorias por curso
INSERT INTO CategoriasXCurso (IDCurso, IDCategoria) VALUES (1, 1);
INSERT INTO CategoriasXCurso (IDCurso, IDCategoria) VALUES (2, 2);
INSERT INTO CategoriasXCurso (IDCurso, IDCategoria) VALUES (3, 3);
INSERT INTO CategoriasXCurso (IDCurso, IDCategoria) VALUES (4, 4);
go
-- Insertar materiales con NroMaterial al final
INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Algebra', 'Documento', 'https://sitios.ucsc.cl/pace/wp-content/uploads/sites/41/2020/03/ManualAlgebra.pdf', 'Manual de Álgebra para estudiantes universitarios', 1);
GO

INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Fisica', 'Video', 'https://www.youtube.com/watch?v=0vlAAjszSFA', 'Video explicativo sobre fundamentos de Física', 1);
GO

INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Programación', 'Documento', 'https://sanfrancisco.utn.edu.ar/documentos/archivos/cursillo_de_ingreso/2024/Intro_%20a%20la%20Programaci%C3%B3n%20-%20S_%20Ingreso%20TUP.pdf', 'Introducción a la programación para principiantes', 1);
GO

INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Historia', 'Video', 'https://www.youtube.com/watch?v=qgw4D_9t_mw', 'Documental sobre la historia antigua', 1);
GO

INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Variables y Tipos de Datos', 'Documento', 'https://departamento.us.es/edan/php/asig/LICFIS/LFIPC/Tema2FISPC0809.pdf', 'Guía sobre variables y tipos de datos en programación', 1);
GO

INSERT INTO Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial)
VALUES ('Material Edad Media', 'Video', 'https://www.youtube.com/watch?v=DjdFLJT5lhY', 'Exploración de la Edad Media en Europa', 1);
GO

-- Insertar lecciones
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (1, 'Introducción al Algebra', 'Esta lección cubre los conceptos básicos de algebra.');
go
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (1, 'Introducción a la Fisica', 'Esta lección cubre los conceptos básicos de fisica.');
go
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (1, 'Introducción a la Programación', 'Esta lección cubre los conceptos básicos de programación.');
go
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (1, 'Introducción a la Historia', 'Esta lección cubre los conceptos básicos de historia.');
go
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (2, 'Variables y Tipos de Datos', 'Esta lección cubre variables y tipos de datos en programación.');
go
INSERT INTO Lecciones (NroLeccion, Nombre, Descripcion)
VALUES (2, 'La Edad Media', 'Esta lección cubre los eventos importantes de la Edad Media.');
go
-- Insertar lecciones por estudiantes
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (2, 1, 1);
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (2, 2, 0);
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (3, 3, 0);
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (4, 4, 0);
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (3, 5, 0);
INSERT INTO LeccionesXEstudiantes (IDEstudiante, IDLeccion, Completado) VALUES (4, 6, 0);
go
-- Insertar materiales por lecciones
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (1, 1);
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (2, 2);
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (3, 3);
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (4, 4);
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (5, 5);
INSERT INTO MaterialesXLecciones (IDMaterial, IDLeccion) VALUES (6, 6);
go
-- Insertar unidades
INSERT INTO Unidades (NroUnidad, Nombre, Descripcion)
VALUES (1, 'Unidad 1 de Algebra', 'Descripción de la unidad 1 de algebra.');
go
INSERT INTO Unidades (NroUnidad, Nombre, Descripcion)
VALUES (1, 'Unidad 1 de Fisica', 'Descripción de la unidad 1 de fisica.');
go
INSERT INTO Unidades (NroUnidad, Nombre, Descripcion)
VALUES (1, 'Unidad 1 de Programación', 'Descripción de la unidad 1 de programación.');
go
INSERT INTO Unidades (NroUnidad, Nombre, Descripcion)
VALUES (1, 'Unidad 1 de Historia', 'Descripción de la unidad 1 de historia.');
go
-- Insertar lecciones por unidades
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (1, 1);
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (2, 2);
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (3, 3);
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (4, 4);
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (3, 5);
INSERT INTO LeccionesXUnidades (IDUnidad, IDLeccion) VALUES (4, 6);
go
-- Insertar unidades por curso
INSERT INTO UnidadesXCurso (IDUnidad, IDCurso) VALUES (1, 1);
INSERT INTO UnidadesXCurso (IDUnidad, IDCurso) VALUES (2, 2);
INSERT INTO UnidadesXCurso (IDUnidad, IDCurso) VALUES (3, 3);
INSERT INTO UnidadesXCurso (IDUnidad, IDCurso) VALUES (4, 4);
go