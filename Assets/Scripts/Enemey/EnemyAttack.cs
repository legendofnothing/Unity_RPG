using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour 
{
    private float _distance;
    private Rigidbody2D rb;

    public Transform player;

    [Space]
    public float distanceToMove;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        CalculateDistance();

        if (_distance < distanceToMove) {

            var direction = (player.position - transform.position).normalized;

            rb.MovePosition(player.position * 0.8f);
        }
    }

    private void CalculateDistance() {
        _distance = Vector3.Distance(transform.position, player.position);
    }
}