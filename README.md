# SocialMedia
Ejercicio sumunlacion red Social, se incluyen las entidades de post, user y comment::
aqui trataremos de simular como un usuario puede publicar un post y otros usuarios podran comentar estos post
aplicaremos los diferentes verbos con las api.
Se tendran varias versiones.
asp.Net Core 5.0

# comit Implementación de Patron UnitOfWork y Creación de Repositorio Generico
1 Se Crea una clase baseEntity de tipo abstracta ya que esta no se va ha implementar solo heredar. 
2 Heredamos la clase a todas las clases que queremos integrar en nuestro repositorio genérico.
3 Actualizar los modelos según  las propiedades que esten en la clase abstracta.
4 Creamos la interfaz de IRespositorioGenerico le heredamos la clase baseEnity -Ejemplo  public interface IRepository<T> where T : BaseEntity
5 Creamos una nueva interfaz  interface IUnitOfWork : IDisposable para definir nuestra area de trabajo, esta va hacer una propiedad que usaremos para accerder a nuestro         repositorio. 
5 Implementamos nuestra ineterfaz - class UnitOfWork : IUnitOfWork 
7 Inyectamos nuestro contex y agregamos nuestras propiedades de Irepository<entidades>, creamos el contructor
8 Retornar si el repositorio y si es nulo creamos una nueva instacia de BaseRepostory<enitdad>() Ejemplo - new BaseRepository<Post>(_context)
6 Registramos nuestras interfaces en el sturtap.
7 Hacemos la inyeción de dependencia en nuestro Servicio y actualizamos nuestros métodos en el servicio en servicio y en  interfaz del Servicio de acuerdo a los implementados en nuestro UnitOfWork.
  
  ..Estre actualizando de acuerdo a lo que aprenda..
 
