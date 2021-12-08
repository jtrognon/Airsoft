using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    //spawn if the rotation is given
    public static void Spawn(float xPos, float yPos, float zPos, GameObject obj, Quaternion rotation){ //change the object location or make the object spawn
        if (obj.activeInHierarchy){ //check if the object is already in the scene
            obj.transform.position = new Vector3(xPos, yPos, zPos); //set the position
            obj.transform.rotation = rotation;  //set the rotation
        }
        else
        {
            //instantiate a new object if it's not in the scene
            Vector3 coordinate = new Vector3(xPos, yPos, zPos);
            Instantiate(obj, coordinate, rotation);
        }
    }

    // spawn if the rotation is not given
    public static void Spawn(float xPos, float yPos, float zPos, GameObject obj){
        if (obj.activeInHierarchy){
            obj.transform.position = new Vector3(xPos, yPos, zPos);
        }
        else
        {
            Quaternion rotation = new Quaternion(0, 0, 0, 1); //create a quaternion of zero because no rotation was given
            Vector3 coordinate = new Vector3(xPos, yPos, zPos);
            Instantiate(obj, coordinate, rotation);
        }
    }
}
