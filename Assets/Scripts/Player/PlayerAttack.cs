using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool _canAttack1 = true;
    private bool _canAttack2 = true;

    //Delay Stuff
    [Header("Delay Attack")]
    [SerializeField] private float _attack1Delay;
    [SerializeField] private float _attack2Delay;

    [HideInInspector] public float currDelay1 = 0f;
    [HideInInspector] public float currDelay2 = 0f;

    [Header("Config Stuff")]
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Camera _playerCamera;

    [Header("Attack Config")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attack1Dmg;
    [SerializeField] private float _attack2Dmg;


    //Handle Animations
    private Animator _anime;

    private void Start() {
        _anime = GetComponent<Animator>();
    }

    private void Update() {
        AttackInput();

        //Handle the delay
        //For Attack 1
        if (!_canAttack1) {
            currDelay1 -= Time.deltaTime;
        }

        if (currDelay1 <= 0f) {
            _canAttack1 = true;
            currDelay1 = -1f;
        }

        //For Attack 2
        if (!_canAttack2) {
            currDelay2 -= Time.deltaTime;
        }

        if (currDelay2 <= 0f) {
            _canAttack2 = true;
            currDelay2 = -1f;
        }
    }

    private void AttackInput() {
        //Order:
        //Handle Attack Delay
        //Call Attack Func

        //Attack 1: Simple Punch
        if (Input.GetMouseButtonDown(0) && _canAttack1) {
            _canAttack1 = false;
            currDelay1 = _attack1Delay;

            //Attack
            Attack(_attackRange, _attackPoint, _attack1Dmg);

            _anime.SetTrigger("isAttack2");

            PlayerManager.instance.ReduceMana(4.8f);
        }

        if (Input.GetMouseButtonDown(1) && _canAttack2) {
            _canAttack2 = false;
            currDelay2 = _attack2Delay;

            //Attack
            Attack(_attackRange, _attackPoint, _attack2Dmg);

            _anime.SetTrigger("isAttack1");

            PlayerManager.instance.ReduceMana(5.6f);
        }
    }

    //Attack Func
    private void Attack(float atackRange, Transform attackPoint, float dmg) {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, atackRange, _enemyLayer);

        foreach (Collider2D hit in hitEnemy) {
            hit.GetComponent<EnemyManager>().DamageEnemy(dmg);

        }
    }
}
