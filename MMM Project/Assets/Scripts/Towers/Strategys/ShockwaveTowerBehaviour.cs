using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Shockwave", order = 1)]
public class ShockwaveTowerBehaviour : TowerStrategy
{
    [SerializeField] private float force;
    public override void CreateTower(Vector2 origin){
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
    }
    public override void SpecialBehaviour(GameObject prefab, GameObject other){

    }

    public override bool ColliderBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Enemy")){
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
            if(rb2D != null){
                Vector2 direction = other.transform.position - prefab.transform.position;
                float distance = 1 + direction.magnitude;
                float finalForce = force / distance;
                rb2D.AddForce(direction * finalForce);
                //Debug.Log("Reste");
            }
            return true;

        }
        return false;
    }

    public override void DestroyTower(GameObject prefab){
        
    }

    
}
