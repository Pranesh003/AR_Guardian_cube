# AR Guardian Cube 2.0 - Setup Guide

Complete step-by-step guide to set up, configure, and build the AR Guardian Cube project.

## Prerequisites

### Software
- **Unity 6000.2.14f1** (Download from [Unity Hub](https://unity.com/download))
- **Android SDK** (API level 24+)
- **Android NDK** (Required for ARCore)
- **Visual Studio Code** or **Visual Studio** (C# IDE)

### Hardware
- **Development Machine**: Windows, macOS, or Linux
- **Target Device**: Android phone with ARCore support (Android 7.0+)

### Internet Connection
Required for downloading:
- Unity Editor and modules
- Package dependencies
- ARCore SDK

## Step 1: Install Unity

1. Download **Unity Hub** from https://unity.com/download
2. Install Unity Hub on your computer
3. In Unity Hub, click **Install Editor**
4. Select version **6000.2.14f1**
5. Check the boxes for:
   - ✓ Android Build Support
   - ✓ Android SDK & NDK Tools
   - ✓ OpenJDK
6. Click **Install** and wait for completion

## Step 2: Clone/Open Project

### Option A: Open Existing Project
1. Open Unity Hub
2. Click **Open** → **Add project from disk**
3. Navigate to `d:\projects\AR_Guardian_Cube_2.0`
4. Select the folder and click **Open**
5. Unity will load the project (first load takes 2-5 minutes)

### Option B: Fresh Clone
```bash
git clone https://github.com/your-repo/AR_Guardian_Cube_2.0.git
cd AR_Guardian_Cube_2.0
```

## Step 3: Verify Project Configuration

1. Open the project in Unity
2. Go to **Edit → Project Settings**

### Check Android Settings
- **Platform**: Android
- **Scripting Backend**: IL2CPP
- **Target API Level**: 34+
- **Minimum API Level**: 24+

### Check AR Settings
- **XR Plugin Management** → Android
  - ✓ OpenXR Backend
  - ✓ ARCore enabled

3. Navigate to **Assets → Scenes → SampleScene.unity** to verify scene setup

## Step 4: Configure Build Settings

1. Go to **File → Build Settings**
2. Click **Add Open Scenes** (adds SampleScene.unity)
3. Platform: Select **Android**
4. Player Settings:
   - **Company Name**: Enter your organization
   - **Product Name**: "AR Guardian Cube"
   - **Version**: 1.0.0
   - **Bundle Version Code**: 1

### Android-Specific Settings
**Edit → Project Settings → Player:**

- **Identification**:
  - Package Name: `com.yourcompany.arg2` (e.g., `com.example.arg2`)
  - Minimum API Level: 24
  - Target API Level: 34

- **Graphics**:
  - Graphics API: Vulkan + OpenGL ES 3

- **XR Settings**:
  - Depth 16-bit
  - Stereo Rendering Mode: Single Pass

## Step 5: Android Device Setup

### Enable Developer Mode
1. On your Android device, go to **Settings → About Phone**
2. Tap **Build Number** 7 times
3. Go to **Settings → Developer Options**
4. Enable:
   - ✓ USB Debugging
   - ✓ Install via USB
   - ✓ Android Debugging Bridge (ADB)

### Connect Device
1. Connect Android device to computer via USB
2. On device, tap **Allow** when prompted for USB debugging permission
3. In Unity, you should see your device when going to **Window → Android Logcat**

### Verify Connection
In terminal/command prompt:
```bash
adb devices
```
You should see your device listed.

## Step 6: Build for Android

### Build APK

1. **File → Build Settings**
2. Ensure **Android** is selected
3. Click **Build APK**
4. Choose a folder to save (e.g., `Builds/` folder)
5. Unity will compile and build (takes 3-10 minutes)

### Build and Run

1. **File → Build Settings**
2. Ensure **Android** is selected and device is connected
3. Click **Build and Run**
4. Select build location
5. Unity automatically uploads and launches app

**Output**: APK file saved to build folder

## Step 7: First Run

### On Device
1. App launches automatically after build
2. Grant **Camera Permission** when prompted
3. Allow **AR Tracking** permissions
4. Move device around to detect planes (floor/tables)
5. Tap to place Guardian Cube
6. Move closer/farther to see color changes

### Expected Behavior
- **Green cube** at normal distance (>0.5m) - "SYSTEM ARMED"
- **Yellow cube** when warning distance (0.2-0.5m) - "WARNING: RESTRICTED AREA"
- **Red cube** when too close (<0.2m) - "CRITICAL HALT // BACK AWAY"

## Step 8: Testing

### Test Placement
- [ ] Tap on floor surface → cube appears
- [ ] Cube shows up at tap location
- [ ] Text faces toward camera

### Test Proximity Detection
- [ ] Move closer → yellow text appears
- [ ] Move much closer → red text appears
- [ ] Move back → green text appears

### Test Visual Feedback
- [ ] Cube color changes smoothly
- [ ] Glow intensity increases as you approach
- [ ] Status text updates immediately

## Troubleshooting

### Build Fails: "Cannot find Android SDK"
**Solution**:
1. Go to **Edit → Preferences** (or **Edit → Settings** on macOS)
2. **External Tools → Android**
3. Set Android SDK Path to: `C:\Program Files\Unity\Hub\Editor\[version]\Editor\Data\PlaybackEngines\AndroidPlayer\SDK`
4. Rebuild

### Device Not Showing in Build & Run
**Solution**:
```bash
# Terminal/Command Prompt
adb kill-server
adb start-server
adb devices
```
Reconnect device and try again.

### App Crashes on Startup
**Possible Causes**:
1. **ARCore not installed**: Install ARCore from Google Play Store
2. **Permissions**: Check camera permission is granted
3. **Device not supported**: Verify device supports ARCore (most modern Android phones do)

**Solution**:
- Check Logcat: **Window → Android Logcat** for error messages
- Install ARCore manually on device: Open Play Store → search "ARCore"

### Cube Not Appearing
**Possible Causes**:
1. Planes not detected
2. Poor lighting conditions
3. Not tapping on detected plane surface

**Solution**:
- Ensure good lighting
- Move camera to horizontal surface (floor/table)
- Wait 2-3 seconds for plane detection
- Tap in center of room on floor

### Text Not Visible
**Possible Causes**:
1. Billboard script not attached
2. Camera reference missing
3. Text color same as background

**Solution**:
1. In Editor, select status text object
2. Verify Billboard script is in Inspector
3. Check TMP_Text component has proper text rendering settings

### Performance Issues
**Solution**:
- Build in **Release** mode (not Debug)
- Reduce target resolution if needed
- Check device has sufficient RAM

## Development Setup

### Opening in VS Code/Visual Studio

1. **Assets → Open C# Project** (opens in default IDE)
2. Or manually:
   - **File → Open Project Settings** → locate `.csproj` file
   - Open with Visual Studio Code or Visual Studio

### Enable Script Debugging

1. **Edit → Preferences → External Tools**
2. Set external script editor to VS Code or Visual Studio
3. Attach debugger when needed

## Building Release Version

For App Store/Play Store distribution:

1. **File → Build Settings**
2. Under Android, click **Player Settings**
3. **Player → Identification**:
   - Create Signing Certificate (or use existing)
   - Configure keystores
4. **Build Settings** → Select **Development** → Uncheck
5. Click **Build**

## Performance Optimization

For better performance on older devices:

1. **Edit → Project Settings → Quality**
   - Set to **Fast** for mobile
   - Disable shadows
   - Lower particle counts

2. **Edit → Project Settings → Graphics**
   - Enable GPU Instancing
   - Reduce draw calls

3. In code:
   - Profile with **Profiler** (Window → Analysis → Profiler)
   - Optimize scripts for mobile

## Distribution

### Create Release APK

```bash
# Build from command line (useful for CI/CD)
unity -batchmode -executeMethod EditorBuildSettings.BuildTarget \
  -buildTarget Android \
  -projectPath . \
  -logFile output.log
```

### Share Build
- Upload APK to Google Play Store
- Share APK directly (side-loading)
- Create app bundle for Play Store

## Next Steps

1. Review [README.md](README.md) for project overview
2. Check [Assets/Scripts/README.md](Assets/Scripts/README.md) for script documentation
3. Customize colors, thresholds, and messages
4. Add additional AR features
5. Test on multiple devices
6. Prepare for deployment

## Resources

- [Unity AR Foundation Documentation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@latest)
- [ARCore Documentation](https://developers.google.com/ar)
- [Unity Android Build Guide](https://docs.unity3d.com/Manual/android.html)
- [TextMeshPro Documentation](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest)

## Support

For issues:
1. Check error messages in **Android Logcat**
2. Review Unity Editor console for errors
3. Verify all requirements are met
4. Test on different Android device if possible
5. Check official documentation links above

## Quick Reference

| Task | Path |
|------|------|
| Build Settings | File → Build Settings |
| Project Settings | Edit → Project Settings |
| Android Logcat | Window → Android Logcat |
| Profiler | Window → Analysis → Profiler |
| Scene | Assets/Scenes/SampleScene.unity |
| Scripts | Assets/Scripts/ |

---

**Last Updated**: January 2026  
**Unity Version**: 6000.2.14f1  
**AR Foundation**: 6.2.1
