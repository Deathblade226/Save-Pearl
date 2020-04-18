using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTrap : MonoBehaviour
{



    //need to set up the destroy after x time in projectile
    public enum eType
    {
        STRAIGHT,
        ARC,
        BURST,
        MULTISHOT
    }

    [SerializeField] private eType type;

    public eType Type
    {
        get { return type; }
        set { type = value; }
    }


    [SerializeField] private Transform[] targetPoint;

    public Transform[] TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value; }
    }

    [SerializeField] private Transform spawnPoint;

    public Transform SpawnPoint
    {
        get { return spawnPoint; }
        set { spawnPoint = value; }
    }

    [SerializeField] private float speed;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }


    [SerializeField] private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] private int numberOfPrjectiles = 1;

    public int NumOfProjectiles
    {
        get { return numberOfPrjectiles; }
        set { numberOfPrjectiles = value; }
    }

    [SerializeField] private float fireRate;

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    private float timer = 0;

    [SerializeField] private Projectile projectile = null;

    private void Update()
    {
        timer += Time.deltaTime;


        if (timer >= fireRate)
        {
            timer -= fireRate;
            switch (Type)
            {
                case eType.STRAIGHT:
                    {
                        //spawnPoint = transform.position;
                        Projectile p = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                        p.RB.useGravity = false;
                        p.transform.LookAt(targetPoint[0]);
                        p.transform.Rotate(-90.0f, 0.0f, 0.0f);
                        p.RB.AddForce(((targetPoint[0].position - spawnPoint.position) * Speed), ForceMode.VelocityChange);
                        p.Damage = Damage;
                        Destroy(p.gameObject, 2.0f);
                    }
                    break;
                case eType.ARC:
                    {
                        //spawnPoint = transform.position;
                        Projectile p = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                        p.transform.LookAt(targetPoint[0]);
                        p.transform.Rotate(-90.0f, 0.0f, 0.0f);
                        p.RB.AddForce(((targetPoint[0].position - spawnPoint.position) * Speed), ForceMode.VelocityChange);
                        p.Damage = Damage;
                        Destroy(p.gameObject, 2.0f);
                    }
                    break;
                case eType.BURST:
                    {
                        StartCoroutine("BurstFire");
                    }
                    break;
                case eType.MULTISHOT:
                    {
                        for (int i = 0; i < TargetPoint.Length; i++)
                        {

                            //spawnPoint = transform.position;
                            Projectile p = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
                            p.transform.LookAt(targetPoint[i]);
                            p.transform.Rotate(-90.0f, 0.0f, 0.0f);
                            p.RB.AddForce(((targetPoint[i].position - spawnPoint.position) * Speed), ForceMode.VelocityChange);
                            p.Damage = Damage;
                            Destroy(p.gameObject, 2.0f);

                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator BurstFire()
    {
        for (int i = 0; i < NumOfProjectiles; i++)
        {
            //spawnPoint = transform.position;
            Projectile p = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
            p.transform.LookAt(targetPoint[0]);
            p.transform.Rotate(-90.0f, 0.0f, 0.0f);
            p.RB.AddForce(((targetPoint[0].position - spawnPoint.position) * Speed), ForceMode.VelocityChange);
            p.Damage = Damage;
            Destroy(p.gameObject, 2.0f);
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine("BurstFire");
    }


}
