using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Vector3 _offset;
    void Start()
    {
        _offset = transform.position - _target.transform.position;
    }
    void Update()
    {
        transform.position = _target.transform.position + _offset;
    }
}
