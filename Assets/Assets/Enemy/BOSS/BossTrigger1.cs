using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger1 : MonoBehaviour
{
    public GameObject[] Doors;
    public GameObject Boss;

    public GameObject BackgroundAudio;
    public AudioClip bossTheme;
    public AudioClip bossScream;

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

            StartCoroutine(StartBoss());
        }
    }

    IEnumerator StartBoss() {
        Boss.SetActive(true);
        Boss.GetComponent<BossBehaviour>().enabled = false;

        BackgroundAudio.GetComponent<AudioSource>().clip = bossScream;
        BackgroundAudio.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(bossScream.length);

        BackgroundAudio.GetComponent<AudioSource>().clip = bossTheme;
        BackgroundAudio.GetComponent<AudioSource>().Play();

        Boss.GetComponent<BossBehaviour>().enabled = true;

        Destroy(gameObject);
    }
}
