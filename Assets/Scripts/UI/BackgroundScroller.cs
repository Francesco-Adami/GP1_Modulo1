using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Material backgroundMaterial;
    private Vector2 offset;

    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = offset;
    }
}