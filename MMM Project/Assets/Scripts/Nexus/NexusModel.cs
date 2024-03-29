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
    [Header("List Of Colors")]  //Una lista de colores para manejar los colores del cursor de los misiles
    [SerializeField] private List<Color> colors;
    [SerializeField] private SpriteRenderer missileCursorExt, missileCursorInt;
    private void Awake() {
        nexus = GetComponentInParent<Nexus>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        missileCursorExt.color = colors[0]; //Establezco el cursor de los misiles con el color por default
        missileCursorInt.color = colors[0];
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
    public void ChangeMissileCursorColor(int index){    //Cambio el color del cursor de los misiles segun el indice de misiles
        missileCursorExt.color = colors[index];
        missileCursorInt.color = colors[index];
    }
}
