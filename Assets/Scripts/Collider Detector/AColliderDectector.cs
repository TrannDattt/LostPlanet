using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AColliderDectector : MonoBehaviour
{
    //public bool isTriggered;
    //public bool canDestroy;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if(collidedObject.layer != this.gameObject.layer)
        {
            //isTriggered = true;
        }
    }
}
