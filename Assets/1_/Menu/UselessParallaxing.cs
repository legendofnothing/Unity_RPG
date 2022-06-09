using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessParallaxing : MonoBehaviour
{
    public float parallaxSpeed;

    private Vector3 _lastPos;

    void Start() {
        _lastPos = transform.position;
    }

    void Update() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var parallaxedPos = new Vector3(_lastPos.x + (mousePosition.x * parallaxSpeed), _lastPos.y + (mousePosition.y * parallaxSpeed), 0);

        transform.position = Vector3.Lerp(transform.position, parallaxedPos, 1);
    }
}
