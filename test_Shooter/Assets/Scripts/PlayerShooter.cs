using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(AudioSource))]

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _hitForce = 300f;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float _shotDuration = 1f;
    [SerializeField] private float _weaponRange = 50f;
    [SerializeField] private float _lineWidthStart = 0.5f;
    [SerializeField] private Transform _gunEnd;
    [SerializeField] private Camera _camera;

    private LineRenderer _laserLine;
    private AudioSource _gunAudio;
    private float nextFire;
    private PlayerCharacter _playerCharacter;

    void Start()
    {
        _laserLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _playerCharacter = GetComponentInParent<PlayerCharacter>();
    }

    void Update()
    {
        if (_playerCharacter.PlayerHealthGet() > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 screenCenter = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            _laserLine.startWidth = _lineWidthStart;
            _laserLine.SetPosition(0, _gunEnd.position);

            if (Physics.Raycast(screenCenter, _camera.transform.forward, out hit, _weaponRange))
            {
                _laserLine.SetPosition(1, hit.point);

                EnemyReact enemyReact = hit.transform.GetComponent<EnemyReact>();

                if (enemyReact != null && enemyReact.GetCurrentHealth() < enemyReact.GetMaxHealth())
                {
                    Rigidbody enemyRigidbody;

                    if (enemyReact.GetCurrentHealth() + _damage >= enemyReact.GetMaxHealth())
                    {
                        enemyReact.TryGetComponent<Rigidbody>(out enemyRigidbody);
                        enemyRigidbody.AddForce(-hit.normal * _hitForce);
                    }
                    enemyReact.ReactToHit(_damage);
                }
            }
            else
            {
                _laserLine.SetPosition(1, screenCenter + (_camera.transform.forward * _weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        _gunAudio.Play();
        _laserLine.enabled = true;
        yield return new WaitForSeconds(_shotDuration);
        _laserLine.enabled = false;
    }
}
