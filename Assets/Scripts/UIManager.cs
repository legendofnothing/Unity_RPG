using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject playUI;
    public GameObject deathUI;

    #region PlayUI

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
    public TextMeshProUGUI hpItem;
    public TextMeshProUGUI mnItem;

    [Space]
    public AnimationClip playerDieClip;
    public GameObject[] Buttons;
 
    List<Image> spellList;

    private void Start() {
        spellList = new List<Image> { fireSpell, waterSpell, iceSpell, thunderSpell };
        deathUI.SetActive(false);
        Time.timeScale = 1;
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

        if(PlayerManager.instance.currPlayerHP <= 0) {
            playUI.SetActive(false);

            StartCoroutine(InitDeathUI());
        }
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

        //Set Buttons to false on first on DeathUI
        for (int i = 0; i < Buttons.Length; i++) {
            Buttons[i].SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);

        //Then enable the buttons

        for (int i = 0; i < Buttons.Length; i++) {
            yield return new WaitForSeconds(1f);

            Buttons[i].SetActive(true);
        }
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
