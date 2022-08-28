using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSpawn : MonoBehaviour
{
    public int opening;
    //1 up
    //2 right
    //3 left
    //4 down
    private int randomNumber;
    private roomTemplates template;
    private bool spawned = false;

    private void Start()
    {

        template = GameObject.FindGameObjectWithTag("Rooms").GetComponent < roomTemplates > ();
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            switch (opening)
            {
                case 1:
                    randomNumber = Random.Range(0, template.upRoom.Length);
                    Instantiate(template.upRoom[randomNumber], transform.position, template.upRoom[randomNumber].transform.rotation);
                    break;
                case 2:
                    randomNumber = Random.Range(0, template.leftRoom.Length);
                    Instantiate(template.leftRoom[randomNumber], transform.position, template.leftRoom[randomNumber].transform.rotation);
                    break;
                case 3:
                    randomNumber = Random.Range(0, template.rightRoom.Length);
                    Instantiate(template.rightRoom[randomNumber], transform.position, template.rightRoom[randomNumber].transform.rotation);
                    break;
                case 4:
                    randomNumber = Random.Range(0, template.downRoom.Length);
                    Instantiate(template.downRoom[randomNumber], transform.position, template.downRoom[randomNumber].transform.rotation);
                    break;
                default:
                    break;
            }
            spawned = true;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("spawnPoint")&&collision.GetComponent<roomSpawn>().spawned==true)
        {
            Destroy(gameObject);
        }
    }
}
