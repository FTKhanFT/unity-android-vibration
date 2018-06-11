using UnityEngine;

/// <summary>
/// An example of using the AndroidVibration class
/// </summary>
public class VibrationExample : MonoBehaviour
{

	void Start ()
    {
        if(AndroidVibration.HanVibrator())
        {
            AndroidVibration.LogcatMessage("This phone can vibrate! Woo!");
        }
        else
        {
            AndroidVibration.LogcatMessage("This phone CANNOT vibrate! Awwww");
        }
    }

    public void Vibrate()
    {
        AndroidVibration.VibrateOneShot(400);
    }

    public void CancelVibrate()
    {
        AndroidVibration.CancelVibration();
    }
	

}
