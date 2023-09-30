using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReact : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private GameObject Eye_Light;

    private int _currentDamage;

    private void Start()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = 0;
        _healthSlider.fillRect.gameObject.SetActive(false);
    }

    public void ReactToHit(int damage)
    {
        _currentDamage += damage;
        _healthSlider.fillRect.gameObject.SetActive(true);
        _healthSlider.value = _currentDamage;

        if (_currentDamage >= _maxHealth)
        {
            GetComponent<EnemyAI>().SetAlive(false);
            StartCoroutine(EnemyDie());
        }
    }

    private IEnumerator EnemyDie()
    {
        Eye_Light.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentDamage;
    }
}
