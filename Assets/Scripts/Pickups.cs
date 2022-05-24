using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    //Set Index in Inspector 
    [Header("Set Index [0,1] -> [HP, MN]")]
    [SerializeField] private int pickUpIndex;

    //Pickup Configs
    [Header("Pickup Configs")]
    [SerializeField] private float _hpAdd;
    [SerializeField] private float _mnAdd;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            switch (pickUpIndex) {
                case 0:
                    if (PlayerManager.instance.currPlayerHP < 100) {
                        PlayerManager.instance.currPlayerHP += _hpAdd;

                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (PlayerManager.instance.currPlayerMN < 100) {
                        PlayerManager.instance.currPlayerMN += _mnAdd;

                        Destroy(gameObject);
                    }
                    break;
                default:
                    Debug.Log("Check pickup index at: " + gameObject.name);
                    break;
            }
        }
    }
}
