using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IVisitable
{
    [SerializeField] Proyectile _proyectilePrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _currentFireRateMultiplier = 1;
    private float FireRate
    {
        get { return _fireRate / _currentFireRateMultiplier; }
        set { FireRate = value; }
    }
    [SerializeField]
    private float shootStrength;

    private bool _canShoot = true;

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }


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
        yield return new WaitForSeconds(FireRate);
        _canShoot = true;
    }

    public void FireRateBonus(float fireRateBonus, float bonusTimeLength)
    {
        StopAllCoroutines();
        StartCoroutine(FireRateBonusTimer(fireRateBonus, bonusTimeLength));
    }

    private IEnumerator FireRateBonusTimer(float fireRateBonus, float timeLength)
    {
        _currentFireRateMultiplier = fireRateBonus;
        yield return new WaitForSeconds(timeLength);
        _currentFireRateMultiplier = 1;
    }
}
