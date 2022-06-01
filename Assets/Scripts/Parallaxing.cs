using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public float parallaxSpeed;

    private Vector3 _lastPos;

    private void Start() {
        _lastPos = Camera.main.transform.position;
    }

    private void Update() {
        var camMoveThisFrame = _lastPos.x - Camera.main.transform.position.x;
        var movementInX = camMoveThisFrame * parallaxSpeed;

        var parallaxPos = new Vector3(movementInX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, parallaxPos, 1);
    }
}
