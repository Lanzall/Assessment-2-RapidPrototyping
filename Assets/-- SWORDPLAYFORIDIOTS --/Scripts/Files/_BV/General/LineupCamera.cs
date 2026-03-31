using UnityEngine;

public class LineupCamera : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Camera cam;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector2(-h, v) * Time.deltaTime * speed, Space.World);
        cam.orthographicSize = Mathf.Max(0.1f, cam.orthographicSize + mInput);
    }
}
