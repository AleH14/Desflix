# 🎬 Guía Rápida - Hero Carousel Desflix

## 🚀 Inicio Rápido

El Hero Carousel está completamente integrado y funcionará automáticamente cuando accedas a la página de inicio.

### ¿Qué ver?

1. **En el navegador**, ve a: `https://localhost:your-port/`
2. Deberías ver:
   - Un carrusel grande en la parte superior con una película destacada
   - Información de la película (título, director, año, calificación)
   - Botones de acción: "Ver Detalles" y "Explorar Catálogo"
   - Controles: Flechas, puntos indicadores
   - Grid de películas debajo del carrusel

---

## 🎮 Controles del Carrusel

### Con Mouse
| Acción | Resultado |
|--------|-----------|
| Clic en ❯ | Siguiente película |
| Clic en ❮ | Película anterior |
| Clic en punto | Ir a película específica |
| Hover sobre carrusel | Pausa auto-rotación |
| Alejar mouse | Reanuda auto-rotación |

### Con Teclado
| Tecla | Acción |
|-------|--------|
| **→** (Flecha derecha) | Siguiente película |
| **←** (Flecha izquierda) | Película anterior |

### En Móvil (Touch)
| Gesto | Acción |
|-------|--------|
| **Swipe izquierda** | Siguiente película |
| **Swipe derecha** | Película anterior |

---

## 📊 Información Mostrada

### En el Carrusel
- **🎬 Título**: Nombre de la película
- **👤 Director**: Nombre del director
- **📅 Año**: Año de lanzamiento
- **⭐ Calificación**: Puntuación 0-10
- **📜 Sinopsis**: Descripción truncada (150 caracteres)

### En el Grid
- Imagen/Póster
- Título
- Director
- Calificación

---

## 🔄 Funcionamiento Automático

### Timeline del Carrusel
```
0s - 6s    → Película 1 visible
6s - 12s   → Película 2 visible
12s - 18s  → Película 3 visible
... y así sucesivamente
```

**Nota**: Si haces hover, se pausa automáticamente. Cuando retiras el mouse, continúa.

---

## 🎯 Criterio de Selección

Las películas se muestran en este orden:
1. Ordenadas por **calificación más alta**
2. Se muestran **máximo 6 películas**
3. Si no hay calificación, se toman las primeras 6

---

## 📱 Comportamiento Responsivo

### En Desktop (> 1024px)
- Carrusel de altura 600px
- Imagen grande en lado derecho
- Información y botones en lado izquierdo
- Grid de películas debajo

### En Tablet (768px - 1024px)
- Carrusel de altura 500px
- Tamaños de fuente reducidos
- Imagen aún visible pero más pequeña

### En Móvil (< 768px)
- Carrusel de altura 400px
- Imagen se oculta (enfoque en texto)
- Grid de películas se adapta a 2 columnas
- Controles más pequeños

### En Móvil Pequeño (< 576px)
- Carrusel de altura 350px
- Texto aún más compacto
- Grid de 2 columnas
- Botones a tamaño completo

---

## 🎨 Colores y Estilos

| Elemento | Color |
|----------|-------|
| Fondo principal | Negro (#000000) |
| Texto | Blanco (#ffffff) |
| Acentos | Rojo Netflix (#E50914) |
| Bordes | Gris oscuro (#333333) |

---

## ⚙️ Configuración

### Auto-rotación
- **Intervalo**: 6 segundos
- **Ubicación**: `wwwroot/js/hero-carousel.js` línea ~53
- Para cambiar:
  ```javascript
  autoplayInterval = setInterval(nextSlide, 6000); // Cambiar 6000 a otro valor
  ```

### Películas Mostradas
- **Cantidad**: 6 películas
- **Ubicación**: `Controllers/HomeController.cs` línea ~26
- Para cambiar:
  ```csharp
  .Take(6)  // Cambiar 6 al número deseado
  ```

---

## 🐛 Solución de Problemas

### El carrusel no aparece
- Verifica que haya películas en la base de datos
- Comprueba la consola del navegador (F12) para errores

### Las imágenes no se cargan
- Verifica que `UrlImagen` esté relleno en las películas
- Usa URLs válidas (http/https)

### Los controles no funcionan
- Recarga la página (F5)
- Verifica en la consola si hay errores JavaScript
- Asegúrate de que `hero-carousel.js` se cargó

### El carrusel se ve pequeño
- Verifica la resolución de tu pantalla
- En móvil, verifica que no estés en zoom

---

## 📁 Archivos Relacionados

```
Desflix/
├── Controllers/
│   └── HomeController.cs          ← Lógica del carrusel
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           ← Estructura del carrusel
│   └── Shared/
│       └── _LayoutNetflix.cshtml  ← Referencias CSS/JS
├── wwwroot/
│   ├── css/
│   │   ├── hero-carousel.css      ← Estilos del carrusel
│   │   └── movie-cards.css        ← Estilos de tarjetas
│   └── js/
│       └── hero-carousel.js       ← Funcionalidad JavaScript
└── HERO-CAROUSEL-README.md        ← Documentación completa
```

---

## 🔍 Inspección y Debugging

### Ver Estructura HTML
Presiona **F12** → **Inspector** → Expande `<section class="hero-carousel-section">`

### Ver Estilos Aplicados
Presiona **F12** → **Estilos** → Busca `hero-carousel` o `movie-card`

### Ver Logs JavaScript
Presiona **F12** → **Consola** → Interactúa con el carrusel (verás eventos)

---

## 📞 Contacto y Soporte

Para más detalles técnicos, revisa:
- `HERO-CAROUSEL-README.md` - Documentación completa
- `CAMBIOS-HERO-CAROUSEL.md` - Resumen de cambios

---

**Última actualización**: 2026  
**Versión**: 1.0  
**Estado**: Producción ✅
