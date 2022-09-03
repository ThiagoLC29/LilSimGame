using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Parameters")]
    public float followSpeed = 0.01f;
    public float roamSpeed = 20f;
    public Transform target;
    public bool shouldFollow = true;
    public float screenSizeThickness = 2f; //TODO make roaming camera work


    void Update()
    {
        Vector3 pos = transform.position;

        //if (Input.GetKeyDown(KeyCode.Space)) //TODO make roaming camera work
            //shouldFollow = !shouldFollow;

        if (shouldFollow)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
        else //roaming //TODO make roaming camera work
        {
            Debug.Log("camera is roaming");
            //up
            if (Input.mousePosition.y <= Screen.height - screenSizeThickness)
            {
                pos.y += roamSpeed * Time.deltaTime;
            }
            //down
            if (Input.mousePosition.y >= Screen.height - screenSizeThickness)
            {
                pos.y -= roamSpeed * Time.deltaTime;
            }
            //right
            if (Input.mousePosition.x >= Screen.width - screenSizeThickness)
            {
                pos.x += roamSpeed * Time.deltaTime;
            }
            //left
            if (Input.mousePosition.x <= Screen.width - screenSizeThickness)
            {
                pos.x -= roamSpeed * Time.deltaTime;
            }

            transform.position = pos;
        }
    }
}
