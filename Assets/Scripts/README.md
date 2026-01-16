# AR Guardian Cube - Scripts Documentation

This directory contains the core gameplay and AR interaction scripts for the AR Guardian Cube application.

## Overview

The scripts folder contains three essential C# scripts that work together to create the interactive AR experience:
1. **GuardianState.cs** - Proximity detection and visual state management
2. **TapToPlace.cs** - AR object placement through touch input
3. **Billboard.cs** - UI text that faces the camera

## Script Descriptions

### GuardianState.cs

**Purpose**: Manages the holographic cube's visual appearance and status based on proximity to the AR camera.

**Key Features**:
- Real-time distance calculation between camera and cube
- Dynamic color changes based on proximity zones
- Glow intensity adjustment via shader
- Status text updates for user feedback

**Required Components**:
- `Transform arCamera` - Reference to AR camera transform
- `TMP_Text statusText` - TextMeshPro text component for status display
- `Renderer cubeRenderer` - Cube's renderer component

**Proximity States**:
| Distance | Color  | Glow | Status Text              |
|----------|--------|------|--------------------------|
| < 0.2m   | Red    | 4.0  | CRITICAL HALT // BACK AWAY |
| 0.2-0.5m | Yellow | 2.5  | WARNING: RESTRICTED AREA |
| > 0.5m   | Green  | 1.2  | SYSTEM ARMED            |

**How It Works**:
```
Update() → Calculate distance → Check thresholds → Update material → Update text
```

### TapToPlace.cs

**Purpose**: Handles placement of AR objects on detected planes through touch input.

**Key Features**:
- Touch input detection
- ARRaycast plane detection
- Object positioning and activation
- Automatic plane recognition

**Required Components**:
- `ARRaycastManager` - Scene-based component for raycasting
- `GameObject placementObject` - The object to place in AR

**Workflow**:
1. Monitor for touch input
2. On touch, raycast from screen position
3. Detect intersection with AR planes
4. Position object at hit location with proper rotation
5. Activate the placement object

**Touch Input Flow**:
```
Input.touchCount > 0 → Check touch phase → Raycast → Hit plane → Update position
```

### Billboard.cs

**Purpose**: Ensures UI text always faces the camera for optimal readability in AR space.

**Key Features**:
- Automatic camera detection
- Real-time orientation updates
- Smooth text rotation toward camera

**Required Components**:
- `Camera` - Main camera (auto-detected)

**How It Works**:
```
LateUpdate() → Get camera forward → Rotate object to face camera
```

Uses `transform.LookAt()` to orient the object toward the camera's forward vector.

## Setup Instructions

### Scene Setup

1. **Create AR Session**:
   - Add AR Session Manager component to scene
   - Add AR Plane Manager for surface detection

2. **Set Up AR Camera**:
   - Create AR Camera from XR Rig prefab
   - Assign to GuardianState's `arCamera` field

