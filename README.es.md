<div id="PassGuardLogo" align="center">
    <br />
    <img src="./PassGuard/Images/Logo.png#gh-light-mode-only" alt="PassGuardLogo" width="200"/>
    <img src="./PassGuard/Images/Logoblancblanc.png#gh-dark-mode-only" alt="PassGuardLogo" width="200"/>
    <h1><b>PassGuard</b></h1>
</div>

Yendo al grano, **PassGuard es un gestor de contraseñas de aspecto moderno, totalmente offline y de código abierto** cuyo trabajo principal es almacenar de forma segura y proporcionar al usuario una gestión activa de sus contraseñas. **PassGuard se ejecuta correctamente en distribuciones de 64 bits de Windows 10**, sin embargo **se requieren distribuciones de 32 y 64 bits de .NET Framework 4.6** para su funcionamiento.

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

- **Crear, cargar y guardar** PassGuard Vaults en el formato .encrypted (compatible con PassGuard)
- **Almacenamiento organizado y seguro de contraseñas** en su PassGuard Vault
- Generador de **contraseñas fuertes**.
    - Posibilidad de **generar contraseñas aleatorias que no hayan sido crackeadas previamente**.
- **Exportar datos** de su PassGuard Vault **a un PDF**
- **Crear copias de seguridad y copias de seguridad automatizadas** de una PassGuard Vault seleccionada (la aplicación debe estar funcionando en ese momento)
- **Personalización del tema claro/oscuro y del color del contorno**, guardando sus preferencias para futuras ejecuciones

Cree y use contraseñas fuertes, guárdelas y adminístrelas y **deje de preocuparse por la gestión y seguridad de sus contraseñas con PassGuard**.

## Instalación
--------------

Ahora mismo PassGuard sólo está disponible para distribuciones de 64 bits de Windows 10 (aunque debo comprobar Windows 11).

Para los **usuarios básicos**, en la **sección de Releases de este repositorio** puedes encontrar **las versiones estables de la aplicación**. Dentro de cada Release encontrarás el archivo `.zip` con los instaladores para cada sistema operativo soportado. Para **instalar la aplicación en tu sistema** sólo tienes que **descargar** el archivo `.zip` correspondiente a tu sistema operativo, **descomprimirlo** y **ejecutar** el archivo `setup.exe`.

Para los **usuarios avanzados** que quieran compilar y modificar el código, en la rama principal de este repositorio está disponible el archivo `.sln` para Visual Studio (preferiblemente ediciones 2019 o más recientes).

## Aviso Legal
--------------

Este software es **de código abierto** y no hay **ningún tipo de garantía** asociada a él. **Utilízalo bajo tu propio riesgo**.

Diseñado por **martilux2580**.
