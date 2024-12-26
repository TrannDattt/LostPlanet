using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Vector3 posOffset;
    private Vector3 velocity = Vector3.zero;
    private Vector3 originalPos;

    [SerializeField] private float followDelay;
    [SerializeField] private Vector2 borderX;
    [SerializeField] private Vector2 borderY;

    private void Start()
    {
        originalPos = transform.localPosition;

        player = Player.Instance.gameObject;
        posOffset = transform.localPosition - player.transform.position;
    }

    private void LateUpdate()
    {
        if(!Player.Instance.gameObject.activeInHierarchy)
        {
            transform.localPosition = originalPos;
        }
        else
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = ClampPos(player.transform.position + posOffset);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref velocity, followDelay);
    }

    private Vector3 ClampPos(Vector3 curPos)
    {
        float clampPosX = Mathf.Clamp(curPos.x, borderX.x, borderX.y);
        float clampPosY = Mathf.Clamp(curPos.y, borderY.x, borderY.y);
        return new Vector3 (clampPosX, clampPosY, transform.localPosition.z);
    }
}
