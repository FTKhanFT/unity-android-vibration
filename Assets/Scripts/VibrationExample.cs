using UnityEngine;

/// <summary>
/// Various examples of using the AndroidVibration class
/// </summary>
public class VibrationExample : MonoBehaviour
{
    /// <summary>
    /// Example of checking if this device has a vibrator, if it does
    /// than print that out in LogCat. 
    /// </summary>
	void Start ()
    {
        if(AndroidVibration.HasVibrator())
        {
            AndroidVibration.LogcatMessage("This phone can vibrate! Woo!");
        }
        else
        {
            AndroidVibration.LogcatMessage("This phone CANNOT vibrate! Awwww");
        }
    }

    /// <summary>
    /// Example of vibrating once for 400 milliseconds
    /// </summary>
    public void Vibrate()
    {
        AndroidVibration.VibrateOneShot(400);
    }

    /// <summary>
    /// Example of canceling the vibration
    /// </summary>
    public void CancelVibrate()
    {
        AndroidVibration.CancelVibration();
    }
	
    /// <summary>
    /// Example of vibrating in the pattern below that does not repeat
    /// </summary>
    public void VibratePattern()
    {
        long[] pattern = { 100, 300, 200, 100, 50, 50, 50 };
        AndroidVibration.VibrateWaveform(pattern, -1);
    }

}
