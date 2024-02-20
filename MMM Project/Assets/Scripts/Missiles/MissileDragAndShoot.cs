using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDragAndShoot : MonoBehaviour
{
    public float power = 10;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;
    public bool onNexus = true;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public TrajectoryLine tl;
    void Start()
    {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
    }


    private void OnMouseOver() {
        if(onNexus){
            if(Input.GetMouseButtonDown(0)){
                startPoint = transform.position;
                startPoint.z = 0;
            }
        }
        if(Input.GetMouseButton(0)){
            Vector3 curreentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            curreentPoint.z = 0;
            transform.position = curreentPoint;
            tl.RenderLine(startPoint, curreentPoint);
        }

        if(Input.GetMouseButtonUp(0)){
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            tl.EndLine();
            onNexus = false;
        }
        
    }
}
