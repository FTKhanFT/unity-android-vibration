# Unity Android Vibration
This is a Unity plugin for a native android plugin that will allow native control over the android vibration API

This is useful because Unity's default handheld device vibration is [somewhat lacking](https://docs.unity3d.com/ScriptReference/Handheld.Vibrate.html) for more complex vibration patterns (for what I assume is good reason, seeing that cross platform vibration is probably a _huge_ pain in the butt).

What I have done here is write a wrapper around a the [Vibrator class](https://developer.android.com/reference/android/os/Vibrator) to allow all of the normal functionality for Android developers in Unity.


## Setup

After importing the plugin, you are going to need to give your app permissions to use vibrations in the `AndroidManifest.xml`. I found that the easiest way to do this is to build your app, and then head to `Temp/StagingArea/` where you will find `AndroidManifest.xml`. Copy this file to your `Assets/Plugins/Android`, and then add the permission to that file. Now Unity will use that manifest file instead of the one in the staging area. Here is the permission to add:

```
<uses-permission android:name="android.permission.VIBRATE"/>
```

If you need to learn more about how manifest files work, then head to [Google's documentation on it](https://developer.android.com/guide/topics/manifest/manifest-intro).

## Usage

`AndroidVibration` is a static class that will allow you to make simple calls like `AndroidVibration.Vibrate(100)` to vibrate the phone. If you want to interface directly with the `.jar` then I have also exposed that through the `VibrationPlugin` field.

There is a simple example file under `VibrationExample`
