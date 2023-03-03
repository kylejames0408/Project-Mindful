using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableParent : MonoBehaviour
{
    [SerializeField] int maxInteractions;
    public int currentInteractions;
    private bool childrenDeactivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks to see if the player has maxed out their interaction count. If so, disable all interactable scripts of its children
        if (currentInteractions >= maxInteractions && !childrenDeactivated)
        {
            DeactivateChildren();
        }
    }

    void DeactivateChildren()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("Deactivate children");
            child.GetComponent<MeshCollider>().enabled = false;
            child.GetComponent<SphereCollider>().enabled = false;
            child.GetComponent<Interactable>().DisableOutLine();
        }
        childrenDeactivated = true;
    }
}
