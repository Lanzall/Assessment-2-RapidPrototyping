using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        transform.LookAt(mainCam.transform.position);
    }
}
