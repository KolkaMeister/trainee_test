using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] public Rigidbody rb;

    protected virtual void OnCollisionEnter(Collision collision)
    {
       var _interface = collision.gameObject.GetComponent<ICanTakeDamage>();
        if (_interface!=null)
        {
            _interface.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
