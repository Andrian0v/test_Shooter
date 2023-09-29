using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReact : MonoBehaviour
{
    public void ReactToHit()
    {
        GetComponent<EnemyAI>().SetAlive(false);

        StartCoroutine(EnemyDie());
    }

    private IEnumerator EnemyDie()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
