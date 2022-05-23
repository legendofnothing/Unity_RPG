using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Singleton
    public static PlayerManager instance { get; private set; }

    [Header("Player Configs")]
    //HP
    [HideInInspector] public float currPlayerHP;
    public float maxPlayerHP;

    //Mana
    [HideInInspector] public float currPlayerMN;
    public float maxPlayerMN;
    [HideInInspector] public bool canGenerate;

    private Animator _anime;

    //Singleton Stuff
    private void Awake() {

        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    private void Start() {
        currPlayerHP = maxPlayerHP;
        currPlayerMN = maxPlayerMN;

        StartCoroutine(GeneratingMana());

        _anime = GetComponent<Animator>();
    }

    private void Update() {

        //Making sure these don't go over or below their set limit
        if (currPlayerHP > maxPlayerHP) {
            currPlayerHP = maxPlayerHP;
        }

        if (currPlayerMN > maxPlayerMN) {
            currPlayerMN = maxPlayerMN;
        }

        if (currPlayerHP <= 0) {
            currPlayerHP = 0;
        }

        if (currPlayerMN <= 0) {
            currPlayerMN = 0;
        }

    }

    //Generate x ammount of Mana / s, set in attacks sets.
    private IEnumerator GeneratingMana() {
        while (true) {
            if (currPlayerMN < 100) {
                currPlayerMN += 2.8f;
                yield return new WaitForSeconds(1f);
            }

            else {
                yield return null;
            }
        }
    }

    //Functions that will be used outside of this script
    public void ReduceMana(float mana) {
        currPlayerMN -= mana;
    }

    public void TakeDamage(float dmg) {
        currPlayerHP -= dmg;
        _anime.SetTrigger("isHit");
    }
}
