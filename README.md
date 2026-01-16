# AR Guardian Cube 2.0

An interactive Augmented Reality experience featuring an intelligent holographic cube guardian system. Place virtual cubes in AR space that change state based on proximity to the camera, with visual feedback through dynamic colors and glow effects.

## Overview

AR Guardian Cube is a Unity-based AR application that demonstrates real-time proximity detection and visual state management in augmented reality. Users can tap to place holographic cubes in AR space that respond to camera proximity with color changes and status warnings.

## Features

- **Tap-to-Place**: Easy gesture-based placement of AR objects on detected planes
- **Proximity Detection**: Real-time distance monitoring between camera and placed objects
- **Dynamic Visual States**: 
  - Green (Safe): Object at normal distance (>0.5m)
  - Yellow (Warning): Restricted area (0.2m - 0.5m)
  - Red (Critical): Too close (<0.2m)
- **Holographic Effects**: Custom shader with glow intensity that responds to proximity
- **Billboard UI**: Status text that always faces the camera for optimal readability
- **ARCore Support**: Plane detection and spatial understanding using ARCore/ARFoundation

## Requirements

- **Unity Version**: 6000.2.14f1 (Latest LTS)
- **Platform**: Android with ARCore support
- **Device Requirements**: ARCore-compatible device with camera and tracking capabilities

## Dependencies

Core AR packages:
- `com.unity.xr.arfoundation` (6.2.1) - AR framework
- `com.unity.xr.arcore` (6.2.1) - Android AR support
- `com.unity.inputsystem` (1.16.0) - Input handling
- `com.unity.shadergraph` (17.2.0) - Custom shader creation
- `com.unity.textmeshpro` - Advanced text rendering

## Project Structure

```
AR_Guardian_Cube_2.0/
├── Assets/
│   ├── Scripts/               # Core gameplay scripts
│   │   ├── GuardianState.cs   # Proximity detection & state management
│   │   ├── TapToPlace.cs      # AR placement system
│   │   └── Billboard.cs       # Camera-facing UI
│   ├── Resources/             # Prefabs and references
│   ├── Scenes/
│   │   └── SampleScene.unity  # Main AR scene
│   ├── XR/                    # AR configuration
│   └── HologramShader.shadergraph  # Custom shader for hologram effect
├── Builds/                    # Built application files
├── Packages/
│   └── manifest.json          # Package dependencies
└── ProjectSettings/           # Unity configuration
```

## Core Scripts

### GuardianState.cs
Manages the cube's visual state based on camera proximity:
- Monitors distance between AR camera and cube object
- Updates material color and glow intensity
- Displays proximity warnings via TextMeshPro

**Distance Thresholds**:
- < 0.2m: CRITICAL (Red, Glow 4.0)
- 0.2m - 0.5m: WARNING (Yellow, Glow 2.5)
- > 0.5m: SAFE (Green, Glow 1.2)

### TapToPlace.cs
Handles AR object placement through touch input:
- Uses ARRaycastManager for plane detection
- Places objects on detected horizontal planes
- Maintains proper object orientation and position

### Billboard.cs
Ensures UI text always faces the camera:
- Billboard behavior for status text display
- Improves readability in AR space

## Getting Started

### Setup

1. Open the project in Unity 6000.2.14f1 or later
2. Build settings are pre-configured for Android with ARCore
3. Connect an ARCore-compatible Android device

### Building

1. Go to **File > Build Settings**
2. Select **Android** as target platform
3. Configure player settings as needed
4. Click **Build and Run**

### Usage

1. Launch the app on an ARCore device
2. Allow camera permissions when prompted
3. Move device to detect planes (floor/table surfaces)
4. Tap on a detected plane to place the Guardian Cube
5. Move closer/farther to observe proximity-based color changes

## Customization

### Adjusting Proximity Thresholds

Edit distance values in `GuardianState.cs`:
```csharp
if (distance < 0.2f)  // Critical threshold (in meters)
if (distance < 0.5f)  // Warning threshold (in meters)
```

### Modifying Colors and Effects

Change color and glow values in `GuardianState.cs`:
```csharp
cubeRenderer.material.SetColor(BaseColorID, Color.red);
cubeRenderer.material.SetFloat(GlowID, 4f);  // Glow intensity
```

### Custom Status Messages

Update text in `GuardianState.cs`:
```csharp
statusText.text = "Your custom message";
```

## Shader

The holographic appearance is created using a custom ShaderGraph (`HologramShader.shadergraph`) that supports:
- Dynamic base color changes
- Adjustable glow intensity
- Real-time material property updates

## Troubleshooting

**App crashes on startup**: Ensure ARCore is installed on your device and app has camera permissions

**Cube not appearing**: Check that planes are being detected (usually floor surfaces in good lighting)

**Text not visible**: Ensure the Billboard script is attached to the status text object

**No proximity changes**: Verify GuardianState script is assigned the correct AR Camera reference

## Performance

- Optimized for mobile AR with minimal GPU overhead
- Proximity checks run every frame for real-time response
- Shader-based visual effects reduce CPU load

## Platform Support

- **Android**: ARCore 6.2.1+
- **iOS**: Consider adding ARKit support via UnityXR plugins

## Future Enhancements

- Multiple cube instances with independent states
- Sound effects for proximity warnings
- Gesture-based cube manipulation
- Data logging and analytics
- Cloud-based cube placement sharing

## License

Project created for AR demonstration purposes.

## Support

For issues or questions, review the scripts and ensure all AR setup is properly configured in ProjectSettings.
