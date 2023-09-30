using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookHead : MonoBehaviour
{
    [SerializeField] private float VerticalSens = 9.0f;
    [SerializeField] private float minimunVert = -65.0f;
    [SerializeField] private float maximumVert = 65.0f;

    private PlayerCharacter _playerCharacter;

    private float _rotationX = 0;

    void Start()
    {
        _playerCharacter = GetComponentInParent<PlayerCharacter>();
    }

    void Update()
    {
        if (_playerCharacter.PlayerHealthGet() > 0)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * VerticalSens;
            _rotationX = Mathf.Clamp(_rotationX, minimunVert, maximumVert);

            transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        }
    }
}
