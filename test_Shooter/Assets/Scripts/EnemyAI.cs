using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _obstacleRange = 5.0f;
    [SerializeField] private GameObject _bulletPrefab;

    private bool _alive;
    private GameObject _bullet;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (_alive)
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 1f, out hit))
            {
                GameObject playerCharacter = hit.transform.gameObject;

                if (playerCharacter.GetComponent<CharacterController>())
                {
                    if (_bullet == null)
                    {
                        _bullet = Instantiate(_bulletPrefab);
                        _bullet.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _bullet.transform.rotation = transform.rotation;
                    }
                }
            }

            if (hit.distance < _obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }

    public void SetAlive(bool state)
    {
        _alive = state;
    }
}
