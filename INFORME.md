# Informe – Gestión de catálogo de libros con arreglos (C#)

**Estudiante:** *ROSA OSMILDA BATISTA MARTINEZ*
**Matrícula:** *2024-2426*
**Asignatura:** *Estructuras de datos*
**Enlace de GitHub:**    *https://github.com/RosaOsmilda/Biblioteca-Consola*

## 1\. Descripción

Aplicación de consola en C# que administra un catálogo de libros con un único arreglo `Book\[]` de capacidad fija (20). Los libros ocupan siempre las posiciones `0 .. Count-1`; el contador `\_count` indica la siguiente posición libre. Se cargan 12 libros al iniciar.

## 2\. Captura de la aplicación

*Inserte aquí la captura de pantalla del menú, el listado y las operaciones funcionando.*

## 3\. Análisis de complejidad Big O

|Operación|Big O|Explicación|
|-|-|-|
|Agregar|**O(1)**|Se escribe directo en `\_books\[\_count]`. No se recorre nada; el Id es autoincremental, así que no hay que validar duplicados.|
|Listar|**O(n)**|Se visitan los n libros registrados una vez.|
|Buscar por ID|**O(n)**|Búsqueda lineal. Mejor caso O(1) (posición 0); peor caso O(n) (último o inexistente).|
|Actualizar|**O(n)**|Buscar es O(n); modificar el elemento encontrado es O(1). Total: O(n) + O(1) = O(n).|
|Eliminar|**O(n)**|Buscar es O(n) y desplazar a la izquierda los elementos posteriores es O(n) en el peor caso (eliminar el primero). Total: O(n) + O(n) = O(n).|

## 4\. Limitación del arreglo

**¿Qué pasa con el libro número 21?**
El arreglo tiene 20 posiciones (índices 0–19). Al intentar guardar en el índice 20 se produciría una `IndexOutOfRangeException`. El programa lo evita comprobando `IsFull` antes de insertar, y muestra el mensaje: *"No se puede agregar: el catálogo alcanzó su capacidad máxima"*. El libro no se registra.

**¿Por qué el tamaño fijo es una limitación?**
Un arreglo reserva un bloque contiguo de memoria de tamaño definido al crearlo y ese tamaño no puede cambiar. Si se subestima la capacidad, el sistema no puede seguir creciendo; si se sobreestima, se desperdicia memoria. Además, el desplazamiento al eliminar hace que esa operación sea costosa a medida que crece el catálogo.

**Solución conceptual**
Cuando el arreglo se llene, crear un arreglo nuevo con mayor capacidad (por ejemplo, el doble), copiar los elementos del arreglo anterior al nuevo y continuar usando el nuevo. Así funciona `List<T>` internamente: el redimensionamiento cuesta O(n) cuando ocurre, pero como sucede pocas veces, agregar tiene un costo **amortizado de O(1)**. Otra alternativa es usar una estructura dinámica como una lista enlazada, que crece nodo a nodo sin copiar ni tener capacidad máxima.

## 5\. Conclusiones

Con esta práctica aprendí cómo funciona un arreglo por dentro. Los elementos se guardan uno al lado del otro y se acceden por su índice, por eso agregar un libro al final es rápido: escribe directamente en la siguiente posición libre, con complejidad O(1).



También comprobé que listar, buscar, actualizar y eliminar son O(n), porque hay que recorrer el arreglo. Eliminar fue lo que más se notó: al borrar el libro con ID 1, todos los demás tuvieron que moverse una posición a la izquierda para no dejar huecos, y el listado pasó a empezar en el ID 2.



Al llegar a 20/20, el programa rechazó el libro 21 con un mensaje de capacidad máxima. Entendí que el tamaño fijo es la gran limitación del arreglo: si es pequeño, el catálogo no puede crecer, y si es muy grande, se desperdicia memoria. Una solución sería crear un arreglo del doble de tamaño cuando se llene y copiar los datos, que es lo que hace List<T>, o usar una lista enlazada.



