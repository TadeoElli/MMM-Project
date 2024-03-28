using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
///Esta clase se encarga de administrar todos los cursores, por eso, todos los cursores deben estar dentro de este objeto, para que 
///se muevan junto con el mouse
{
    public static CursorController Instance { get; private set; }
    Vector2 currentPosition;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite mainSprite;
    [SerializeField] private Material mainMaterial;
    [SerializeField] private Quaternion mainTransformRotation;
    [SerializeField] private Vector3 mainTransformScale;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Cursor.visible = false;
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        RestoreCursor();
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentPosition;
    }


    public void SetCursor(Sprite sprite, Material material, Vector3 scale){
        spriteRenderer.sprite = sprite;
        spriteRenderer.material = material;
        transform.localScale = scale;
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }
    public void RestoreCursor(){
        spriteRenderer.sprite = mainSprite;
        spriteRenderer.material = mainMaterial;
        transform.localRotation = mainTransformRotation;
        transform.localScale = mainTransformScale;
    }
 
}
