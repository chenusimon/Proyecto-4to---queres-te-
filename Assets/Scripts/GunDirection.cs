using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDirection : MonoBehaviour
{
    public Transform targetTR;
    public EnemyShoot shoot;
    float lookAngleY;
    float lookAngleX;
    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (targetTR.position - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        }
        lookAngleY = lookRotation.eulerAngles.y;
        lookAngleX = lookRotation.eulerAngles.x;
    }
    public void Shoot()
    {
        shoot.Shoot(lookAngleX, (lookAngleY), 1);
    }
}
