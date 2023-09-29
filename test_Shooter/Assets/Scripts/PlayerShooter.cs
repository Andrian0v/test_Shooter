using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private string _crosshair = "0";
    [SerializeField] private int _crosshairSize = 20;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        CursorOff();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenCenter = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                EnemyReact enemyReact = hit.transform.GetComponent<EnemyReact>();

                if (enemyReact != null)
                {
                    enemyReact.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
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

    void OnGUI()
    {
        float posX = _camera.pixelWidth / 2;
        float posY = _camera.pixelHeight / 2;
        GUI.Label(new Rect(posX, posY, _crosshairSize, _crosshairSize), _crosshair);
    }

    void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
