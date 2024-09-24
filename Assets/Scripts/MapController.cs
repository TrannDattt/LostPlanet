using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public GameObject sun;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = sun.transform.position.x - Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        sun.transform.position = new Vector3(Camera.main.transform.position.x + offset, sun.transform.position.y, sun.transform.position.z);
    }
}
