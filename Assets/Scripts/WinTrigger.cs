using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private void OnTriggerEnter2D(Collider2D collision) {
        UI.GetComponent<UIManager>().StartWin();

        collision.GetComponent<PlayerController>().enabled = false;
    }
}
