using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damagePoint = 20;

    void Update()
    {
        ObjectMove();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacter>())
        {
            other.GetComponent<PlayerCharacter>().Damage(_damagePoint);
        }
        Destroy(this.gameObject);
    }

    void ObjectMove()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
}