3. **Create Guardian Cube**:
   - Create a Cube primitive
   - Add GuardianState script
   - Create a child object for status text
   - Add TextMeshPro component to text object
   - Assign cubeRenderer (the cube's Renderer)
   - Assign statusText (the TextMeshPro component)

4. **Add Billboard to UI**:
   - Attach Billboard script to status text object
   - Script auto-detects main camera

5. **Set Up Placement**:
   - Assign the Guardian Cube as `placementObject` in TapToPlace
   - TapToPlace auto-finds ARRaycastManager

## Integration Example

**Basic scene hierarchy**:
```
ARSession
├── XR Origin
│   └── AR Camera (with arCamera reference)
└── Guardian Cube (Inactive, assigned to TapToPlace)
    ├── Cube Mesh
    │   └── Status Text (Billboard script + TMP_Text)
    └── GuardianState script
```

## Public Properties

### GuardianState
- `arCamera: Transform` - Reference to the AR camera
- `statusText: TMP_Text` - Status display text
- `cubeRenderer: Renderer` - Cube renderer for material updates

### TapToPlace
- `placementObject: GameObject` - Object to place in AR space

### Billboard
- No public properties (self-contained)

## Shader Properties Used

Both GuardianState uses the following shader properties:
- `_BaseColor` - Main color of the cube
- `_GlowIntensity` - Glow effect strength

These are defined in `HologramShader.shadergraph`.

## Performance Considerations

- **GuardianState**: Runs distance calculation every frame (~1ms)
- **TapToPlace**: Only processes on touch input
- **Billboard**: Runs every frame but uses simple math (~0.5ms)

## Common Customizations

### Adjust Proximity Distances

In `GuardianState.cs`, modify the if conditions:
```csharp
// Change these values (in meters)
if (distance < 0.2f)  // Critical threshold
if (distance < 0.5f)  // Warning threshold
```

### Change Status Messages

In `GuardianState.cs`, modify the text assignments:
```csharp
statusText.text = "YOUR CUSTOM MESSAGE";
```

### Add Sound Effects

Extend `GuardianState.cs` to add audio playback when state changes:
```csharp
AudioSource audioSource;
void Start() { audioSource = GetComponent<AudioSource>(); }
// In proximity state checks:
audioSource.PlayOneShot(warningSound);
```

### Allow Multiple Placements

Modify `TapToPlace.cs` to instantiate new objects instead of reusing one:
```csharp
Instantiate(placementObject, hitPose.position, hitPose.rotation);
```

## Debugging

### Enable Debug Logging

Add to any script to track state changes:
```csharp
Debug.Log($"Distance: {distance:F2}m, State: {statusText.text}");
```

### Common Issues

1. **Script not finding components**: Ensure components are properly assigned in Inspector
2. **Proximity not changing**: Check arCamera reference is correct and camera is moving
3. **Placement not working**: Verify ARPlaneManager is enabled and detecting planes
4. **Text not visible**: Ensure Billboard script is attached to text object and camera reference exists

## Script Dependencies

```
GuardianState.cs
├── Requires: Transform (arCamera), TMP_Text, Renderer
└── Uses: UnityEngine, TMPro

TapToPlace.cs
├── Requires: GameObject (placementObject)
├── Uses: UnityEngine, UnityEngine.XR.ARFoundation, UnityEngine.XR.ARSubsystems
└── Finds: ARRaycastManager (automatic)

Billboard.cs
├── Requires: Camera (auto-detected)
└── Uses: UnityEngine
```

## API Reference

### GuardianState

```csharp
public class GuardianState : MonoBehaviour
{
    public Transform arCamera;                    // AR camera transform
    public TMP_Text statusText;                   // Status text display
    public Renderer cubeRenderer;                 // Cube renderer
    
    // Shader property IDs (cached for performance)
    static readonly int BaseColorID;              // "_BaseColor"
    static readonly int GlowID;                   // "_GlowIntensity"
    
    void Update();                                // Called every frame
}
```

### TapToPlace

```csharp
public class TapToPlace : MonoBehaviour
{
    public GameObject placementObject;            // Object to place
    
    private ARRaycastManager raycastManager;      // Auto-found
    private static List<ARRaycastHit> hits;       // Raycast results
    
    void Start();                                 // Initialize
    void Update();                                // Handle input
}
```

### Billboard

```csharp
public class Billboard : MonoBehaviour
{
    private Camera cam;                           // Main camera (auto-found)
    
    void Start();                                 // Initialize
    void LateUpdate();                            // Rotate to face camera
}
```

## Version Information

- **Unity Version**: 6000.2.14f1
- **C# Version**: .NET Framework 4.7.1
- **AR Foundation**: 6.2.1
- **ARCore**: 6.2.1

## Next Steps

- Extend scripts with additional features
- Add more AR interactions
- Implement data persistence
- Create UI for configuration
- Add animations and particle effects
