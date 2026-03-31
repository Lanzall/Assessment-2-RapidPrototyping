using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TextureOffset : MonoBehaviour
{
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    private Renderer renderer;
    
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
