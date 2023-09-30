using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;

    private CharacterController _characterController;
    private PlayerCharacter _playerCharacter;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCharacter = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        if (_playerCharacter.PlayerHealthGet() > 0)
        {
            MoveInput();
        }
    }

    private void MoveInput()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _characterController.Move(movement);
    }
}
