<div id="PassGuardLogo" align="center">
    <br />
    <img src="./PassGuard/Images/Logo.png#gh-light-mode-only" alt="PassGuardLogo" width="200"/>
    <img src="./PassGuard/Images/Logoblancblanc.png#gh-dark-mode-only" alt="PassGuardLogo" width="200"/>
    <h1><b>PassGuard</b></h1>
</div>

Yendo al grano, **PassGuard es un gestor de contraseñas de aspecto moderno, totalmente offline y de código abierto** cuyo trabajo principal es almacenar de forma segura y proporcionar al usuario una gestión activa de sus contraseñas.

*Puedes leer esto en otros idiomas: [Español](README.es.md), [English](README.md).*

## ¿Por qué existe PassGuard? El Origen de PassGuard
----------------------------------------------------

<p>La idea de crear un gestor de contraseñas surgió en primer lugar del <b>deseo de crear un proyecto personal</b> utilizando los conocimientos obtenidos en los primeros semestres de la carrera de Ingeniería Informática.<br> 
Lo primero que hice fue <b>buscar procesos (relacionados con la informática) en mi vida</b> que pudiera optimizar. Entre los primeros procesos que se me ocurrieron fue la idea de crear un gestor de contraseñas. 
Dentro de mi círculo cercano sólo una persona utiliza un gestor de contraseñas profesional, el resto guardaba sus contraseñas en un archivo de texto plano, las escribía a mano en un papel o utilizaba la misma contraseña con pequeñas variaciones.<br>
En mi caso, <b>aunque los actuales gestores de contraseñas del mercado tienen buenas críticas</b>, algunos han sufrido fallos de seguridad (aparentemente sin consecuencias para los usuarios), y <b>el hecho de guardar todas tus valiosas contraseñas en un solo lugar me hizo desconfiar de estos servicios</b>. Por eso <b>decidí crear mi propio gestor de contraseñas :)</b><br>
Aprendí acerca de los algoritmos y protocolos de seguridad que se utilizan para almacenar estos datos de forma segura, y el resultado de esta investigación e implementación es PassGuard.</p>

## Listado de Características
-----------------------------

**PassGuard** tiene una variedad de características que se adaptan a diferentes tipos de usuarios, desde principiantes hasta usuarios avanzados. El listado de las mismas viene a continuación:

- Versión 1.1:
    - Añadida posibilidad de **marcar contraseñas como importantes**, y poder ordenarlas en la parte superior.
    - Mejorado el **gestor de colores de contorno**, con posibilidad de **importar y exportar configuraciones**.
    - Añadida posibilidad de **generar y descargar estadísticas acerca de las contraseñas** guardadas.
    - Añadida opción para **ejecutar Passguard al inicio de Windows**, o **mantenerlo ejecutándose en segundo plano** al cerrarlo.

- Versión 1.0:
    - **Crear, cargar y guardar** PassGuard Vaults en el formato .encrypted (compatible con PassGuard)
    - **Almacenamiento organizado y seguro de contraseñas** en su PassGuard Vault
    - Generador de **contraseñas fuertes**.
        - Posibilidad de **generar contraseñas aleatorias que no hayan sido crackeadas previamente**.
    - **Exportar datos** de su PassGuard Vault **a un PDF**
    - **Crear copias de seguridad y copias de seguridad automatizadas** de una PassGuard Vault seleccionada (la aplicación debe estar funcionando en ese momento)
    - **Personalización del tema claro/oscuro y del color del contorno**, guardando sus preferencias para futuras ejecuciones

Cree y use contraseñas fuertes, guárdelas y adminístrelas y **deje de preocuparse por la gestión y seguridad de sus contraseñas con PassGuard**.

## Ideas para futuras versiones
-------------------------------
Seguramente hay maneras de dejarme ideas para futuras versiones. Algunas ideas que contemplo son las siguientes:
- Tamaño de ventana no fijo y maximizable.
- Mejorar aspecto visual en pantallas con diferentes resoluciones y tamaños, ya que para monitores de 24 pulgadas o portatiles de 15 pulgadas, ambos con resolucion 1080p, la aplicación funciona bien, sin embargo con otras configuraciones pueden haber glitches.
- Añadir más estadísticas a la parte de contraseñas, y evaluarlo para la parte de colores de contorno.
- En acciones que consuman cierto tiempo, añadir el cursor WaitCursor o poner un aviso de que la acción está en progreso.

## Instalación y Dependencias
-----------------------------

**PassGuard se ejecuta correctamente en distribuciones de 64 bits de Windows**, sin embargo, dependiendo de la versión de Passguard se necesitan unas o otras dependencias.

Las dependencias según la versión son las siguientes:
- Versión **1.1**:
    - Windows OS (Windows 10 or 11, no he probado en otras versiones), 64 bits.
    - Distribución de .NET7.0 de 64 bits.

- Versión **1.0**:
    - Windows OS (Windows 10, no he probado con las otras versiones), 64 bits.
    - Distribuciones tanto de 32 como de 64 bits de .NET Framework 4.6.

Para los **usuarios básicos**, en la **sección de Releases de este repositorio** puedes encontrar **las versiones estables de la aplicación**. Dentro de cada Release encontrarás el archivo `.zip` con los instaladores `.exe` para cada sistema operativo soportado. Para **instalar la aplicación en tu sistema** sólo tienes que **descargar** el archivo `.zip` correspondiente a tu sistema operativo, **descomprimirlo** y **ejecutar** el archivo `setup.exe`.

Para los **usuarios avanzados** que quieran compilar y modificar el código, en la rama principal de este repositorio está disponible el archivo `.sln` para Visual Studio (preferiblemente ediciones 2019 o más recientes según la versión de Passguard).

## Aviso Legal
--------------

Este software es **de código abierto** y no hay **ningún tipo de garantía** asociada a él. **Utilízalo bajo tu propio riesgo**.

Diseñado por **martilux2580**.
