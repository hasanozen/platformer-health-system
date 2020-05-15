using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public float min_X_Treshold;
    public float smoothSpeed;

    [SerializeField] private Transform farBG, midBG;
    [SerializeField] private float minYHeight, maxYHeight;

    //private float lastXPos;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        GetLastXPosition();
    }

    // Update is called once per frame
    private void Update()
    {
        //float amountToMoveX = transform.position.x - lastPos.x;
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
        midBG.position += new Vector3(amountToMove.x * .5f, amountToMove.y * .5f, 0f);
        GetLastXPosition();
    }

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        float clampedY = Mathf.Clamp(transform.position.y, minYHeight, maxYHeight);

        Vector3 newTarget = new Vector3(transform.position.x, clampedY);
        Vector3 temp = transform.position;
        Vector3 smoothedPos = Vector3.Lerp(newTarget, target.position, smoothSpeed);

        temp.x = smoothedPos.x;
        temp.y = smoothedPos.y;

        transform.position = temp;
    }

    private void GetLastXPosition()
    {
        lastPos = transform.position;
    }

}
