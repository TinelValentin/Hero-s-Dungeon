using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite almostFullHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;

    
    private int maxHealth;
    private int currentHealth;
    private Vector3 heartSize = new Vector3(0.35f, 0.35f, 0);

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("player");
        Player playerScript = playerObject.GetComponent<Player>();
        this.maxHealth = playerScript.maxHealth;
        this.currentHealth = playerScript.currentHealth;
        ShowHealth();

    }

    void CreateHeart(Sprite heartState,float xPosition)
    {
        GameObject NewObj = new GameObject();
        NewObj.name = "Heart";
        NewObj.transform.localScale = heartSize;

        Image fullHeart = NewObj.AddComponent<Image>();
        fullHeart.sprite = heartState;
        NewObj.GetComponent<RectTransform>().SetParent(this.transform);
        NewObj.transform.localPosition = new Vector3(-this.transform.localPosition.x / 1.1f + xPosition, this.transform.localPosition.y / 1.6f, 0.0f);
        NewObj.SetActive(true);
        
    }

    void ShowHealth()
    {
        float xPosition = 0;
        for(int i=0;i<currentHealth/4;i++)
        {
            CreateHeart(fullHeart, xPosition);
            xPosition += 25.0f;
        }
        
        switch (currentHealth % 4)
        {
            case 1:
                CreateHeart(quarterHeart, xPosition);
                xPosition += 25.0f;
                break;
            case 2:
                CreateHeart(halfHeart, xPosition);
                xPosition += 25.0f;
                break;
            case 3:
                CreateHeart(almostFullHeart, xPosition);
                xPosition += 25.0f;
                break;
            default:
                break;

        }

        for (int i = 0; i < (maxHealth-currentHealth)/4; i++)
        {
            CreateHeart(emptyHeart, xPosition);
            xPosition += 25.0f;
        }
    }

    void SetCurrentHealth(int health)
    {
        this.currentHealth = health;
        DeleteHearts();
        ShowHealth();
    }

    void DeleteHearts()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.name=="Heart")
            {
                Destroy(child.gameObject);
            }
            
        }
    }
}
