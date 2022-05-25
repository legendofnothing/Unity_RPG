using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour
{
    public string[] dialouges;
    public GameObject text;

    private bool _isInRange;
    private int _textIndex;

    private void Update() {
        if (_isInRange) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (text.GetComponent<TextMesh>().text == dialouges[_textIndex]) {
                    NextDialouge();
                }

                else {
                    StopAllCoroutines();
                    text.GetComponent<TextMesh>().text = dialouges[_textIndex];
                }
            }
        }

        else {
            text.GetComponent<TextMesh>().text = "Press E";
            _textIndex = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Player") {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            _isInRange = false;
        }
    }

    private void StartDialouge() {
        _textIndex = 0;
        StartCoroutine(TypeWriterEffect());
    }

    private void NextDialouge() {
        if (_textIndex < dialouges.Length - 1) {
            _textIndex++;
            text.GetComponent<TextMesh>().text = "";
            StartCoroutine(TypeWriterEffect());
        }

        else text.SetActive(false);
    }

    private IEnumerator TypeWriterEffect() {
        foreach (char c in dialouges[_textIndex].ToCharArray()) {
            text.GetComponent<TextMesh>().text += c;
            yield return new WaitForSeconds(0.06f);
        }
    }
}
