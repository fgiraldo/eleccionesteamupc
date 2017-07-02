
create database elecciones
go
use elecciones
go
CREATE TABLE militante (
 id_militante INT NOT NULL,
 nombres VARCHAR(100) NOT NULL,
 apellidos VARCHAR(200) NOT NULL,
 correo VARCHAR(200),
 id_cargo INT   NULL,
 nro_doc_identidad CHAR(8)
);

ALTER TABLE militante ADD CONSTRAINT PK_militante PRIMARY KEY (id_militante);


CREATE TABLE oed (
 id_oed INT NOT NULL,
 direccion VARCHAR(200)
);

ALTER TABLE oed ADD CONSTRAINT PK_oed PRIMARY KEY (id_oed);


CREATE TABLE oed_militante (
 id_oed INT NOT NULL,
 id_militante INT NOT NULL
);

ALTER TABLE oed_militante ADD CONSTRAINT PK_oed_militante PRIMARY KEY (id_oed,id_militante);


CREATE TABLE centro_votacion (
 id_centro_votacion INT NOT NULL,
 direccion VARCHAR(200) NOT NULL,
 id_distrito CHAR(2) NOT NULL,
 id_provincia CHAR(2) NOT NULL,
 id_departamento CHAR(10) NOT NULL,
 id_oed INT NOT NULL
);

ALTER TABLE centro_votacion ADD CONSTRAINT PK_centro_votacion PRIMARY KEY (id_centro_votacion);


CREATE TABLE mesa (
 id_mesa VARCHAR(10) NOT NULL,
 id_centro_votacion INT NOT NULL
);

ALTER TABLE mesa ADD CONSTRAINT PK_mesa PRIMARY KEY (id_mesa);


CREATE TABLE acta (
 id_mesa VARCHAR(10) NOT NULL,
 id_militante_presidente INT NOT NULL,
 id_militante_secretario INT NOT NULL,
 id_militante_tercer_miembro INT,
 fecha_registro DATETIME NOT NULL,
 id_militante_registro INT NOT NULL,
 estado INT NOT NULL,
 nombre_archivo VARCHAR(200) NOT NULL,
 observaciones VARCHAR(1000)
);

ALTER TABLE acta ADD CONSTRAINT PK_acta PRIMARY KEY (id_mesa);


CREATE TABLE detalle_acta (
 id_mesa VARCHAR(10) NOT NULL,
 id_militante INT NOT NULL,
 cant_votos INT NOT NULL
);

ALTER TABLE detalle_acta ADD CONSTRAINT PK_detalle_acta PRIMARY KEY (id_mesa,id_militante);


CREATE TABLE solicitud_anulacion_acta (
 id_solicitud INT NOT NULL,
 fecha_registro DATETIME NOT NULL,
 estado INT NOT NULL,
 motivo VARCHAR(1000) NOT NULL,
 id_mesa VARCHAR(10) NOT NULL,
 id_militante_registro INT NOT NULL,
 id_militante_atencion INT
);

ALTER TABLE solicitud_anulacion_acta ADD CONSTRAINT PK_solicitud_anulacion_acta PRIMARY KEY (id_solicitud);


CREATE TABLE evidencia (
 id_evidencia INT NOT NULL,
 id_mesa VARCHAR(10),
 id_solicitud INT
);

ALTER TABLE evidencia ADD CONSTRAINT PK_evidencia PRIMARY KEY (id_evidencia);


ALTER TABLE oed_militante ADD CONSTRAINT FK_oed_militante_0 FOREIGN KEY (id_oed) REFERENCES oed (id_oed);
ALTER TABLE oed_militante ADD CONSTRAINT FK_oed_militante_1 FOREIGN KEY (id_militante) REFERENCES militante (id_militante);


ALTER TABLE centro_votacion ADD CONSTRAINT FK_centro_votacion_0 FOREIGN KEY (id_oed) REFERENCES oed (id_oed);


ALTER TABLE mesa ADD CONSTRAINT FK_mesa_0 FOREIGN KEY (id_centro_votacion) REFERENCES centro_votacion (id_centro_votacion);


ALTER TABLE acta ADD CONSTRAINT FK_acta_0 FOREIGN KEY (id_mesa) REFERENCES mesa (id_mesa);
ALTER TABLE acta ADD CONSTRAINT FK_acta_1 FOREIGN KEY (id_militante_presidente) REFERENCES militante (id_militante);
ALTER TABLE acta ADD CONSTRAINT FK_acta_2 FOREIGN KEY (id_militante_secretario) REFERENCES militante (id_militante);
ALTER TABLE acta ADD CONSTRAINT FK_acta_3 FOREIGN KEY (id_militante_tercer_miembro) REFERENCES militante (id_militante);
ALTER TABLE acta ADD CONSTRAINT FK_acta_4 FOREIGN KEY (id_militante_registro) REFERENCES militante (id_militante);


ALTER TABLE detalle_acta ADD CONSTRAINT FK_detalle_acta_0 FOREIGN KEY (id_mesa) REFERENCES acta (id_mesa);
ALTER TABLE detalle_acta ADD CONSTRAINT FK_detalle_acta_1 FOREIGN KEY (id_militante) REFERENCES militante (id_militante);


ALTER TABLE solicitud_anulacion_acta ADD CONSTRAINT FK_solicitud_anulacion_acta_0 FOREIGN KEY (id_mesa) REFERENCES acta (id_mesa);
ALTER TABLE solicitud_anulacion_acta ADD CONSTRAINT FK_solicitud_anulacion_acta_1 FOREIGN KEY (id_militante_registro) REFERENCES militante (id_militante);
ALTER TABLE solicitud_anulacion_acta ADD CONSTRAINT FK_solicitud_anulacion_acta_2 FOREIGN KEY (id_militante_atencion) REFERENCES militante (id_militante);


ALTER TABLE evidencia ADD CONSTRAINT FK_evidencia_0 FOREIGN KEY (id_mesa) REFERENCES acta (id_mesa);
ALTER TABLE evidencia ADD CONSTRAINT FK_evidencia_1 FOREIGN KEY (id_solicitud) REFERENCES solicitud_anulacion_acta (id_solicitud);


