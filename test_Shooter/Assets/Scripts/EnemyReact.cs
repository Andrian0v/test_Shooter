using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReact : MonoBehaviour
{
    [SerializeField] private int _health = 3;

    public void ReactToHit(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            GetComponent<EnemyAI>().SetAlive(false);
            StartCoroutine(EnemyDie());
        }
    }

    private IEnumerator EnemyDie()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }

    public int GetHealth()
    {
        return _health;
    }
}
