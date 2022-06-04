using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private float _dirX = -1f;
    private bool _canAttack;

    public float speed;

    private void Update() {
        if (_canAttack) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_dirX * speed, 0);
        }

        else if (!_canAttack) {
            StartCoroutine(DelayAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerManager.instance.TakeDamage(20f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            _dirX *= -1f;
            _canAttack = false;
        }
    }

    private IEnumerator DelayAttack() {
        _canAttack = false;
        yield return new WaitForSeconds(1.2f);
        _canAttack = true;
    }
}
