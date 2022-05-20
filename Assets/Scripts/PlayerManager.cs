using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager pm { get; private set; }

    private void Awake() {

        if (pm != null && pm != this) {
            Destroy(this);
        }
        else {
            pm = this;
        }
    }


}
