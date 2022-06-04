using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.SetBool("isSleep", true);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        anim.SetBool("isSleep", false);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        anim.SetBool("isSleep", true);
    }
}

