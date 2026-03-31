using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private float speed = 1;
    
    public void SetSpeed(float _speed) => speed = _speed;
    
    private void Update()
    {
        transform.Rotate(rotationAngle, Time.deltaTime * speed);
    }
}
