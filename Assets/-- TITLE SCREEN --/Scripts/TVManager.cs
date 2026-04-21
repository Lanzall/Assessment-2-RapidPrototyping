using Unity.Cinemachine;
using UnityEngine;

public class TVManager : MonoBehaviour
{
    public TVInteraction[] allTVs;
    public TVInteraction currentTV;

    public CustomFade titleFader;
    public CustomFade BlackFade;

    public CinemachineCamera mainCam;

    public void Start()
    {
        BlackFade.FadeOut();
    }

    public void ActivateTV(TVInteraction tv)
    {
        currentTV = tv;

        foreach (var t in allTVs)
        {
            if (t != tv)
            {
                t.Deactivate();
            }
        }
    }

    public void BackToMenu()
    {
        if (currentTV != null)
        {
            // Lower the active TV's camera priority
            currentTV.tvCam.Priority = currentTV.innactiveCameraPriority;

            // Turn off the TV visuals
            currentTV.isActiveTV = false;
            currentTV.SetEmission(currentTV.offEmission);
            currentTV.SetScreenVisible(false);

            // Fade out the TV's title
            if (currentTV.tvTitleFader != null)
                currentTV.tvTitleFader.FadeOut();

            currentTV = null;
        }

        // Fade main title back in
        titleFader.FadeIn();

        // Raise main camera priority
        mainCam.Priority = 20;
    }
}
