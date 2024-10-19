using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : ABorder
{
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        RepositioningPlayer();
    }

    public void RepositioningPlayer()
    {
        Vector2 position = player.body.transform.position;

        if (position.x < leftBorder) position = new Vector2(leftBorder + 0.2f, position.y);
        else if (position.x > rightBorder) position = new Vector2(rightBorder - 0.2f, position.y);
        else if (position.y < bottomBorder) position = new Vector2(position.x, bottomBorder + 0.2f);
        else if (position.y > topBorder) position = new Vector2(position.x, topBorder - 0.2f);

        player.body.transform.position = position;
    }
}
