//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyColliderDetector : AColliderDectector
//{
//    public AEnemyController enemyController;

//    private void ColliderExercuting(GameObject collidedObject)
//    {
//        if (collidedObject.GetComponent<SingleAttack>())
//        {
//            SingleAttack attack = collidedObject.GetComponent<SingleAttack>();
//            enemyController.ChangeHealth(attack.damage, true); Debug.Log(attack.damage);
//        }
//    }

//    protected override void OnTriggerEnter2D(Collider2D collision)
//    {
//        base.OnTriggerEnter2D(collision);
//        ColliderExercuting(collision.gameObject);
//    }
//}
