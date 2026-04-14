using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private static ScreenShot instance;

    private Camera cam;
    private bool takeScreenshotOnNextFrame;
    int screenshotIndex = 1;
    private void Awake()
    {
        instance = this;
        cam = gameObject.GetComponent<Camera>();
    }


    //private void OnPostRender()
    //{
    //    if(takeScreenshotOnNextFrame)
    //    {
    //        takeScreenshotOnNextFrame = false;
    //        RenderTexture renderTexture = cam.targetTexture;

    //        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
    //        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
    //        renderResult.ReadPixels(rect, 0, 0);

    //        byte[] byteArray = renderResult.EncodeToPNG();
    //        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
    //        Debug.Log("Saved CameraScreenshot.png");

    //        RenderTexture.ReleaseTemporary(renderTexture);
    //        cam.targetTexture = null;
    //    }
    //}
    IEnumerator PostRender()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = cam.targetTexture;

        Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        renderResult.ReadPixels(rect, 0, 0);

        byte[] byteArray = renderResult.EncodeToJPG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot" + screenshotIndex + ".jpg", byteArray);
        Debug.Log(Application.dataPath + "/CameraScreenshot" + screenshotIndex + ".jpg");
        Debug.Log("Saved CameraScreenshot" + screenshotIndex + ".jpg");
        screenshotIndex++;

        RenderTexture.ReleaseTemporary(renderTexture);
        cam.targetTexture = null;
    }
    private void TakeScreeshot(int width, int height)
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        StartCoroutine(PostRender());
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreeshot(width, height);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot_Static(Screen.width, Screen.height);
        }
    }
}
