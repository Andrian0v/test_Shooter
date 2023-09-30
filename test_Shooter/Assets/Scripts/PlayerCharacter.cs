using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _playerHealth;
    [SerializeField] private TMP_Text _healthText;

    void Start()
    {
        _healthText.text = _playerHealth.ToString();
    }

    public void Damage(int damage)
    {
        _playerHealth -= damage;
        _healthText.text = _playerHealth.ToString();
    }

    public int PlayerHealthGet()
    {
        return _playerHealth;
    }
}
