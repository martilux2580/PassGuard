# Checklist antes / después de construir el APK

## Cuando hagas el build nativo (`npx expo run:android`)

### ✅ Verificar: derivación de claves rápida
- Ir a Vault Search, introducir credenciales y pulsar SEARCH
- La derivación debería completarse en **menos de 1 segundo** (antes tardaba minutos)
- Si tarda más de 2-3 segundos, el módulo `react-native-quick-crypto` no se ha enlazado correctamente

### ✅ Verificar: derivación continúa en background
- Iniciar una búsqueda (si la derivación ya es rápida, este punto es irrelevante)
- Pulsar el botón cuadrado de Android y cambiar a otra app
- Volver: la derivación debería haber continuado (o ya terminado)
- El mensaje "(mantén la app abierta)" ya no tiene razón de ser → eliminarlo del código

### 🔧 Cambio de código pendiente tras confirmar que quick-crypto funciona
En `vault-credentials.tsx`, línea con el mensaje de carga:
```
`Deriving keys… ${progress.toFixed(0)}% (mantén la app abierta)`
```
Cambiar a simplemente:
```
`Deriving keys… ${progress.toFixed(0)}%`
```
O incluso eliminarlo del todo si la derivación es instantánea.
