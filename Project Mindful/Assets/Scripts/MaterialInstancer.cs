using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=AbHTpZmaVpk
/// </summary>
public class MaterialInstancer : MonoBehaviour
{
    private GameObject parentObj;
    private GameObject obj;
    private Material material;
    [SerializeField] public float outlineStrength;

    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.parent.gameObject;
        obj = this.gameObject;
        material = parentObj.GetComponent<MeshRenderer>().material;
            //whats the difference between the top and bottom line?
        //material = obj.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOutline(float thickness)
    {
        material.SetFloat("_OtlWidth", thickness);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SetOutline(outlineStrength);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            SetOutline(0f);
        }
    }
}
