using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;

    public Transform[] moveSpots;
    public float accuracy;
    private Transform currentTarget;
    private int currentTargetIndex = 0;
    public float waitTime;
    public bool direction = true; //true means from index 0 to len(moveSpots) and false the opposite

    private void Start() {
        currentTarget = moveSpots[currentTargetIndex];
        StartCoroutine(PatrolCoroutine());
    }


    IEnumerator PatrolCoroutine()
    {
        while (true){
            while (Vector3.Distance(transform.position, currentTarget.position) > accuracy){
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

                yield return null;
            }

            if (currentTargetIndex == moveSpots.Length-1){
                direction = false;
            }
            else{
                if (currentTargetIndex <= 0){
                    direction = true;
                }
            }


            if (direction) {
                currentTargetIndex ++;
            } else{
                currentTargetIndex --;
            }

            currentTarget = moveSpots[currentTargetIndex];

            yield return new WaitForSeconds(waitTime);
        }
    }
}


