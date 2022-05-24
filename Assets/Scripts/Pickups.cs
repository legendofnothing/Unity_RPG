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
                    PlayerManager.instance.pickupHP += 1;
                    Destroy(gameObject);
                    break;
                case 1:
                    PlayerManager.instance.pickupMN += 1;
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Check pickup index at: " + gameObject.name);
                    break;
            }
        }
    }
}
