using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusModel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Sprite> sprites;
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
    }
}
