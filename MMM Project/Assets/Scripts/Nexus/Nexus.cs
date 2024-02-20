using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField] private MissileStrategy [] missiles;
    [SerializeField] private GameObject mouseOverMissile;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int index = 0;
    [SerializeField] private bool haveMissile;
    public TrajectoryLine tl;
    [SerializeField] private Collider2D collider1, collider2;
    Camera cam;
    Vector2 force;
    public Vector2 minPower;
    public Vector2 maxPower;
    Vector3 startPoint;
    Vector3 endPoint;

    private void Awake() {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
    }
    void Start()
    {
        mouseOverMissile.SetActive(false);
        index = 0;
        missilePrefab = missiles[index].CreateMissile(transform);
        haveMissile = true;
        collider2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!haveMissile){
            missilePrefab = missiles[index].CreateMissile(transform);
            haveMissile = true;
        }
    }

    private void OnMouseOver() {
        if(haveMissile){
            mouseOverMissile.SetActive(true);
            if(Input.GetMouseButtonDown(0)){
                startPoint = transform.position;
                startPoint.z = 0;
            }
            if(Input.GetMouseButton(0)){
                Vector3 curreentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                curreentPoint.z = 0;
                missilePrefab.transform.position = curreentPoint;
                collider2.enabled = true;
                tl.RenderLine(startPoint, curreentPoint);
            }
            if(Input.GetMouseButtonUp(0)){
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 0;

                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                missilePrefab.rigidbody.AddForce(force * 1, ForceMode2D.Impulse);
                tl.EndLine();
                collider2.enabled = false;
                haveMissile = false;
            }
        }
    }

    private void OnMouseExit() {
        mouseOverMissile.SetActive(false);
    }
}
