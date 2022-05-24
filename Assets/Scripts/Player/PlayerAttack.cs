using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [HideInInspector] public int _weaponIndex;

    [Header("Spells Configs")]
    public GameObject fire;
    public GameObject water;
    public GameObject ice;
    public GameObject thunder;

    [Header("Attack Config")]
    public float fireSpeed;
    public float waterSpeed;
    public float iceSpeed;
    public float thunderSpeed;

    [Space]
    public float fireMana;
    public float waterMana;
    public float iceMana;
    public float thunderMana;
    
    [Space]
    //Internal Configs
    private bool _canAttack;
    public float attackDelay;
    [HideInInspector] public float currDelay;
    public GameObject attackPoint;

    //Animation
    private Animator _anim;

    private void Start() {
        _anim = GetComponent<Animator>();
    }

    private void Update() {
        SwitchWeapon();

        AttackInput();

        //Handle the delay
        if (!_canAttack) {
            currDelay -= Time.deltaTime;
        }

        if (currDelay <= 0f) {
            _canAttack = true;
            currDelay = -1f;
        }
    }

    private void SwitchWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _weaponIndex = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _weaponIndex = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            _weaponIndex = 2;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            _weaponIndex = 3;
        }
    }

    private void AttackInput() {

        if (Input.GetMouseButtonDown(0) && _canAttack && PlayerManager.instance.currPlayerMN > 0) {
            currDelay = attackDelay;
            _canAttack = false;

            _anim.SetTrigger("Attack");

            switch (_weaponIndex) {
                case 0:
                    PlayerManager.instance.ReduceMana(fireMana);
                    break;
                case 1:
                    PlayerManager.instance.ReduceMana(waterMana);
                    break;
                case 2:
                    PlayerManager.instance.ReduceMana(iceMana);
                    break;
                case 3:
                    PlayerManager.instance.ReduceMana(thunderMana);
                    break;  
            }
        }
    }

    public void CastSpell() {
        GameObject spelltocast = null;
        var projectileSpeed = 0f;

        switch (_weaponIndex) {
            case 0:
                spelltocast = fire;
                projectileSpeed = fireSpeed;
                break;

            case 1:
                spelltocast = water;
                projectileSpeed = waterSpeed;
                break;

            case 2:
                spelltocast = ice;
                projectileSpeed = iceSpeed;
                break;
            
            case 3:
                spelltocast = thunder;
                projectileSpeed = thunderSpeed;
                break;
        }

        GameObject spellInstance = Instantiate(spelltocast, attackPoint.transform.position, attackPoint.transform.rotation);
        spellInstance.GetComponent<Rigidbody2D>().velocity = attackPoint.transform.right * projectileSpeed;
    }
}
