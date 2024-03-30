using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusModel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<LineRenderer> renderers;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer missileCursorExt, missileCursorInt; //Los cursores para cambiarles los colores
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMissileCursorColor(Color color, Sprite sprite, Texture texture){    //Cambio el color del cursor de los misiles segun el indice de misiles
        missileCursorExt.color = color;
        missileCursorInt.color = color;
        spriteRenderer.sprite = sprite;
        foreach (var render in renderers)
        {
            render.material.SetTexture("_MainTex", texture);
        }
    }
}
