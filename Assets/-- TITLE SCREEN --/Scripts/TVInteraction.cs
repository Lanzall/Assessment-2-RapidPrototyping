using UnityEngine;
using Unity.Cinemachine;

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
        if(!isActiveTV)
            rend.material = onMat;
    }

    public void OnHoverExit()
    {
        if(!isActiveTV)
            rend.material = offMat;
    }

    public void OnClick()
    {
        manager.ActivateTV(this);

        isActiveTV = true;
        rend.material = onMat;

        tvCam.Priority = activeCameraPriority;
    }

    public void Deactivate()
    {
        isActiveTV = false;
        rend.material = offMat;

        tvCam.Priority = innactiveCameraPriority;

        //prompt UI
    }

    private void BackToMenu(CinemachineCamera mainCam)
    {
        tvCam.Priority = innactiveCameraPriority;
        mainCam.Priority = activeCameraPriority;
    }
}
