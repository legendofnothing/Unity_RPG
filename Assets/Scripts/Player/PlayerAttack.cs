using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [HideInInspector] public int _weaponIndex;
    [HideInInspector] public GameObject spellToCast;

    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float projectileManaCost;
    [HideInInspector] public float projectileDamage;
    [HideInInspector] public string sfxName;

    public GameObject[] Spells;

    [Space]
    //Internal Configs
    private bool _canAttack;
    public float attackDelay;
    [HideInInspector] public float currDelay;
    public GameObject attackPoint;

    //Animation
    private Animator _anim;
    private SpellCooldown spellCD;

    private void Start() {
        _anim = GetComponent<Animator>();
        spellCD = GetComponent<SpellCooldown>();

        ActivateSpell(0);
    }

    private void Update() {
        SwitchWeapon();

        AttackInput();
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

        if (Input.GetMouseButtonDown(0) && PlayerManager.instance.currPlayerMN > 0 && CanCastSpell()) {
            if (spellCD.canShoot[_weaponIndex]) {
                spellCD.canShoot[_weaponIndex] = false;
                spellCD.currDelay[_weaponIndex] = spellCD.maxDelay[_weaponIndex];

                _anim.SetTrigger("Attack");

                PlayerManager.instance.ReduceMana(projectileManaCost);

                AudioManager.manager.PlaySFX(sfxName);
            }
        }
    }

    public void CastSpell() {
        GameObject spellInstance = Instantiate(spellToCast, attackPoint.transform.position, attackPoint.transform.rotation);
        spellInstance.GetComponent<Rigidbody2D>().velocity = attackPoint.transform.right * projectileSpeed;
    }

    public bool CanCastSpell() {
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
