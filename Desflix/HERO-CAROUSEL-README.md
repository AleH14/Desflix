# Hero Carousel - Desflix

## 📽️ Descripción

Se ha agregado un **Hero Carrusel profesional y moderno** a la página de inicio del proyecto Desflix. Este componente muestra las películas más destacadas de la plataforma con un diseño inspirado en Netflix, que incluye transiciones suaves, animaciones dinámicas y controles intuitivos.

## ✨ Características Principales

### 1. **Carrusel Automático**
- Auto-rotación cada 6 segundos
- Transiciones suaves entre películas (fade in/out)
- Pausas automáticas al hacer hover
- Reanuda al retirar el mouse

### 2. **Interfaz Interactiva**
- Botones de navegación (anterior/siguiente)
- Indicadores circulares para ir a una película específica
- Navegación por teclado (flechas izquierda/derecha)
- Soporte para gestos táctiles (swipe)

### 3. **Diseño Visual Profesional**
- Fondo oscuro con overlay sofisticado
- Imagen destacada con efecto cinemático
- Información detallada de la película:
  - Título principal
  - Director
  - Año de lanzamiento
  - Calificación en estrellas
  - Sinopsis truncada

### 4. **Botones de Acción**
- **"Ver Detalles"**: Redirige a la página de detalles de la película
- **"Explorar Catálogo"**: Acceso directo al listado de películas

### 5. **Grid de Películas Destacadas**
Bajo el carrusel, se muestra un grid responsivo con:
- Tarjetas de películas con imagen
- Información resumida (título, director, calificación)
- Efecto hover con elevación 3D
- Botones "Ver más" para acceder a detalles

## 🎨 Estilos Personalizados

Se han creado tres archivos CSS principales:

### `hero-carousel.css`
- Estilos del carrusel hero
- Animaciones de entrada (slideIn)
- Controles y indicadores
- Responsive design completo

### `movie-cards.css`
- Estilos mejorados para las tarjetas de películas
- Efectos hover 3D
- Estados responsivos
- Animaciones de pulso

### `_LayoutNetflix.cshtml`
- Integración de todos los estilos
- Scripts necesarios para la funcionalidad

## 🔧 Funcionalidad JavaScript

El archivo `hero-carousel.js` proporciona:

### Métodos Principales
- `showSlide(index)`: Muestra una película específica
- `nextSlide()`: Avanza al siguiente slide
- `prevSlide()`: Retrocede al slide anterior
- `startAutoplay()`: Inicia la auto-rotación
- `resetAutoplay()`: Reinicia el temporizador de rotación

### Eventos Soportados
- **Click**: Botones anterior/siguiente e indicadores
- **Teclado**: Flechas izquierda/derecha
- **Touch**: Gestos swipe en dispositivos móviles
- **Mouse**: Pausa/resume al hover

## 📊 Modelo de Datos

El controlador `HomeController` obtiene las películas de la siguiente manera:

```csharp
// Obtiene películas y las ordena por calificación descendente
var peliculasDestacadas = peliculas
	.OrderByDescending(p => p.Calificacion ?? 0)
	.Take(6)
	.ToList();
```

### Propiedades Mostradas
- **Titulo**: Nombre de la película
- **UrlImagen**: URL de la imagen/póster
- **Director**: Nombre del director
- **FechaLanzamiento**: Año de lanzamiento
- **Calificacion**: Puntuación en estrellas (0-10)
- **Sinopsis**: Descripción truncada a 150 caracteres

## 📱 Responsive Design

### Puntos de Quiebre
- **Desktop (> 1024px)**: Vista completa con imagen lateral
- **Tablet (768px - 1024px)**: Ajustes en tamaño de fuente y espaciado
- **Móvil (< 768px)**: Stack vertical, oculta imagen lateral
- **Móvil pequeño (< 576px)**: Optimizado para pantallas muy pequeñas

## 🚀 Optimizaciones

1. **Lazy Loading**: Las imágenes se cargan bajo demanda
2. **Hardware Acceleration**: Uso de `transform` para animaciones fluidas
3. **CSS Grid/Flexbox**: Layouts modernos y eficientes
4. **Transiciones Suaves**: `transition` en lugar de animaciones costosas
5. **Touch Optimization**: Gestos nativos para móviles

## 🔐 Validaciones

- Verifica si existen películas antes de mostrar el carrusel
- Manejo de URLs vacías con placeholders
- Sincronización entre slides e indicadores
- Protección contra índices fuera de rango

## 📝 Cómo Usar

### En Controlador
```csharp
// El HomeController obtiene automáticamente las películas destacadas
public async Task<IActionResult> Index()
{
	var peliculas = await _peliculaService.GetAllPeliculasAsync();
	var peliculasDestacadas = peliculas
		.OrderByDescending(p => p.Calificacion ?? 0)
		.Take(6)
		.ToList();
	return View(peliculasDestacadas);
}
```

### En Vista
```html
@model List<PeliculaResponseDto>

<!-- El carrusel se renderiza automáticamente -->
<!-- Itera sobre el modelo para crear slides -->
```

## 🎯 Patrones de Diseño Aplicados

1. **MVC Pattern**: Separación clara entre Controller, View y Services
2. **Service Layer**: PeliculaService maneja la lógica de negocio
3. **DTO Pattern**: Uso de PeliculaResponseDto para transferencia de datos
4. **Responsive Design**: Mobile-first approach
5. **CSS Modules**: Archivos CSS separados por componente

## 🌟 Características Futuras (Opcionales)

- [ ] Filtrado por género
- [ ] Búsqueda de películas en el carrusel
- [ ] Rating dinámico basado en usuario
- [ ] Carruseles múltiples por género
- [ ] Persistencia de película favorita
- [ ] Integración con Google Analytics

## 📚 Archivos Modificados/Creados

### Creados
- `wwwroot/css/hero-carousel.css` - Estilos del carrusel
- `wwwroot/css/movie-cards.css` - Estilos de tarjetas
- `wwwroot/js/hero-carousel.js` - Funcionalidad del carrusel

### Modificados
- `Controllers/HomeController.cs` - Inyección de PeliculaService
- `Views/Home/Index.cshtml` - Nueva estructura del Hero
- `Views/Shared/_LayoutNetflix.cshtml` - Inclusión de estilos y scripts

## 🤝 Compatibilidad

- ✅ Chrome/Edge 88+
- ✅ Firefox 86+
- ✅ Safari 14+
- ✅ Navegadores móviles modernos
- ✅ IE11 (con fallbacks)

## 📞 Notas de Desarrollo

- Las animaciones utilizan `transform` y `opacity` para máximo rendimiento
- Todos los colores siguen la paleta de Desflix (Netflix-inspired)
- Los breakpoints responsivos están definidos en `hero-carousel.css`
- El JavaScript es vanilla (sin dependencias externas)

---

**Última actualización**: 2026
**Estado**: Producción
