using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private float _damage;

    [HideInInspector] public int weaponIndex;

    private GameObject player;

    private void Start() {
        player = GameObject.Find("Player");

        Destroy(gameObject, 8f);
    }

    private void Update() {
        _damage = player.GetComponent<PlayerAttack>().projectileDamage;
        weaponIndex = player.GetComponent<PlayerAttack>()._weaponIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            collision.GetComponent<EnemyManager>().DamageEnemy(_damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
