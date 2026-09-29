# Informe – Gestión de catálogo de libros con arreglos (C#)

**Estudiante:** _Nombre completo_
**Matrícula:** _XXXX_
**Asignatura:** _Estructuras de datos_
**Enlace de GitHub:** _https://github.com/usuario/BibliotecaConsola_

## 1. Descripción

Aplicación de consola en C# que administra un catálogo de libros con un único arreglo `Book[]` de capacidad fija (20). Los libros ocupan siempre las posiciones `0 .. Count-1`; el contador `_count` indica la siguiente posición libre. Se cargan 12 libros al iniciar.

## 2. Captura de la aplicación

_Inserte aquí la captura de pantalla del menú, el listado y las operaciones funcionando._

## 3. Análisis de complejidad Big O

| Operación | Big O | Explicación |
|---|---|---|
| Agregar | **O(1)** | Se escribe directo en `_books[_count]`. No se recorre nada; el Id es autoincremental, así que no hay que validar duplicados. |
| Listar | **O(n)** | Se visitan los n libros registrados una vez. |
| Buscar por ID | **O(n)** | Búsqueda lineal. Mejor caso O(1) (posición 0); peor caso O(n) (último o inexistente). |
| Actualizar | **O(n)** | Buscar es O(n); modificar el elemento encontrado es O(1). Total: O(n) + O(1) = O(n). |
| Eliminar | **O(n)** | Buscar es O(n) y desplazar a la izquierda los elementos posteriores es O(n) en el peor caso (eliminar el primero). Total: O(n) + O(n) = O(n). |

## 4. Limitación del arreglo

**¿Qué pasa con el libro número 21?**
El arreglo tiene 20 posiciones (índices 0–19). Al intentar guardar en el índice 20 se produciría una `IndexOutOfRangeException`. El programa lo evita comprobando `IsFull` antes de insertar, y muestra el mensaje: *"No se puede agregar: el catálogo alcanzó su capacidad máxima"*. El libro no se registra.

**¿Por qué el tamaño fijo es una limitación?**
Un arreglo reserva un bloque contiguo de memoria de tamaño definido al crearlo y ese tamaño no puede cambiar. Si se subestima la capacidad, el sistema no puede seguir creciendo; si se sobreestima, se desperdicia memoria. Además, el desplazamiento al eliminar hace que esa operación sea costosa a medida que crece el catálogo.

**Solución conceptual**
Cuando el arreglo se llene, crear un arreglo nuevo con mayor capacidad (por ejemplo, el doble), copiar los elementos del arreglo anterior al nuevo y continuar usando el nuevo. Así funciona `List<T>` internamente: el redimensionamiento cuesta O(n) cuando ocurre, pero como sucede pocas veces, agregar tiene un costo **amortizado de O(1)**. Otra alternativa es usar una estructura dinámica como una lista enlazada, que crece nodo a nodo sin copiar ni tener capacidad máxima.

## 5. Conclusiones

_Escriba aquí sus conclusiones personales sobre lo aprendido._
