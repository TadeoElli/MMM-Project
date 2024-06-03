using System.Collections.Generic;
using UnityEngine;
public interface IQuery {

    IEnumerable<IGridEntity> Query(Transform origin);

}
