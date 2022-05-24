using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void Start() {
        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            collision.GetComponent<EnemyManager>().DamageEnemy(_damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
