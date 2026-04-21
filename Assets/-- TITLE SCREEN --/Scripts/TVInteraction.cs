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

    //Game UI Prompt reference here
    public bool isHoveringTV;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = offMat;
        tvCam.Priority = innactiveCameraPriority;

    }

    private void OnMouseEnter()
    {
        rend.material = onMat;
    }

    private void OnMouseExit()
    {
        rend.material = offMat;
    }

    private void OnMouseDown()
    {
        tvCam.Priority = activeCameraPriority;
    }

    private void BackToMenu(CinemachineCamera mainCam)
    {
        tvCam.Priority = innactiveCameraPriority;
        mainCam.Priority = activeCameraPriority;
    }
}
