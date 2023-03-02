using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : MonoBehaviour
{
    [SerializeField] int maxInteractions;
    public int currentInteractions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if the player has maxed out their interaction count. If so, disable all interactable scripts of its children
        if (currentInteractions >= maxInteractions)
        {
            foreach(Transform child in transform)
            {
                child.GetComponent<Interactable>().enabled = false;
            }
        }
    }
}
