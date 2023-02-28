using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    #region Fields
    private float _time;
    private float _scale;
    private float _originalScale;
    [SerializeField] private float _scalar; // set larger to have small decreases over time
    [SerializeField] private float fovScale;
    [SerializeField] private float minScale;
    [SerializeField] private GameObject _firstPerson;
    [SerializeField] private GameObject _thirdPerson;
    #endregion
    public float ScalePercent { get { return (_scale / _originalScale); } }

    /// <summary>
    /// Initialize Fields.
    /// </summary>
    void Start()
    {
        // Initialize fields
        _time = 0;
        _originalScale = _firstPerson.transform.localScale.x; // Assumes scale is uniform
        _scale = _originalScale;
    }

    /// <summary>
    /// Update the player scale.
    /// </summary>
    void Update()
    {
        // Update the scale if larger than 1/4 of the original size.
        if (_scale > _originalScale*minScale)
        {
            // Update time and scale
            _time += Time.deltaTime;
            _scale = _originalScale - (_time / _scalar);

            // Scale the first person appropriately
            _firstPerson.transform.localScale = new Vector3(_scale, _scale, _scale);

            // Scale the third person and camera FOV appropriately
            _thirdPerson.transform.localScale = new Vector3(_scale, _scale, _scale);
            _thirdPerson.transform.Find("PlayerFollowCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = _scale * fovScale;
        }
    }
}
