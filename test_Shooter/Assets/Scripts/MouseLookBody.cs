using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookBody : MonoBehaviour
{
    [SerializeField] private float HorizontalSens = 9.0f;

    private float _rotationY = 0;

    void Update()
    {
        _rotationY = transform.eulerAngles.y + Input.GetAxis("Mouse X") * HorizontalSens;

        transform.eulerAngles = new Vector3(0, _rotationY, 0);
    }
}
