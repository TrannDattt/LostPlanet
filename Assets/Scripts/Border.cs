using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    float topBorder; public float TopBorder { get { return topBorder; } }
    float bottomBorder; public float BottomBorder { get {return bottomBorder; } }
    float leftBorder; public float LeftBorder { get { return leftBorder; } }
    float rightBorder; public float RightBorder { get { return rightBorder; } }

    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Vector2 center = boxCollider.bounds.center;
        Vector2 size = boxCollider.bounds.size;

        topBorder = center.y + size.y / 2;
        bottomBorder = center.y - size.y / 2;
        leftBorder = center.x - size.x / 2;
        rightBorder = center.x + size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 Re_positioning(Vector2 position)
    {
        if (position.x < leftBorder) return new Vector2(leftBorder + 0.2f, position.y);
        else if (position.x > rightBorder) return new Vector2(rightBorder - 0.2f, position.y);
        else if (position.y < bottomBorder) return new Vector2(position.x, bottomBorder + 0.2f);
        else if (position.y > topBorder) return new Vector2(position.x, topBorder - 0.2f);
        return position;
    }
}
