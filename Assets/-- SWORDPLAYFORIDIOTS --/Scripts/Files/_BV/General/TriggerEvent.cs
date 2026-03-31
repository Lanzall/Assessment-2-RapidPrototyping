using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public string[] checkTags;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;
    public void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onTriggerEnter?.Invoke();
        }
    }
    public void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onTriggerStay?.Invoke();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < checkTags.Length; i++)
        {
            if(other.gameObject.CompareTag(checkTags[i]))
                onTriggerExit?.Invoke();
        }
    }
}