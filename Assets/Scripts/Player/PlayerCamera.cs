using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _yOffSet;

    private void Update() {
        transform.position = new Vector3(_player.position.x, _player.position.y + _yOffSet, transform.position.z);
    }
}
