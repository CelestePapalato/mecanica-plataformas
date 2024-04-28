using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    Camera _cam;

    // Start is called before the first frame update
    void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        updateRotation();
    }

    void updateRotation()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit) || !_spawnPoint)
        {
            transform.localEulerAngles = new Vector3(CameraObjective.RotationOnXAxis, 0, 0);
        }
        Vector3 raycastDirection = (hit.point - _spawnPoint.position).normalized;
        ray = new Ray(hit.point, raycastDirection);
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
        }
    }
}
