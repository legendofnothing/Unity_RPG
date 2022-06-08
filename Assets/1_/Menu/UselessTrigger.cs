using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessTrigger : MonoBehaviour
{
    public GameObject[] platforms;
    private bool _hasChanged;

    private void Start() {
        ChangeColor(27, 130, 255);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(197f / 255f, 47f / 255f, 56f / 255f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_hasChanged) {
            _hasChanged = false;
            ChangeColor(27, 130, 255);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(197f / 255f, 47f / 255f, 56f / 255f);
        }

        else {
            _hasChanged = true;
            ChangeColor(197, 47, 56);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(27f / 255f, 130f / 255f, 255f / 255f);
        }
    }

    void ChangeColor(float r, float g, float b) {
        for(int i = 0; i < platforms.Length; i++) {
            platforms[i].GetComponent<SpriteRenderer>().color = new Color(r/255f, g/255f, b/255f);
        }
    }
}
