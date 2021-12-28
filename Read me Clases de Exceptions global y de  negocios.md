#Clases de Exceptions global y de  negocios
1 Agreagamos una nueva clase  PostRepositorio que contendra un metodo adicional para poder consultar los post por id del usuario
2 Modificación de la interfaz IUnitOfWork donde definimos la propiedad IPostRepository ya que extenderá métodos diferentes al crud
3 BaseRepository<T> modificamos los métodos y definimos nuestra propiedad Dbset con privacidad tipo protected para que pueda ser accedida por las clases que la heredan.
le damos todo el control para las operaciones a nuesto unitOfWork eliminando los saveChanges que hacia el context.
5 Le quitamos la asincronía a a algunos métodos en IRepository
6 Creamos la nueva clase de GlobalException para controlar las excepciones que nos arrojen lo servicios que desarrollemos en nuestro negocio (EntidadService)
7 Desarrollamos la lógica para que un usuario solo pueda crear un post si tiene menos de 10 publicaciones solo publicará una cada semana. con su implementación de Errores globales
8 Creamos una clase de BusinessExection para clasificar las exceptiones ..
9 Matriculamos nuestra excepcionesGlobal en el startup   
services.AddControllers(options=>
 {
   options.Filters.Add<GlobalExceptionFilter>();
 });
			
'

