use elecciones
go
select * from oed

insert into oed(id_oed, direccion)
values(1, 'Andahuaylillas')
go

insert into oed(id_oed, direccion)
values(2, 'Urcos')
go

insert into centro_votacion(id_centro_votacion,
direccion, id_distrito, id_provincia, id_departamento, id_oed)
values(1, 'Rosales 212', '15', '15', '15', 1)
go

insert into mesa(id_mesa, id_centro_votacion)
values('1501', 1)
go

insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(1, 'Jorge', 'Guevara', 'jorge@hotmail.com', 4, '1000001')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(2, 'Marco', 'Gutierrez', 'marco@hotmail.com', 4, '1000002')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(3, 'Julia', 'Lozano', 'hello@hello.com', 4, '1000003')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(4, 'Martina', 'Bustamante', 'hello@hello.com', 4, '1000004')
go

insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(5, 'Jorge', 'Becerril', 'hello@hello.com', 4, '1000005')
go

insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(6, 'Luz', 'Portocarrero', 'hello@hello.com', 4, '1000006')
go
 

insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(7, 'Votos en blanco', '', '', 4, '')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(8, 'Votos nulos', '', '', 4, '')
go



insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(9, 'Votos impugnados', '', '', 4, '')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(10, 'Nestor', 'Fernandez', '', 1, '10334482')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(11, 'Julio', 'Fernandez', '', 1, '10334487')
go

 
insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(12, 'Fernando', 'Liñan', 'fernando@gmail.com', null, '20334487')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(13, 'Julio', 'Enrique', 'enrique@gmail.com', null, '21334487')
go


insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(14, 'Virgilio', 'Justo', 'jvirgilio@gmail.com', null, '21334587')
go

insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(15, 'Rodolfo', 'Jimenez', 'rjimenez@gmail.com', 2, '21334597')
go



insert into militante (id_militante, 
nombres, apellidos, correo, id_cargo, nro_doc_identidad)
values(16, 'Omar', 'Salcedo', 'osalcito@gmail.com', 3, '21134597')
go 
 
insert into oed_militante(id_oed, id_militante)
values(1, 10)
go
insert into oed_militante(id_oed, id_militante)
values(2, 11)
go