using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Enemey Patrol Configs")]
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float _enemySpeed;

    private int _currentWaypointIndex;

    private void FixedUpdate() {
        if (gameObject.GetComponent<EnemyManager>().canPatrol) {
            Patrol();
        }
    }

    private void Patrol() {
        //Handle the patrolling
        Transform destination = _wayPoints[_currentWaypointIndex];

        //Checking distance from the enemy to the waypoints
        /*How it works
         *It will check distance from enemy to all waypoints. If a waypoint is close enough to the enemy
         *then consider the enemy to be at that waypoint, and then switch waypoints. Else keep moving to
         *that waypoint. uwu
         */

        //Threshold to change is 0.2 cuz floats cant be absolute 0 anyways
        if (Vector3.Distance(transform.position, destination.position) < 0.2f) {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
        }

        else {
            var direction = (destination.position - transform.position).normalized;

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * _enemySpeed;
        }
    }

}
