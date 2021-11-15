using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    void LateUpdate()
    {
        Vector3 newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //follows player
        transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0);
    }
}