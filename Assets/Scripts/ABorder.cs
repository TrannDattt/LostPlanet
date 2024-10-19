using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABorder : MonoBehaviour
{

    public BoxCollider2D boxCollider;

    public virtual float topBorder { get; private set; }
    public virtual float bottomBorder { get; private set; }
    public virtual float leftBorder { get; private set; }
    public virtual float rightBorder { get; private set; }

    protected virtual void Awake()
    {
        Vector2 center = boxCollider.bounds.center;
        Vector2 size = boxCollider.bounds.size;

        topBorder = center.y + size.y / 2;
        bottomBorder = center.y - size.y / 2;
        rightBorder = center.x + size.x / 2;
        leftBorder = center.x - size.x / 2;
    }
}
