//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerColliderDetector : AColliderDectector
//{
//    public PlayerController playerController;

//    private void ColliderExercuting(GameObject collidedObject)
//    {
//        if (collidedObject.GetComponent<AProjectile>())
//        {
//            AProjectile projectile = collidedObject.GetComponent<AProjectile>();
//            playerController.ChangeHealth(projectile.damage, true); Debug.Log(projectile.damage);
//        }
//        else if (collidedObject.GetComponent<SingleAttack>())
//        {
//            SingleAttack attack = collidedObject.GetComponent<SingleAttack>();
//            playerController.ChangeHealth(attack.damage, true); Debug.Log(attack.damage);
//        }
//    }

//    protected override void OnTriggerEnter2D(Collider2D collision)
//    {
//        base.OnTriggerEnter2D(collision);
//        ColliderExercuting(collision.gameObject);
//    }
//}
