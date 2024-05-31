using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircleQuery : MonoBehaviour, IQuery {

    public SpatialGrid             targetGrid;
    public float                   radius  = 15f;

    public IEnumerable<IGridEntity> Query() {
        var r = radius * 0.5f;
        //posicion inicial --> esquina superior izquierda de la "caja"
        //posición final --> esquina inferior derecha de la "caja"
        //como funcion para filtrar le damos una que siempre devuelve true, para que no filtre nada.
        return targetGrid.Query(
                                transform.position + new Vector3(-r, 0, -r),
                                transform.position + new Vector3(r,  0, r),
                                x => Vector3.Distance(x, transform.position) < radius);
    }

    void OnDrawGizmos() {
        if (targetGrid == null) return;

        //Flatten the sphere we're going to draw
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, radius);
    }
}