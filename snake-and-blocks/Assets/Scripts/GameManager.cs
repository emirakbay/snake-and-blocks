﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
   #region Variables
   public GameObject CubePrefab;
   public float lastZValue = 0F;
   bool gameHasEnded = false;
   public float restartDelay = 1F;
   public Vector3 vector;

   
   #endregion

   #region Main Methods
   void Start()
   {
       for (int i=0; i<100; i++)
       {
            lastZValue = SpawnCubes(CubePrefab, 5, 0F, 0F, lastZValue + Random.Range(5,30), 2F, i);
       }
   }
   void Update()
   {
       CheckSize();
   }

   #endregion

   #region Helper Methods
   float SpawnCubes(GameObject prefab, int numCubes, float startX, float startY, float startZ, float delta, int linear_increase)
    {
        GameObject myObj = null;
        for (int i = 0; i < numCubes; ++i) 
        {
            myObj = 
                    Instantiate(
                    prefab,
                    new Vector3(startX + (float)i * delta, startY, startZ),
                    Quaternion.identity
                    );

            int rand = Random.Range(CalcMin(linear_increase), CalcMax(linear_increase));
            myObj.GetComponent<Block>().Setup(rand);
        }

        return myObj.transform.position.z;
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CheckSize()
    {
        int size = SnakeMovement.Instance.SnakeSize();

        if (size == 0)
            EndGame();
    }

    public int CalcMin(int num)
    {
        return num/2 + 1;
    }

    public int CalcMax(int num)
    {
        return num*2 + 5;
    }
    #endregion
}

