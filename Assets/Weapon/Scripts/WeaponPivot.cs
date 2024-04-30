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
        var ray = _cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (_spawnPoint)
        {
            ray.origin = _spawnPoint.position;
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(hit.point);
        }
        else
        {
            transform.localEulerAngles = new Vector3(CameraObjective.RotationOnXAxis, 0, 0);
        }
    }
}
