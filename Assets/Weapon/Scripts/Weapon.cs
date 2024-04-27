using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Proyectile _proyectilePrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private float shootStrength;

    private bool _canShoot = true;
    // Update is called once per frame
    void OnShoot()
    {
        if (!_canShoot)
        {
            return;
        }
        StartCoroutine(controlarCadencia());
        Proyectile proyectile = Instantiate(_proyectilePrefab, _spawnPoint.position, Quaternion.identity);
        Rigidbody rb = proyectile.GetComponent<Rigidbody>();
        rb.AddRelativeForce(transform.up * shootStrength, ForceMode.Impulse);
    }

    private IEnumerator controlarCadencia()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }
}
