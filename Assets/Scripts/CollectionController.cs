using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;

    public string description;

    public Sprite itemImage;

}

public class CollectionController : MonoBehaviour
{
    public Item item;

    public float healthChange;

    public float moveSpeedChange;

    public float attackSpeedChange;

    public float bulletSizeChange;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        var collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //PlayerMovement.collectedAmount ++;
            GameManager.HealPlayer(healthChange);
            GameManager.MoveSpeedChange(moveSpeedChange);
            GameManager.FireRateChange(attackSpeedChange);
            GameManager.BulletSizeChange(bulletSizeChange);
            //GameManager.instance.UpdateCollectedItems(this);
            Destroy(gameObject);
        }
    }
}
