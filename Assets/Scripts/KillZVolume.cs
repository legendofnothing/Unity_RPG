using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZVolume : MonoBehaviour
{
    public GameObject playerCamera;

    private void OnTriggerEnter2D(Collider2D collision) {
        playerCamera.GetComponent<PlayerCamera>().enabled = false;
        PlayerManager.instance.currPlayerHP = 0;
    }
}
