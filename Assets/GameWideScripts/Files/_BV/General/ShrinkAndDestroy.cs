using System.Collections;
using UnityEngine;

public class ShrinkAndDestroy : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    private Vector3 originalScale;
    private float elapsedTime = 0f;
    private SkinnedMeshRenderer meshRenderer;

    private IEnumerator Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.enabled = false;
        originalScale = transform.localScale;
        yield return new WaitForEndOfFrame();
        meshRenderer.enabled = true;
    }
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);
        float curve = Mathf.Sin(t * Mathf.PI);
        transform.localScale = originalScale * curve;

        if (t >= 1f)
            Destroy(gameObject);
    }
}