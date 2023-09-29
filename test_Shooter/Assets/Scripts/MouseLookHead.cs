using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookHead : MonoBehaviour
{
    [SerializeField] private float VerticalSens = 9.0f;
    [SerializeField] private float minimunVert = -65.0f;
    [SerializeField] private float maximumVert = 65.0f;

    private float _rotationX = 0;

    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * VerticalSens;
        _rotationX = Mathf.Clamp(_rotationX, minimunVert, maximumVert);

        transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
    }
}
