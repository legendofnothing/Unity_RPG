using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    List<Image> spellList;

    private void Start() {
        spellList = new List<Image> { fireSpell, waterSpell, iceSpell, thunderSpell };
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
    }

    private void SetSpell(Image spells) {
        for (int i = 0; i < spellList.Count; i++) {
            if (spells == spellList[i]) {
                spellList[i].color = new Color(spellList[i].color.r, spellList[i].color.g, spellList[i].color.b, 1f);
            }

            else spellList[i].color = new Color(spellList[i].color.r, spellList[i].color.g, spellList[i].color.b, 0.5f);
        }
    }
}
