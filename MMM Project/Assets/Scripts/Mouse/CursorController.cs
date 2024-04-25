using UnityEngine;

public class CursorController : MonoBehaviour
///Esta clase se encarga de administrar todos los cursores, a los de los misiles les cambia el color segun el indice del misil
///tambien maneja el de "bloqueado" chequeando si esta activado alguno de los cursores de las torres y esta dentro del area prohibida para spawnear
///Y despues cada ScriptableObject de los poderes o habilidades tiene un sprite, material y escala que se envia a esta clase para cambiar el SpriteRenderer
{
    public static CursorController Instance { get; private set; }
    Vector2 currentPosition;    //Donde se guarda la posicion del mouse
    private SpriteRenderer spriteRenderer;
    [Header("MainCursor")]  //Las caracteristicas del cursor principal (el que esta activo por default)
    [SerializeField] private Sprite mainSprite;
    [SerializeField] private Material mainMaterial;
    [SerializeField] private Quaternion mainTransformRotation;
    [SerializeField] private Vector3 mainTransformScale;

    [Header("Block Cursor")]    //El cursor de bloqueado y las variables para manejar cuando tiene que mostrarse o no
    [SerializeField] private GameObject blockCursor;
    [SerializeField] private Nexus nexus;
    [SerializeField] private float distanceFromNexus;
    private int  towerIndex;

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
        nexus = FindObjectOfType<Nexus>();  //Busco al nexo


        Cursor.visible = false; //Desactivo el mouse
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        RestoreCursor();    //Activo el MainCursor
    }

    // Update is called once per frame
    void Update()   //Guardo la posicion del mouse y tambien pregunto si esta activado algun cursor de la torre y si es asi si
    //Debe mostrar el cursor de bloqueado o no
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentPosition;
        UpdateBlockCursor();
    }
    private void UpdateBlockCursor(){
        blockCursor.SetActive(towerIndex > 0 && CheckDistanceFromNexus());
        blockCursor.transform.position = currentPosition;
    }
    
    public void ChangeTowerIndex(int index){    //Cambio el indice de la torre para tenerlo
        towerIndex = index;
    }
    private bool CheckDistanceFromNexus(){  //Chequero la distancia entre el mouse y el nexo, el valor "distanceFromNexus" debe ser el mismo 
    //que en el towerController
        return Vector2.Distance(currentPosition, nexus.transform.position) < distanceFromNexus || currentPosition.y < -3f || currentPosition.y > 4.25f;
    }
    
    public void SetCursor(Sprite sprite, Material material, Vector3 scale){ //Establezco que imagen,material y escala debo poner en el SpriteRenderer
        spriteRenderer.sprite = sprite;
        spriteRenderer.material = material;
        transform.localScale = scale;
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }
    public void RestoreCursor(){    //Establezco el SpriteRenderer para que muestre el MainCursor
        spriteRenderer.sprite = mainSprite;
        spriteRenderer.material = mainMaterial;
        transform.localRotation = mainTransformRotation;
        transform.localScale = mainTransformScale;
    }
 
}
