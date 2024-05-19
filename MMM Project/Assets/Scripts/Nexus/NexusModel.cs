using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusModel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<LineRenderer> renderers;  //Los LineRenderers de la electricidad
    private SpriteRenderer spriteRenderer;  //El Sprite del Nexo
    [SerializeField] private SpriteRenderer missileCursorExt, missileCursorInt; //Los cursores para cambiarles los colores
    private bool pauseState = true;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Start(){
        foreach (var render in renderers)
        {
            render.gameObject.SetActive(false);
        }
    }
    public void StartState(){
        foreach (var render in renderers)
        {
            render.gameObject.SetActive(true);
        }
        pauseState = false;
    }
    void Update()
    {
        
    }
    public void ChangeNexusModel(Color color, Sprite sprite, Texture texture){    //Cambio el color del cursor de los misiles 
        if(!pauseState){
            missileCursorExt.color = color;
            missileCursorInt.color = color;
            spriteRenderer.sprite = sprite; //Cambio el sprite por el color adecuado
            foreach (var render in renderers)   //Cambio los rayos de electricidad por el color adecuado
            {
                render.material.SetTexture("_MainTex", texture);
            }
        }
    }
}
