using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour
{
    private float _dirX = -1f;
    private float _currHP;
    
    private bool _canAttack;

    private bool _isHealing;
    private bool _canHeal = true;

    private bool _forWhomWhoPassedAway;

    private Animator anim;

    [Header("Boss Configs")]
    public float speed;
    public float maxHP;
    public float chargeAttackDelay;
    [Space]
    public TextMesh damageIndicator;
    public GameObject[] Doors;

    private void Start() {
        _currHP = maxHP;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (!_forWhomWhoPassedAway) {
            ChargeAttack();

            HealAttack();
        }

        damageIndicator.text = "HP: " + _currHP.ToString("0") + " / " + maxHP.ToString("0");

        if(_currHP <= 0) {
            _currHP = 0;
            _forWhomWhoPassedAway = true;
            StartCoroutine(Die());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && PlayerManager.instance.currPlayerHP > 0) {
            PlayerManager.instance.TakeDamage(20f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            _dirX *= -1f;
            _canAttack = false;

            if (gameObject.GetComponent<SpriteRenderer>().flipX == true) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            else gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Spells")) {
            var damage = Random.Range(10, 15);

            _currHP -= damage;
        }
    }

    //Attack Stuff
    private IEnumerator DelayAttack() {
        _canAttack = false;
        yield return new WaitForSeconds(chargeAttackDelay);
        _canAttack = true;
    }

    private void ChargeAttack() {
        if (_canAttack && !_isHealing) {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_dirX * speed, 0);
        }

        else if (!_canAttack) {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(DelayAttack());
        }
    }

    //Healing Stuff
    private void HealAttack() {
        if(_currHP < maxHP / 2 && _canHeal) {
            StartCoroutine(Heal());
            _canHeal = false;
        }
    }

    IEnumerator Heal() {
        _isHealing = true;
        _canHeal = false;

        _currHP += Random.Range(15, 25);

        yield return new WaitForSeconds(2.4f);

        _isHealing = false;
    }

    IEnumerator Die() {
        damageIndicator.text = "";

        anim.SetTrigger("Die");
        var clip = anim.runtimeAnimatorController.animationClips[1];

        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        yield return new WaitForSeconds(clip.length + 1.1f);

        for(int i = 0; i < Doors.Length; i++) {
            Doors[i].SetActive(false);
        }
    }
}
