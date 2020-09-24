﻿using UnityEngine.UI;
using UnityEngine;
public class Block : MonoBehaviour
{
    public GameObject myObj;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(int n)
    {
        number = n;
        foreach (Text text in myObj.GetComponentsInChildren<Text>())
        {
            text.text = "" + n;
        }
    }
    //Decreases the number appended to the block by one in each collide. 
    public void OnSnakeCollider()
    {
        number--;

        if (number <= 0)
            Destroy(gameObject);
            
        Setup(number);
    }
}
