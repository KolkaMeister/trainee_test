using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 difference;

    private void Start()
    {
        difference = transform.position-target.position;
    }

    private void Update()
    {
        transform.position = target.position + difference;
    }
}
