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

                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * _enemySpeed;
            }
        }
    }
}
