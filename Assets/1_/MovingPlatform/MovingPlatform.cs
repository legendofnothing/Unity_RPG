using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool _isActivated;
    private bool _isMoving;
    private bool _isPressed;

    public int weaponIndexHit;
    public float speed;

    private void Update() {
        if (_isActivated) {
            if (!_isPressed) {
                if (Input.GetKeyDown(KeyCode.A)) {
                    StartCoroutine(MovePlatform(Vector2.left, speed * 10));
                    _isPressed = true;
                }

                else if (Input.GetKeyDown(KeyCode.D)) {
                    StartCoroutine(MovePlatform(Vector2.right, speed * 10));
                    _isPressed = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<AttackManager>().weaponIndex == weaponIndexHit && !_isMoving) {
            _isActivated = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.transform.SetParent(gameObject.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.transform.parent = null;
        }
    }

    private IEnumerator MovePlatform(Vector2 direction, float speed) {
        _isMoving = true;

        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.8f);

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        _isMoving = false;
        _isActivated = false;
        _isPressed = false;
    }
}


