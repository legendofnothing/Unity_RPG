using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPausing;

    public GameObject playUI;
    public GameObject deathUI;
    public GameObject pauseUI;

    #region PlayUI

    [Space]
    //Handle HP Bar
    public GameObject HealthBar;
    public GameObject ManaBar;

    [Space]
    //Handle Skill Display
    public GameObject player;
    public Image fireSpell;
    public Image waterSpell;
    public Image iceSpell;
    public Image thunderSpell;

    [Space]
    //Handle Display Items
    public Text hpItem;
    public Text mnItem;

    [Space]
    public AnimationClip playerDieClip;
    public GameObject[] Buttons;

    [Space]
    //This is for fun
    public Text hintText;
    public string[] hints;

    List<Image> spellList;

    [Space]
    //Make sure this is in order im too lazy to fix it
    public Text[] spellCDs;

    [Space]
    public GameObject lowManaText;

    private void Start() {
        spellList = new List<Image> { fireSpell, waterSpell, iceSpell, thunderSpell };
        Time.timeScale = 1;

        deathUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    private void Update() {
        //Handle HP Bar
        var fillVal1 = PlayerManager.instance.currPlayerHP / PlayerManager.instance.maxPlayerHP;
        HealthBar.GetComponent<Slider>().value = fillVal1;

        //Handle MANA Bar
        var fillVal2 = PlayerManager.instance.currPlayerMN / PlayerManager.instance.maxPlayerMN;
        ManaBar.GetComponent<Slider>().value = fillVal2;

        //Handle Equipped Spell
        switch (player.GetComponent<PlayerAttack>()._weaponIndex) {
            case 0:
                SetSpell(fireSpell);
                break;
            case 1:
                SetSpell(waterSpell);
                break;
            case 2:
                SetSpell(iceSpell);
                break;
            case 3:
                SetSpell(thunderSpell);
                break;
        }

        //Handle counting items
        hpItem.text = "x" + PlayerManager.instance.pickupHP.ToString();
        mnItem.text = "x" + PlayerManager.instance.pickupMN.ToString();

        //Handle Death Screen
        if(PlayerManager.instance.currPlayerHP <= 0) {
            playUI.SetActive(false);
            deathUI.SetActive(true);
        }

        //Handle Pausing
        if (Input.GetKeyDown(KeyCode.Escape) && !isPausing) {
            playUI.SetActive(false);
            pauseUI.SetActive(true);

            Time.timeScale = 0;

            isPausing = true;

            var hintIndex = Random.Range(0, hints.Length);
            hintText.text = hints[hintIndex];
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPausing) {
            playUI.SetActive(true);
            pauseUI.SetActive(false);

            Time.timeScale = 1;

            isPausing = false;
        }

        //Handle Displaying Cooldown
        SetDelayTime(0);
        SetDelayTime(1);
        SetDelayTime(2);
        SetDelayTime(3);

        //Handle Displaying LowManaText
        if (player.GetComponent<PlayerAttack>().CanCastSpell() == true) {
            lowManaText.SetActive(false);
        }

        else lowManaText.SetActive(true);
    }

    private void SetSpell(Image spells) {
        for (int i = 0; i < spellList.Count; i++) {
            if (spells == spellList[i]) {
                spellList[i].color = new Color(spellList[i].color.r, spellList[i].color.g, spellList[i].color.b, 1f);
            }

            else spellList[i].color = new Color(spellList[i].color.r, spellList[i].color.g, spellList[i].color.b, 0.5f);
        }
    }

    IEnumerator InitDeathUI() {
        yield return new WaitForSeconds(playerDieClip.length);
        deathUI.SetActive(true);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() {
        //Do Code Here
    }

    public void Continue() {
        playUI.SetActive(true);
        pauseUI.SetActive(false);

        Time.timeScale = 1;

        isPausing = false;
    }

    private void SetDelayTime(int index) {
        spellCDs[index].text = player.GetComponent<SpellCooldown>().currDelay[index].ToString("0.0") + "s"; 

        if(player.GetComponent<SpellCooldown>().currDelay[index] <= 0) {
            spellCDs[index].text = "Ready";
        }
    }
    #endregion
}
