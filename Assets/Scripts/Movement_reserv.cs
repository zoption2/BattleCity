using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement_reserv : MonoBehaviour
{
    public float speed = 5;
    protected Transform playerIDLE;
    protected bool isMoving = false;

    protected IEnumerator MoveHorizontal (float movementHorizontal, Rigidbody rigidbody )
    {
        isMoving = true;

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
       
        Quaternion quaternion = Quaternion.Euler(0, movementHorizontal * 90, 0);
        transform.rotation = quaternion;

        float movementProgress = 0f;
        Vector3 movement, endPosition;

        while (movementProgress < Mathf.Abs(movementHorizontal))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);
            movement = new Vector3(speed * Time.deltaTime * movementHorizontal, 0f, 0f);
            endPosition = rigidbody.position + movement;

            if (movementProgress == 1f)
            {
                endPosition = new Vector3(Mathf.Round(endPosition.x), transform.position.y, endPosition.z);
            }
            rigidbody.MovePosition(endPosition);

            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }

    protected IEnumerator MoveVertical(float movementVertical, Rigidbody rigidbody)
    {
        isMoving = true;

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        Quaternion quaternion;

        if (movementVertical < 0)
        {
            quaternion = Quaternion.Euler(0, movementVertical * 180f, 0);
        }
        else
        {
            quaternion = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = quaternion;

        float movementProgress = 0f;
        Vector3 endPosition, movement;

        while (movementProgress < Mathf.Abs(movementVertical))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);

            movement = new Vector3(0f, 0f, speed * Time.deltaTime * movementVertical);
            endPosition = rigidbody.position + movement;

            if (movementProgress == 1)
            {
                endPosition = new Vector3(endPosition.x, transform.position.y, Mathf.Round(endPosition.z));
            }
            rigidbody.MovePosition(endPosition);

            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }

    protected IEnumerator MoveHorizontal_Ice(float movementHorizontal, Rigidbody rigidbody)
    {
        isMoving = true;

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        Quaternion quaternion = Quaternion.Euler(0, movementHorizontal * 90, 0);
        transform.rotation = quaternion;

        float movementProgress = 0f;
        Vector3 movement, endPosition;

        while (movementProgress < Mathf.Abs(movementHorizontal*2))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 2f);
            movement = new Vector3(speed * Time.deltaTime * movementHorizontal, 0f, 0f);
            endPosition = rigidbody.position + movement;

            if (movementProgress == 2f)
            {
                endPosition = new Vector3(Mathf.Round(endPosition.x), transform.position.y, endPosition.z);
                Debug.Log("Move throu the ice!");
            }
            rigidbody.MovePosition(endPosition);

            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }

    protected IEnumerator MoveVertical_Ice(float movementVertical, Rigidbody rigidbody)
    {
        isMoving = true;

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        Quaternion quaternion;

        if (movementVertical < 0)
        {
            quaternion = Quaternion.Euler(0, movementVertical * 180f, 0);
        }
        else
        {
            quaternion = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = quaternion;

        float movementProgress = 0f;
        Vector3 endPosition, movement;

        while (movementProgress < Mathf.Abs(movementVertical * 2))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 2f);

            movement = new Vector3(0f, 0f, speed * Time.deltaTime * movementVertical);
            endPosition = rigidbody.position + movement;

            if (movementProgress == 2)
            {
                endPosition = new Vector3(endPosition.x, transform.position.y, Mathf.Round(endPosition.z));
            }
            rigidbody.MovePosition(endPosition);

            yield return new WaitForEndOfFrame();
        }

        isMoving = false;
    }
}
