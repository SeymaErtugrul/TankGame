using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMenuScript : MonoBehaviour
{
    public int coin;

    public Button upgradeButton;

    public TextMeshProUGUI textAttackStrength;
    public TextMeshProUGUI textAttackSpeed;
    public TextMeshProUGUI textDefense;

    private int attackSpeed = 10;

    private int attackStrength = 10;

    private int defense = 10;

    void Start()
    {
        upgradeButton.interactable = false;
 
    }



    private void Update()
    {
        coin = PlayerPrefs.GetInt("coins", coin);
        if (coin >= 10)
        {
            upgradeButton.interactable = true;

        }
        else
        {
            upgradeButton.interactable = false;
        }
    }
    public void butonAttackSpeedUpgrade()
    {
        coin -= 10;
        PlayerPrefs.SetInt("coins", coin);
        PlayerPrefs.Save();
        attackSpeed += 10;
       textAttackSpeed.text = attackSpeed.ToString();

    }

  public void buttonDefenseUpgrade()
    {
        coin -= 10;
        PlayerPrefs.SetInt("coins", coin);
        PlayerPrefs.Save();
        defense += 10;
        textDefense.text = defense.ToString();
    }

    public void buttonAttackStrengthUpgrade()
    {
        coin -= 10;
        PlayerPrefs.SetInt("coins", coin);
        PlayerPrefs.Save();
        attackStrength += 10;
        textAttackStrength.text = attackSpeed.ToString();
    }
}
