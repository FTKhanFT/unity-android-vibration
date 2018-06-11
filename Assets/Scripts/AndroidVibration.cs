using UnityEngine;

/// <summary>
/// Android Vibration wrapper class for the Android Java Object VibrationPlugin.
/// Allows access to native android vibration calls instead of Unity's single
/// vibration call along with other utilities such as Android's LogCat
/// </summary>
public static class AndroidVibration
{

    #region Fields

    /// <summary>
    /// The AndroidObject that is the vibration plugin.
    /// </summary>
    private static AndroidJavaObject _vibrationPlugin;

    /// <summary>
    /// The current context of the android application running on this device.
    /// </summary>
    private static AndroidJavaObject _context;

    /// <summary>
    /// Wheter this static class has been initalized successfully or not
    /// </summary>
    private static bool _isInitalized = false;

    /// <summary>
    /// The actual plugin that is used in this class
    /// </summary>
    public static AndroidJavaObject VibrationPlugin { get { return _vibrationPlugin; } }

    /// <summary>
    /// The current android context. Can be used to interface with the plugin 
    /// and other native android calls
    /// </summary>
    public static AndroidJavaObject Context { get { return _context; } }

    /// <summary>
    /// If this class has been initalized successfully or not. 
    /// </summary>
    public static bool IsInitalized { get { return _isInitalized; } }

    #endregion

    /// <summary>
    /// Default constructor for this object
    /// </summary>
    static AndroidVibration()
    {
        Initalize();
    }

    /// <summary>
    /// Initalize the current application context and plugin status.
    /// If successful then IsInitalized will be set to true.
    /// </summary>
    public static void Initalize()
    {
        // Get the current context of the application
        using (AndroidJavaClass activeClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            _context = activeClass.GetStatic<AndroidJavaObject>("currentActivity");
            if (_context == null)
            {
                Debug.LogError("There was an error getting the current context! Exiting...");
                return;
            }
        }

        // Create the plugin that we can use to call methods from the plugin
        _vibrationPlugin = new AndroidJavaObject("com.bullhorngames.ben.vibrationlibrary.AndroidVibration");
        if(_vibrationPlugin == null)
        {
            Debug.LogError("There was an error creating the plugin object!Exiting...");
            return;
        }

        _vibrationPlugin.Call("Initalize", _context);

        _isInitalized = true;
    }

    /// <summary>
    /// Determines if this phone can vibrate
    /// </summary>
    /// <returns>True if the device can vibrate</returns>
    public static bool HanVibrator()
    {
        return _vibrationPlugin.Call<bool>("HasVibrator");
    }

    /// <summary>
    /// Check whether the vibrator has amplitude control.
    /// </summary>
    /// <returns>True if the vibrator has amplitude control</returns>
    public static bool HasAmplitudeControl()
    {
        return _vibrationPlugin.Call<bool>("HasAmplitudeControl");
    }

    /// <summary>
    /// Turn the vibrator off.
    /// </summary>
    public static void CancelVibration()
    {
        _vibrationPlugin.Call("CancelVibration");
    }

    /// <summary>
    /// Create a one shot vibration for the given amount of time
    /// at the devices default vibration level
    /// </summary>
    /// <param name="time">The milliseconds to play this vibration for</param>
    public static void VibrateOneShot(long time)
    {
        _vibrationPlugin.Call("VibrateOneShot", time);
    }

    /// <summary>
    /// Create a one shot vibration for the given amount of time at the
    /// given amplitude.
    /// </summary>
    /// <param name="time">The milliseconds to play this vibration for</param>
    /// <param name="amplitude">The amplitude (strength) to play this vibration</param>
    public static void VibrateOneShot(long time, int amplitude)
    {
        _vibrationPlugin.Call("VibrateOneShot", time, amplitude);
    }

    /// <summary>
    /// Send a logcat message from this android device with the tag
    /// [UnityVibes]
    /// </summary>
    /// <param name="message">The message to send out to LogCat</param>
    public static void LogcatMessage(string message)
    {
        _vibrationPlugin.Call("LogcatMessage", message);
    }
}
