using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int coins;

    private void Start()
    {
        coins = 0;
        SetCurrency(0);
    }

    private void SetCurrency(int coins)
    {
        this.coins = coins;
        ShowCurrency();
    }

    public void AddCurrency(int coins)
    {
        this.coins += coins;
        ShowCurrency();
    }

    private void ShowCurrency()
    {
        GameObject textBox = GameObject.Find("Coins");
        textBox.GetComponent<UnityEngine.UI.Text>().text = this.coins.ToString();
    }
}
