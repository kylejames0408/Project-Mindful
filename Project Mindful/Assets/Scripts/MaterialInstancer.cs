using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SOURCE: https://www.youtube.com/watch?v=AbHTpZmaVpk
/// </summary>
public class MaterialInstancer : MonoBehaviour
{
    private GameObject obj;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
        material = obj.GetComponent<MeshRenderer>().material;
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
}
