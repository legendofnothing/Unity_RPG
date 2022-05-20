using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    //HP Display
    public TextMeshPro eHPDisplay;

    //HP Stuff
    [HideInInspector] public float currEnemyHP;
    [SerializeField] private float _maxEnemyHP;

    //Handle Flipping Stuff
    private Vector3 _lastPos;
    private Vector3 _velocity;
    private Vector3 _velocityActual;

    //Animation Stuff
    private Animator _anime;

    private void Start() {
        currEnemyHP = _maxEnemyHP;
        _anime = this.GetComponent<Animator>();
    }

    private void Update() {
        
        //Display HP
        eHPDisplay.text = "HP: " + currEnemyHP.ToString("0") + " / " + _maxEnemyHP.ToString("0");

        if (currEnemyHP <= 0) {
            _anime.SetBool("isDie", true);
        }

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

    }

    public void DamageEnemy(float dmg) {

        if (currEnemyHP > 0) {

            currEnemyHP -= dmg;
        }

        _anime.SetTrigger("isBeingHit");
    }

    private IEnumerator Die() {
        _anime.SetTrigger("isDie");

        yield return new WaitForSeconds(0.7631579f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerManager.instance.TakeDamage(10f);
        }
    }
}
