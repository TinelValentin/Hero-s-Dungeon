using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Image healthSlider;
    static CanvasGroup healthBarGroup;
    static float maxHealth;
    static float health;
    static float healthPerPhase;
    static readonly int numberOfPhases = 2;
    
    static public void ShowHealthBar(float maxHealth, float health)
    {
        healthBarGroup.alpha = 1f;
        healthBarGroup.blocksRaycasts = true;
        BossHealthBar.maxHealth = maxHealth;
        BossHealthBar.health = health;
        BossHealthBar.healthPerPhase = maxHealth / numberOfPhases;
    }

    static public void HideHealthBar()
    {
        healthBarGroup.alpha = 0f;
        healthBarGroup.blocksRaycasts = false;
    }

    static public void UpdateHealth(float newHealth)
    {
        health = newHealth;
    }

    void Start()
    {
        healthSlider = GameObject.Find("SliderFirst").GetComponent<Image>();
        healthBarGroup = GameObject.Find("BossHealthBarHolder").GetComponent<CanvasGroup>();
        HideHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.fillAmount = (health-healthPerPhase)/(maxHealth-healthPerPhase);
        if(healthSlider.fillAmount==0)
        {
            healthSlider = GameObject.Find("SliderSecond").GetComponent<Image>();
            maxHealth -= healthPerPhase;
            healthPerPhase = 0;
        }
    }
}
