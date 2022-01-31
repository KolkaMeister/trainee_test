using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeChacker : MonoBehaviour
{
    [SerializeField] private Collider range;
    [SerializeField] private LayerMask checkLayer;

    private GameObject target;

    public LayerMask CheckLayer => checkLayer;
    public GameObject Target => target;


    private void OnTriggerEnter(Collider other)
    {
       var isInRange = other.gameObject.IsInLayer(checkLayer);
        if (isInRange)
            target = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
       var isInRange = !other.gameObject.IsInLayer(checkLayer);
        if (!isInRange)
            target = null;
    }

}
