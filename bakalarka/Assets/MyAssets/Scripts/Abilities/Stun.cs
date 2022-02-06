using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stun : Ability
{
    public float stunTime;
    public int damage;

    public Camera cam;
    public GameObject projectile;
 //   public Transform firePoint;
    public float fireRate = 4;

    private Vector3 destination;
    private float timeToFire;
    private GroundSlash groundSlashScript;


    public override void Activate(GameObject parent, int level)
    {
        damage += level * 2;
        Ray ray = cam.ScreenPointToRay(new Vector3(0.5f,0.5f,0));
        destination = ray.GetPoint(1000);

        InstantiateProjectile(parent);
    }

    void InstantiateProjectile(GameObject parent) {
        var projectileObj = Instantiate(projectile, parent.transform.position, parent.transform.rotation) as GameObject;
        
        groundSlashScript = projectileObj.GetComponent<GroundSlash>();
        groundSlashScript.OnHit += OnHit;

        projectileObj.GetComponent<Rigidbody>().velocity = parent.transform.forward * groundSlashScript.speed;
    }

    void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    void OnHit(Enemy enemy)
    {
        Debug.Log(enemy + " " + enemy.GetCharacterStats().currentHealth + " " + damage);
        enemy.GetCharacterStats().TakeDamage(damage);
    }

    public override void LevelUp()
    {
        damage++;
    }
}
