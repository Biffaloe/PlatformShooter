using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlatfroming : MonoBehaviour
{

    public Transform target;
    public Transform pivot;

    Collider enemyTarget;

    public Vector3 offset;

    public bool InvertY;
    public bool usedOffsetValues;
    bool lockedOn;

    public float lockOnRange;
    public float followDistance;
    public float followHeight;
    public float MaxViewAngle;
    public float MinViewAngle;
    public float rotateSpeed;

	void Start ()
    {
        if ( !usedOffsetValues)
            offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void LateUpdate ()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            lockedOn = true;
            CameraLockon();
        }
        else
        {
            lockedOn = false;
            CameraTurn();
        }
	}

    void CameraTurn()
    {
        //get x and y pos of the mouse and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        if (InvertY)
            pivot.Rotate(vertical, 0, 0);
        else
            pivot.Rotate(-vertical, 0, 0);


        //limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x > MaxViewAngle && pivot.rotation.eulerAngles.x < 180f)
            pivot.rotation = Quaternion.Euler(MaxViewAngle, 0, 0);
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < (360 + MinViewAngle))
            pivot.rotation = Quaternion.Euler((360f + MinViewAngle), 0, 0);



        //Move the camera based on the current rotation of the target;
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        //transform.position = target.position - offset;
        transform.LookAt(target);
    }

    void CameraLockon()
    {
        FindClosestEnemy();

        if (target != null)
        {
            Vector3 newPos = target.position + (-target.transform.forward * followDistance);
            newPos.y += followHeight;
            transform.position = newPos;
        }
        if (enemyTarget != null)
        {
            gameObject.transform.LookAt(enemyTarget.transform);
            target.LookAt(new Vector3(enemyTarget.transform.localPosition.x, 0, enemyTarget.transform.localPosition.z));
        }
        else if (enemyTarget != null)
        {
            Vector3 pos1 = target.transform.position;
            Vector3 pos2 = enemyTarget.transform.position;
            Vector3 dir = (pos2 - pos1).normalized;
            Vector3 perpDir = Vector3.Cross(dir, Vector3.right);
            Vector3 midPoint = (pos1 + pos2) / 2f;
            gameObject.transform.LookAt(midPoint);
        }
    }

    void FindClosestEnemy()
    {
        int numEnemies = 0;
        var hitColliders = Physics.OverlapSphere(transform.position, lockOnRange);
        foreach (var hit in hitColliders)
        {
            if (!hit || hit.gameObject == this.gameObject || hit.gameObject.tag == this.gameObject.tag)
            {
                continue;
            }
            if (hit.tag != "Enemy") 
                continue;
            var relativePoint = Camera.main.transform.InverseTransformPoint(hit.transform.position);
            if (relativePoint.z < 0)
            {
                continue;
            }
            numEnemies += 1;
            if (enemyTarget == null)
            {
                print("TARGET FOUND");
                enemyTarget = hit;
            }
        }
        if (numEnemies < 1)
        {
            lockedOn = false;
            enemyTarget = null;
        }
    }
}
