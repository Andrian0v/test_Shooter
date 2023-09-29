using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    void Update()
    {
        ObjectMove();
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    void ObjectMove()
    {
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }
}
