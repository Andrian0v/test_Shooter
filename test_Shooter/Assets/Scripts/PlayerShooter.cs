using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private float _shotDuration = 0.02f;
    [SerializeField] private float _weaponRange = 50f;
    [SerializeField] private float _lineWidthStart = 0.1f;
    [SerializeField] private float fireRate = 0.95f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _hitForce = 100f;

    [SerializeField] private Transform _gunEnd;
    [SerializeField] private Camera _camera;

    private LineRenderer _laserLine;
    private AudioSource _gunAudio;
    private float nextFire;
    /*private PlayerCharacter _playerCharacter;*/

    void Start()
    {
        _laserLine = GetComponent<LineRenderer>();
        _camera = GetComponent<Camera>();
        _gunAudio = GetComponent<AudioSource>();
        /*_playerCharacter = GetComponentInParent<PlayerCharacter>();*/
        CursorOff();
    }

    void Update()
    {
        /*
        if (_playerCharacter.PlayerHealthGet() > 0)
        {
            Shoot();
        }
        */
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 screenCenter = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            _laserLine.startWidth = _lineWidthStart;
            _laserLine.SetPosition(0, _gunEnd.position);

            if (Physics.Raycast(screenCenter, _camera.transform.forward, out hit, _weaponRange))
            {
                _laserLine.SetPosition(1, hit.point);

                EnemyReact enemyReact = hit.transform.GetComponent<EnemyReact>();

                if (enemyReact != null && enemyReact.GetHealth() > 0)
                {
                    enemyReact.ReactToHit(_damage);
                }
                if (hit.rigidbody != null && enemyReact.GetHealth() - _damage <= 0)
                {
                    hit.rigidbody.AddForce(-hit.normal * _hitForce);
                }
            }
            else
            {
                _laserLine.SetPosition(1, screenCenter + (_camera.transform.forward * _weaponRange));
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(0.2f);

        Destroy(sphere);
    }

    void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private IEnumerator ShotEffect()
    {
        _gunAudio.Play();
        _laserLine.enabled = true;
        yield return new WaitForSeconds(_shotDuration);
        _laserLine.enabled = false;
    }
}
