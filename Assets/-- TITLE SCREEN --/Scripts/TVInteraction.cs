using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using System;

public class TVInteraction : MonoBehaviour
{
        [Header("Camera")]
    public CinemachineCamera tvCam;
    public int activeCameraPriority = 20;
    public int innactiveCameraPriority = 5;

        [Header("Visuals")]
    public Material offMat;
    public Material onMat;
    private Renderer rend;
    public GameObject screenMesh;

    [Header("CRT Flicker Settings")]
    public float flickerIntensity = 2f;
    public float flickerDuration = 0.1f;
    public int flickerSteps = 5;


        [Header("Emissive Settings")]
    public float fadeDuration = 0.25f;
    public Color offEmission = Color.black;
    public Color onEmission = Color.white;
    private Coroutine emissiveRoutine;

        [Header("TV Title")]
    public CustomFade tvTitleFader;

    public TVManager manager;

    public bool isActiveTV = false;

    //Game UI Prompt reference here

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = offMat;
        //tvCam.Priority = innactiveCameraPriority;

    }

    public void OnHoverEnter()
    {
        // If THIS TV is active, keep it lit
        if (isActiveTV)
        {
            SetEmission(onEmission);
            return;
        }

        // If ANOTHER TV is active, ignore hover
        if (manager.currentTV != null && manager.currentTV != this)
            return;

        // Otherwise, normal hover
        SetEmission(onEmission);
        SetScreenVisible(true);
    }

    public void OnHoverExit()
    {
        if (isActiveTV)
            return;

        SetEmission(offEmission);

        // If another TV is active, ignore hover exit
        if (manager.currentTV != null && manager.currentTV != this)
            return;

        rend.material = offMat;
        SetScreenVisible(false);
    }

    public void OnClick()
    {
        manager.ActivateTV(this);
        manager.titleFader.FadeOut();

        //Fade out THIS Tv's Title
        if (tvTitleFader != null)
            tvTitleFader.FadeIn();

        isActiveTV = true;
        SetEmission(onEmission);

        PlayCRTFlicker();

        SetScreenVisible(true);

        tvCam.Priority = activeCameraPriority;
    }

    public void Deactivate()
    {
        isActiveTV = false;
        SetEmission(offEmission);
        SetScreenVisible(false);

        tvCam.Priority = innactiveCameraPriority;

        // Fade out this TV's title
        if (tvTitleFader != null)
            tvTitleFader.FadeOut();

        //prompt UI
    }

    public void SetEmission(Color targetColor)
    {
        if (emissiveRoutine != null)
            StopCoroutine(emissiveRoutine);

        emissiveRoutine = StartCoroutine(FadeEmission(targetColor));
    }

    private IEnumerator FadeEmission(Color target)
    {
        Material mat = rend.material;
        Color start = mat.GetColor("_EmissionColor");
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            Color newColor = Color.Lerp(start, target, time / fadeDuration);
            mat.SetColor("_EmissionColor", newColor);
            yield return null;
        }
        mat.SetColor("_EmissionColor", target);
    }

    public void PlayCRTFlicker()
    {
        StartCoroutine(CRTFlickerRoutine());
    }

    private IEnumerator CRTFlickerRoutine()
    {
        Material mat = rend.material;
        Color baseEmission = mat.GetColor("_EmissionColor");

        for (int i = 0; i < flickerSteps; i++)
        {
            float t = (float)i / flickerSteps;

            // Random spike
            float intensity = Mathf.Lerp(flickerIntensity, 1f, t);
            Color flickerColor = baseEmission * intensity;

            mat.SetColor("_EmissionColor", flickerColor);

            // Tiny random delay for jitter
            yield return new WaitForSeconds(flickerDuration / flickerSteps);
        }

        // Restore normal emission
        mat.SetColor("_EmissionColor", baseEmission);
    }

    public void SetScreenVisible(bool visible)
    {
        if (screenMesh != null)
            screenMesh.SetActive(visible);
    }


    
}
