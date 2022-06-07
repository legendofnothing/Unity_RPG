using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [HideInInspector] public int _weaponIndex;
    [HideInInspector] public GameObject spellToCast;

    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float projectileManaCost;
    [HideInInspector] public float projectileDamage;

    public GameObject[] Spells;

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
        ActivateSpell(0);
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
            ActivateSpell(_weaponIndex);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _weaponIndex = 1;
            ActivateSpell(_weaponIndex);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            _weaponIndex = 2;
            ActivateSpell(_weaponIndex);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            _weaponIndex = 3;
            ActivateSpell(_weaponIndex);
        }
    }

    private void AttackInput() {

        if (Input.GetMouseButtonDown(0) && _canAttack && PlayerManager.instance.currPlayerMN > 0 && CanCastSpell()) {
            currDelay = attackDelay;
            _canAttack = false;

            _anim.SetTrigger("Attack");

            PlayerManager.instance.ReduceMana(projectileManaCost);
        }
    }

    public void CastSpell() {
        GameObject spellInstance = Instantiate(spellToCast, attackPoint.transform.position, attackPoint.transform.rotation);
        spellInstance.GetComponent<Rigidbody2D>().velocity = attackPoint.transform.right * projectileSpeed;
    }

    private bool CanCastSpell() {
        if (PlayerManager.instance.currPlayerMN - projectileManaCost <= 0) {
            return false;
        }

        else return true;
    }

    private void ActivateSpell(int index) {
        for(int i = 0; i < Spells.Length; i++) {
            if (Spells[index] == Spells[i]) {
                Spells[i].SetActive(true);
            }

            else Spells[i].SetActive(false);    
        }
    }
}
