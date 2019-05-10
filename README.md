# LexiBalance

## Avances en el proyecto
Siguiendo el [Diagrama de Gantt](https://ibb.co/X3kwp6w).
- [x] Creación de la web
- [x] Creacion de los MVC
- [x] Creación de la base de datos
- [x] Implementación de la base de datos (posibles cambios para mejora del sistema en la fase de testeo)
- [x] Opciones de añadir, editar y borrar en la base de datos (posibles cambios para mejora del sistema en la fase de testeo)
- [x] Creación de usuario gerente y trabajador (posibles cambios para mejora del sistema en la fase de testeo)
- [ ] Rediseño web
- [ ] Testear
- [ ] Entrega con el informe final y vídeo explicativo

## Semana a semana

###### Primera semana (01/04/2019 - 07/04/2019)
  Creación de los MVC, formularios y clases (productos, personal, clientes) a través de Razor Pages.
  
###### Segunda semana (08/04/2019 - 14/04/2019) 
  Creación de la BD e implementación. Creación de clase ventas, al insertar una nueva venta con distintos SELECT se elige el producto, trabajador y cliente ya existentes en la BD. La cantidad del producto vendido disminuye en la cantidad total del producto.
  
###### Tercera semana (15/04/2019 - 21/04/2019)
> La BD va a ser gestionada de forma más compleja con distintas cantidades que se pueden vender de un mismo producto.

> Se va a añadir un usuario "gerente" que puede insertar ventas, clientes, productos y trabajadores; y un usuario "trabajador" que puede insertar ventas y clientes.

  Creación de la gestión de usuarios. La BD ahora está dentro del proyecto para mejor acceso. Implementando los usuarios.
  Trabajador: puede acceder a ventas (crear, editar, borrar, ver detalles), clientes (crear, editar, borrar, ver detalles) y productos (ver detalles).
  Gerente: todo
  Al añadir una venta se introduce la fecha automáticamente, no hay que indicarla, mejor usabilidad. Los campos tienen límites. Ya se pueden poner decimales a los productos. Si la BD está vacía no falla. Cambios visuales (trabajador no ve la pestaña de personal, ni ve que se puedan editar, borrar ni crear productos)

###### Cuarta semana (22/04/2019 - 28/04/2019)
  Al añadir una venta y especificar la cantidad de producto, si la cantidad es mayor a la cantidad existente del producto no se sale de la página (la venta se guarda, se compara la cantidad del producto y la introducida, si es mayor la venta se borra). Al poner una cantidad menor se vuelve al índice.
  Mejora en los campos, poniendo número máximo de longitud y que sean obligatorios.
  Al borrar una venta, la cantidad borrada vendida se suma a la cantidad del producto.
  Al editar una venta se modifican las cantidades, se le suma la cantidad actual a la cantidad del producto y se resta la nueva. Si al final se cancela la edición se colocan los datos iniciales. No se puede editar el producto ni la fecha.
  Mejora de los colores de los productos. Hay más colores; en crear y editar se seleccionan del ENUM de Producto, no se ponen a mano.
  Mini-limpieza de código.

###### Quinta semana (29/04/2019 - 05/05/2019)
  Cambiar el diseño web. El menú ahora está en la izquierda, en vez de estar arriba; el contenido de las páginas se sitúa a su derecha.
  Imágenes añadidas en el menú y en el index. Cambio de fuente. Si el texto es muy largo la tabla crece a lo alto, no a lo ancho. Imágenes para el título de cada página.
  
###### Sexta semana (06/05/2019 - /05/2019)
  Cambios en el diseño del menú. 
  Al insertar o editar un cliente se comprueba que no se repita el número de télefono. Añadida una barra de búsqueda para el nombre de los productos. Añadida barra de búsqueda para clientes por número de teléfono y para personal por DNI. Al insertar o editar un trabajador se comprueba que no se repita el DNI. En los inputs de CP, teléfono y DNI tienen que ser 5 y 9 caracteres exactos. Al insertar una venta se muestra el número de unidades disponibles de cada producto en el select. Arreglada la gestión de la BD respecto a ventas, las cantidades se gestionan con el ID del producto, no con el nombre (así se pueden repetir nombres). Al insertar o editar una venta si el producto es borrado durante este proceso no se inserta y se avisa del error.
  
  ###### Realizando en esta semana
  Ser accesible a través de internet.
  
