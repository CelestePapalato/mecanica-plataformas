using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Proyectile _proyectilePrefab;
    [SerializeField] Transform _spawnPoint;

    // Update is called once per frame
    void OnShoot()
    {
        Debug.Log("ugh");
    }
}
