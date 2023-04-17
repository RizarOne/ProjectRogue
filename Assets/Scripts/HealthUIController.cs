using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUIController : MonoBehaviour
{
    public GameObject hearthContainer;

    private float fillValue;
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameManager.Health;
        fillValue = fillValue / GameManager.MaxHealth;
        hearthContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
