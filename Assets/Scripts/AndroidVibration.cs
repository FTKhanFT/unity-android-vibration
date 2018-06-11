using UnityEngine;

/// <summary>
/// Android Vibration wrapper class for the Android Java Object VibrationPlugin.
/// Allows access to native android vibration calls instead of Unity's single
/// vibration call along with other utilities such as Android's LogCat.
/// 
/// Some descriptions of methods have just been copied from the Android developer
/// section to keep this 1 to 1 and fully transparent
/// https://developer.android.com/reference/android/os
/// 
/// Author: Ben Hoffman
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
    /// Static constructor for this object.
    /// Calls the Initalize function for this object.
    /// </summary>
    static AndroidVibration()
    {
        Initalize();
    }

    /// <summary>
    /// Initalize the current application context and plugin status.
    /// If successful then IsInitalized will be set to true.
    /// This can be useful if you want to reset the activity or for 
    /// debugging
    /// </summary>
    public static void Initalize()
    {
        _isInitalized = false;

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

        // Call the inialize function of the actual .jar library
        _vibrationPlugin.Call("Initalize", _context);

        _isInitalized = true;
    }

    /// <summary>
    /// Determines if this phone can vibrate
    /// </summary>
    /// <returns>True if the device can vibrate</returns>
    public static bool HasVibrator()
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
    /// Create a waveform vibration. Waveform vibrations are a potentially 
    /// repeating series of timing and amplitude pairs. For each pair, 
    /// the value in the amplitude array determines the strength of the 
    /// vibration and the value in the timing array determines how long 
    /// it vibrates for. An amplitude of 0 implies no vibration (i.e. off),
    /// and any pairs with a timing value of 0 will be ignored.
    /// </summary>
    /// <param name="pattern">The pattern of alternating on-off timings, starting with off.
    ///                       Timing values of 0 will cause the timing / amplitude pair to 
    ///                       be ignored.</param>
    /// <param name="repeat">The index into the timings array at which to repeat, or -1 if
    ///                      you you don't want to repeat.</param>
    public static void VibrateWaveform(long[] pattern, int repeat)
    {
        _vibrationPlugin.Call("VibrateWaveform", pattern, repeat);
    }

    /// <summary>
    /// Create a waveform vibration. Waveform vibrations are a potentially repeating
    /// series of timing and amplitude pairs. For each pair, the value in the amplitude
    /// array determines the strength of the vibration and the value in the timing array
    /// determines how long it vibrates for. An amplitude of 0 implies no vibration 
    /// (i.e. off), and any pairs with a timing value of 0 will be ignored.
    /// </summary>
    /// <param name="pattern">The timing values of the timing / amplitude pairs. 
    ///                       Timing values of 0 will cause the pair to be ignored</param>
    /// <param name="amplitudes">The amplitude values of the timing / amplitude pairs.
    ///                         Amplitude values must be between 0 and 255, or equal to
    ///                         DEFAULT_AMPLITUDE. An amplitude value of 0 implies the 
    ///                         motor is off.></param>
    /// <param name="repeat">The index into the timings array at which to repeat, 
    ///                      or -1 if you you don't want to repeat.</param>
    public static void VibrateWaveform(long[] pattern, int[] amplitudes, int repeat)
    {
        _vibrationPlugin.Call("VibrateWaveform", pattern, amplitudes, repeat);
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
