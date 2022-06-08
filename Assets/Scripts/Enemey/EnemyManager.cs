using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    //HP Display
    public GameObject eHPDisplay;

    //HP Stuff
    [HideInInspector] public float currEnemyHP;
    [SerializeField] private float _maxEnemyHP;

    //Animation Stuff
    private Animator _anime;

    //State
    [HideInInspector] public bool canPatrol;

    //Variable that make stuff play once
    private bool _isDone;

    private bool _hasDied;

    private void Start() {
        currEnemyHP = _maxEnemyHP;
        _anime = this.GetComponent<Animator>();
        canPatrol = true;
    }

    private void Update() {
        
        //Display HP
        eHPDisplay.GetComponent<TextMesh>().text = "HP: " + currEnemyHP.ToString("0") + " / " + _maxEnemyHP.ToString("0");

        //Handle Flipping
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0f) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            eHPDisplay.transform.eulerAngles = new Vector3(0, 0, 0);
            
        }

        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            eHPDisplay.transform.eulerAngles = new Vector3(0, 0, 0);

        }

        else {
            transform.eulerAngles = transform.eulerAngles;
        }

        if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0f || gameObject.GetComponent<Rigidbody2D>().velocity.x < 0f) {
            _anime.SetTrigger("isWalking");
        }

        else _anime.SetTrigger("isIdle");

        if (currEnemyHP <= 0) {
            currEnemyHP = 0;
            canPatrol = false;

            _hasDied = true;

            if (!_isDone) {
                _anime.SetTrigger("isDie");
                _isDone = true;
                AudioManager.manager.PlaySFX("EnemyDie");
            }
        }

    }

    public void DamageEnemy(float dmg) {

        if (currEnemyHP > 0) {

            currEnemyHP -= dmg;
        }

        _anime.SetTrigger("isBeingHit");
        AudioManager.manager.PlaySFX("EnemyHit");
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player") && !_hasDied) {
            PlayerManager.instance.TakeDamage(10f);
        }
    }
}
