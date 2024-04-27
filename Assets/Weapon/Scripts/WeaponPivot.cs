using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    Camera _cam;
    GameObject _spawnPoint;

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
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
            return;
        }
        transform.localEulerAngles = new Vector3(CameraObjective.RotationOnXAxis, 0, 0);
    }
}
