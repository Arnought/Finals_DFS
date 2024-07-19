using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    private List<Vector3> path;
    private int targetIndex;

    public void SetPath(List<Vector3> path)
    {
        this.path = path;
        targetIndex = 0;
    }

    void Update()
    {
        if (path == null || targetIndex >= path.Count)
            return;

        Vector3 targetPosition = path[targetIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetIndex++;
        }
    }
}
