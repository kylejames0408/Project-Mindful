using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeVisibilityManager : MonoBehaviour
{
    [SerializeField] GameObject firstPerson;
    [SerializeField] GameObject thirdPerson;

    //The scale value the player needs to get under to activate small objects
    [SerializeField] float playerSizeThresholdSmall;
    //The game objects that become active once the player has reached the small size threshold
    [SerializeField] List<GameObject> smallObjectGroup;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in smallObjectGroup)
        {
            obj.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(thirdPerson.transform.Find("Player").localScale.x <= playerSizeThresholdSmall || firstPerson.transform.Find("Player").localScale.x <= playerSizeThresholdSmall)
        {
            foreach (GameObject obj in smallObjectGroup)
            {
                obj.SetActive(true);
            }
        }
    }
}
