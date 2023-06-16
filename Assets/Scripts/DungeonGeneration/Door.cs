using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    public GameObject doorCollider;

    private GameObject player;

    public Animator transition;
    public float transitionTime = 0f;


    //Oven trigger teleport/pelaajan liikutus "säätö"
    private float leftOffset = 19f;
    private float topOffset = 11f;
    private float bottomOffset = 9f;
    private float rightOffset = 37f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")


        {
            StartCoroutine(SwitchRoom());

            switch (doorType)
            {                            
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - bottomOffset);
                    break;

                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - leftOffset, transform.position.y);
                        break;

                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + rightOffset, transform.position.y);
                    break;

                case DoorType.top:                   
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + topOffset);
                    break;

            }
        }
    }

    IEnumerator SwitchRoom()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

}
