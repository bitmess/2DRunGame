using UnityEngine;
using System.Collections;

public class TrashCollect : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}
