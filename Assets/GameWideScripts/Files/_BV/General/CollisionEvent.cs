using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public string[] checkTags;
    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionStay;
    public UnityEvent onCollisionExit;
    public void OnCollisionEnter(Collision other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onCollisionEnter?.Invoke();
        }
    }
    public void OnCollisionStay(Collision other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onCollisionStay?.Invoke();
        }
    }
    public void OnCollisionExit(Collision other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onCollisionExit?.Invoke();
        }
    }
}