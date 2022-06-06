using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger1 : MonoBehaviour
{
    public GameObject[] Doors;
    public GameObject Boss;

    private void Start() {
        for(int i = 0; i < Doors.Length; i++) {
            Doors[i].SetActive(false);
        }

        Boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            for (int i = 0; i < Doors.Length; i++) {
                Doors[i].SetActive(true);
            }

            Boss.SetActive(true);

            Destroy(gameObject);
        }
    }
}
