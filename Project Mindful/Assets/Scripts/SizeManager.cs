using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour
{
    #region Fields
    private float _scale;
    private float _originalScale;

    // LERP vals
    private float _time;
    private float _newScale;
    private float _prevScale;
    private float _scaleSpeed;

    [SerializeField] private float _fovScale;
    [SerializeField] private float _minScale;
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
        _newScale = _scale;
        _prevScale = _scale;
        _scaleSpeed = 1f;
    }

    /// <summary>
    /// Update the player scale.
    /// </summary>
    void Update()
    {       
        if (_scale != _newScale)
        {
            _scale = Mathf.Lerp(_prevScale, _newScale, _time / _scaleSpeed);
            _time += Time.deltaTime;
            ScalePlayer();
        }
    }

    public void SetScale(float scale, float duration = 1f)
    {
        if (scale <= _originalScale && scale >= _minScale)
        {
            _newScale = scale;
            _prevScale = _scale;
            _scaleSpeed = duration;
            _time = 0f;
        }
    }

    public void DecreaseScale(float decrease, float duration = 1f)
    {
        float endValue = _scale - decrease;

        if (endValue > _minScale)
        {
            _newScale = endValue;
            _prevScale = _scale;
            _scaleSpeed = duration;
            _time = 0f;
        }
    }

    public void IncreaseScale(float increase, float duration = 1f)
    {
        float endValue = _scale + increase;

        if (endValue > _originalScale)
        {
            _newScale = endValue;
            _prevScale = _scale;
            _scaleSpeed = duration;
            _time = 0f;
        }
    }

    private void ScalePlayer()
    {
        // Scale the first person appropriately
        _firstPerson.transform.localScale = new Vector3(_scale, _scale, _scale);

        // Scale the third person and camera FOV appropriately
        _thirdPerson.transform.localScale = new Vector3(_scale, _scale, _scale);
        Debug.Log("Scale: " + _scale + "FOV Scale: " + _fovScale);
        _thirdPerson.transform.Find("PlayerFollowCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = _scale * _fovScale;
    }
}
