using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    GameObject mobs;

    // Start is called before the first frame update
    void Start()
    {
        mobs = GameObject.Find("RoomMobs");
        SetActiveAllChildren(mobs.transform, false);
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
    
            SetActiveAllChildren(child, value);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SetActiveAllChildren(mobs.transform, true);
        }
    }
}
