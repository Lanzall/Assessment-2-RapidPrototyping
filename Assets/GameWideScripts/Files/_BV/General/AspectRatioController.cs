using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    // Desired aspect ratio (16:9)
    private float targetAspectRatio = 16f / 9f;

    private void Start()
    {
        // Calculate the target resolution based on the desired aspect ratio
        int targetWidth = 1920;
        int targetHeight = Mathf.RoundToInt(targetWidth / targetAspectRatio);

        // Set the resolution and adjust the screen settings
        Screen.SetResolution(targetWidth, targetHeight, fullscreen: true);
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;          
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
    }
}
