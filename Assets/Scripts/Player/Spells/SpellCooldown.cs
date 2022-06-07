using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Good luck understanding this
//Referenced back to PlayerAttack class
public class SpellCooldown : MonoBehaviour {
    public float fireSpellCD;
    public float waterSpellCD;
    public float iceSpellCD;
    public float thunderSpellCD;

    private PlayerAttack playerAttack;

    [Space]
    [HideInInspector] public bool[] canShoot = { true, true, true, true };
    [HideInInspector] public float[] currDelay = {0,0,0,0};
    [HideInInspector] public List<float> maxDelay;

    private void Start() {
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        maxDelay = new List<float> { fireSpellCD, waterSpellCD, iceSpellCD, thunderSpellCD };
    }

    private void Update() {
        Delay(0);
        Delay(1);
        Delay(2);
        Delay(3);
    }

    private void Delay(int spellIndex) {
        if (!canShoot[spellIndex]) {
            currDelay[spellIndex] -= Time.deltaTime;
        }

        if(currDelay[spellIndex] <= 0) {
            CanShoot(spellIndex);

            currDelay[spellIndex] = 0;
        }
    }

    private void CanShoot(int spellIndex) {
        canShoot[spellIndex] = true;
    }
}
