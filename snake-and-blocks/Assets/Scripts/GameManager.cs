using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
   // Instantiates prefabs in a circle formation
   public GameObject prefab;
   public int numberOfObjects = 20;
   public float radius = 5f;
   void Start()
   {
       SpawnCubes(prefab, 5, 0F, 0F, 12F, 2F);
       SpawnCubes(prefab, 10, 0F, 0F, 14F, 2F);
       SpawnCubes(prefab, 15, 0F, 0F, 16F, 2F);
       SpawnCubes(prefab, 20, 0F, 0F, 18F, 2F);   
       
    /*
       for (int i = 0; i < numberOfObjects; i++)
       {
           float angle = i * Mathf.PI * 2 / numberOfObjects;
           float x = Mathf.Cos(angle) * radius;
           float z = Mathf.Sin(angle) * radius;
           Vector3 pos = transform.position + new Vector3(x, -1F, z);
           float angleDegrees = -angle*Mathf.Rad2Deg;
           Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
           Instantiate(prefab, pos, rot);
       }
    */
   }
   void SpawnCubes(GameObject prefab, int numCubes, float startX, float startY, float startZ, float delta)
    {
    for (int i = 0; i < numCubes; ++i) {
        Instantiate(
            prefab,
            new Vector3(startX + (float)i * delta, startY, startZ),
            Quaternion.identity
        );
        }
    }
}

