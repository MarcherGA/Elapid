using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private GameObject _projectilePrefab;

    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.transform.position, _projectilePrefab.transform.rotation);
        projectile.transform.localScale = new Vector3(projectile.transform.localScale.x * transform.localScale.x > 0 ? 1 : -1, projectile.transform.localScale.y, projectile.transform.localScale.z);
    }
}
