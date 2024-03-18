using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusModel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<LineRenderer> renderers;
    [SerializeField] private List<Texture> textures;
    private SpriteRenderer spriteRenderer;
    private Nexus nexus;
    private void Awake() {
        nexus = GetComponentInParent<Nexus>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[nexus.index];
        foreach (var render in renderers)
        {
            render.material.SetTexture("_MainTex", textures[nexus.index]);
        }
    }
}
