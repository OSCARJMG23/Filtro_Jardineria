# Filtro_Jardineria

## Consultas Requridas
*  Devuelve el listado de clientes indicando el nombre del cliente y cuántos 
  pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no 
  han realizado ningún pedido.
  * Ruta: http://localhost:5000/api/Cliente/clienteytotalpedidos
  * Codigo:
    
    ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/ebe971fd-77d4-4abf-a4bc-27205818c8e4)
    
  * Explicacion: En este metodo se accede a los clientes y posteriormente se selecciona la informacion necesaria a retornar con la creacion de un nuevo objeto,
    dentro de la informacion seleccionada se usa e.Pedidos.Count() para acceder a la coleccion de pedidos de cada cliente y Contar cuantos tiene cada uno.

*  Devuelve un listado con el código de pedido, código de cliente, fecha 
    esperada y fecha de entrega de los pedidos que no han sido entregados a 
    tiempo
   * Ruta: http://localhost:5000/api/Pedido/pedidosnoentregadosatiempo
   * Codigo:
     
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/8432b93d-3a17-40dd-b32d-008e3ff2792c)
 

   * Explicacion: En este metodo se empieza acciendo a los pedidos, despues se le pone la condicion de que solo obtenga los pedidos donde la decha de entrega se mayor a la fecha esperada,
     despues se procede a seleccionar la informacion necesaria a retornar en el nuevo objeto.


  *   Devuelve un listado de los productos que nunca han aparecido en un 
      pedido. El resultado debe mostrar el nombre, la descripción y la imagen del 
      producto.
      * Ruta: http://localhost:5000/api/Producto/productosnuncapedidos
      * Codigo:
        
       ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/b3b23ecd-9876-4eff-9cf1-0a03e40da03e)


   * Explicacion: Para este metodo empiezo accediendo a los productos, despues en la codicion accedo a la coleccion de detalles pedidos para que con el .Count(), me seleccione los que
     devuelvan un resultado de 0, despues se procede a seleccionar la informacion necesaria para retornarla en un nuevo objeto.


  *   Devuelve las oficinas donde no trabajan ninguno de los empleados que 
      hayan sido los representantes de ventas de algún cliente que haya realizado 
      la compra de algún producto de la gama Frutales
      * Ruta: http://localhost:5000/api/Oficina/oficinaNotrabajaempleadofrutales
      * Codigo:
        
      ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/30854f03-50f0-4889-80f8-27f606900320)



   * Explicacion: Para este metodo se accede a la entidad de Oficinas y se filtra aquellas donde no exista ningún empleado que cumpla con la condición especificada. La condición incluye la verificación de clientes asociados a esos empleados, pedidos realizados       por esos clientes, y finalmente, productos de la gama "Frutales" presentes en los detalles de esos pedidos.
     Usé !e.Empleados.Any(...) para asegurar que solo se seleccionen las oficinas donde no haya empleados que cumplan con la condición especificada en la subconsulta.

  * Lista las ventas totales de los productos que hayan facturado más de 3000 
    euros. Se mostrará el nombre, unidades vendidas, total facturado y total 
    facturado con impuestos (21% IVA).
    * Ruta: http://localhost:5000/api/Producto/productosfacturado3000
    * Codigo:
      
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/dc72ea7a-32f8-4c7a-8a45-f83444fd60ee)


   * Explicacion: En este metodo empiezo accediendo a los productos, posteriormente procedo a seleccionar la informacion que se requiere retornar en un objeto nuevo,
     donde dentro del objeto accedo a los ddetalles pedidos para sumar la cantidad que se ha vendido, despues para calcular el total facturado usando el .Sum() lo que hago es
     sumar la cantidad por el precio unitario del producto, despues para calcular el total facturado con el iva lo que hago es sumar la cantidad X precioUnidad y X 1.21 y se le agrega la m
     para indicar que el resultado de la operacion debe ser decimal, depues de tener el objeto con la informacion a retornar pongo la condicion de que solo devuelva los que en el total facturado
     sean mayores o igual a 3000.

  * Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.
    * Ruta: http://localhost:5000/api/Empleado/empleadoSinClientes
     * Codigo:
       
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/30e842a6-8fa4-4c18-9e57-b0364a768e43)

   * Explicacion: Para este metodo se empieza accediendo a los empleados, despues en la condicion accedo a la coleccion de clientes de cada uno para usar el .Count para obtener los que
     tengan un resultado de 0, es decir los que no tengan clientes asociados, despues se procede a seleccionar la informacion necesaria para retornarla en un nuevo objeto.
  
  * Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido)
   * Ruta: http://localhost:5000/api/Producto/productovendidoMasUnidades
   * Codigo:
     
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/193f18a3-74b3-4493-b99d-74f3de24c9d6)


   * Explicacion: En este metodo empiezo accediendo a los detalles pedidos, despues los agrupo con el codigo de cada producto y seguidamente los ordeno descendentemente por el resultado que devuelva la suma
     de la cantidad de cada producto, despues procedo a seleccionar la informacion necesaria a retornar en un nuevo objeto, donde para obtener el nombre del producto accedo a la llave que retorna el agrupamiento de los productos,
     donde selecciono unicamente el nombre que coincida con el codigo del producto y posteriormente para calcular la cantidad vendida lo que hago es usar el .Sum() para obtener el total de las cantidades vendidas.
    
  * Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.
   * Ruta: http://localhost:5000/api/Producto/productomasvendidoytotalunidades
   * Codigo:
     
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/03262310-61e3-4d2a-9326-ba1e96c3a010)


   * Explicacion: Para este metodo lo que hago es acceder a los productos, despues los ordeno descendentemente por la suma de la cantidad obtenida en los detalles del pedido, seguidamente se le
     indica que solo tome 20 resultados con el .Take(20), y despues lo que hago es seleccionar la informacion necesaria a retornar en un nuevo objeto.
     
  *  Devuelve el nombre de los clientes a los que no se les ha entregado a 
    tiempo un pedido.

   * Ruta: http://localhost:5000/api/Cliente/clienteentregadofueratiempo 
   * Codigo:
     
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/e39a7a11-f390-4b0e-9830-43504822ad16)


   * Explicacion: En este metodo lo que hago es acceder a los clientes, despues pongo la condicion entrando a la coleccion de los pedidos del cliente para indicar que si alguno llega a tener la fecha de entrega mayor a la
     fecha esperada, para que me retorne los que cumplen con esa condicion, despues procedo a seleccionar la informacion necesaria para retornarla en un nuevo objeto donde selecciono el nombre, y adicionalmente pongo un
     atributo pedido con el mensaje "Entregado Fuera de tiempo".

* Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente.

   * Ruta: http://localhost:5000/api/Cliente/clientesygamascomprado
   * Codigo:
     
     ![image](https://github.com/OSCARJMG23/Filtro_Jardineria/assets/133609079/9bccc2f7-327d-4e47-a5db-bcb0206a444f)


   * Explicacion: Para este metodo empiezo obteniendo la lista de todos los clientes que tengan o hayan realizado algun producto, despues lo que hago es seleccionar los datos que deseo retornar
     como lo es el nombre, y para obtener las gamas lo que hago es acceder a los pedidos de cada cliente donde uso el .SelectMany() para obtener la secuencias de gamas de cada producto para los pedidos de cada cliente,
     despues se selecciona la gama(que vendria siendo el nombre de la gama) y uso el .Distinct() para eliminar las gamas que se duplican el el listado y por ultimo se retorna el objeto con la informacion necesaria.
