using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Config Stuff")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player;
    private Animator anim;

    [Header("Attack Config")]
    [SerializeField] private float _minDistance;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _attackCooldown;

    private bool _isCoolingDown;

    private Rigidbody2D _rb;
    private Rigidbody2D rb {
        get {
            if (!_rb) _rb = GetComponent<Rigidbody2D>();
            return _rb;
        }
    }

    private void Start() {
        anim = this.GetComponent<Animator>();
    }

    private void Update() {
        EnemyLogic();
    }

    private void EnemyLogic() {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 4f) {
            if (distance > _minDistance) {
                var direction = (player.position - transform.position).normalized;

                rb.velocity = new Vector2(direction.x, direction.y) * _enemySpeed;

                anim.SetBool("isAttacking", false);
            }

            else if (_minDistance > distance) {
                anim.SetBool("isAttacking", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerManager.instance.TakeDamage(2f);
        }
    }
}
