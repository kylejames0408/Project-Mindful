using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    #region Fields
    private float _time;
    private float _scale;
    private float _scalar;
    [SerializeField] private GameObject _firstPerson;
    [SerializeField] private GameObject _thirdPerson;
    #endregion

    /// <summary>
    /// Initialize Fields.
    /// </summary>
    void Start()
    {
        // Initialize fields
        _time = 0;
        _scale = 1;
        _scalar = 100; // set larger to have small decreases over time
    }

    /// <summary>
    /// Update the player scale.
    /// </summary>
    void Update()
    {
        // Update the scale if larger than 1/4 of the original size.
        if (_scale > 0.25f)
        {
            // Update time and scale
            _time += Time.deltaTime;
            _scale = 1 - (_time / _scalar);

            // Scale the first person appropriately
            _firstPerson.transform.localScale = new Vector3(_scale, _scale, _scale);

            // Scale the third person and camera FOV appropriately
            _thirdPerson.transform.localScale = new Vector3(_scale, _scale, _scale);
            _thirdPerson.transform.Find("PlayerFollowCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = _scale * 50;
        }
    }
}
