using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    #region Variables
    public List<Transform> bodyParts = new List<Transform>();
    public float minDist = 0.25f;
    public float rotationSpeed = 150;
    public float speed = 1;
    public int beginSize = 1;
    public GameObject bodyPrefab;
    private float dist;
    private Transform currBodyPart;
    private Transform prevBodyPart;
    public static SnakeMovement instance = null;

    #endregion

    #region Main Methods
    // Start is called before the first frame update
    void Start()
    {
        EnableHeadCollider();
        for (int i=0; i < beginSize -1; i++) {
            AddBodyParts();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        Move();

        //Debug.Log("max_X == >" + Camera.main.ScreenToWorldPoint(new Vector3(0,0,Camera.main.depth)));
        //Debug.Log("min_X == >" + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,Camera.main.depth)));

        if (Input.GetKeyDown(KeyCode.Q))
            AddBodyParts();
    }
    // void LateUpdate()
    // {
    //     if(bodyParts.Count == 0)
    //         return;
    //     screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    //     Vector3 viewPos = bodyParts[0].position;
    //     viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
    //     bodyParts[0].position = viewPos;
        
    // }
    public static SnakeMovement Instance
     {
         get
         { 
             return instance; 
         }
     }
     private void Awake()
     {
         // if the singleton hasn't been initialized yet
         if (instance != null && instance != this) 
         {
             Destroy(this.gameObject);
         }
 
         instance = this;
         //DontDestroyOnLoad(this.gameObject);
     }

    #endregion

    #region Helper Methods
    public void Move() {

        if (bodyParts.Count == 0)
            return;

        float currSpeed = speed;

        if (Input.GetKey(KeyCode.W))
            currSpeed *= 4f;

        bodyParts[0].Translate(bodyParts[0].forward * currSpeed * Time.deltaTime, Space.World);

        if(Input.GetAxis("Horizontal") != 0)
            bodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for (int i=1; i < bodyParts.Count; i++) {

            currBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i-1];

            dist = Vector3.Distance(prevBodyPart.position, currBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.y = bodyParts[0].position.y;

            float T = Time.deltaTime * dist / minDist * currSpeed;

            if  (T > 0.5f)
                T = 0.5f;

            currBodyPart.position = Vector3.Slerp(currBodyPart.position, newPos, T);
            currBodyPart.rotation = Quaternion.Slerp(currBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }
    public void AddBodyParts() {

        Transform newPart = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count -1].rotation) as GameObject).transform;

        newPart.SetParent(transform);

        bodyParts.Add(newPart);
    }
    public int SnakeSize()
    {
        return bodyParts.Count;
    }
    public void EnableHeadCollider()
    {
        if (bodyParts.Count == 0)
        {
            FindObjectOfType<GameManager>().EndGame();
            return; 
        }
        bodyParts[0].GetComponent<Collider>().enabled = true;
    }
    public void OnBlockCollided()
    {
        Destroy(bodyParts[0].gameObject);
        bodyParts.RemoveAt(0);
        if (bodyParts.Count == 0)
        {
            FindObjectOfType<GameManager>().EndGame();
            return; 
        }
        EnableHeadCollider();
    }
    // public void BorderController()
    // {
    //     Vector3 limit = Camera.main.ScreenToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane));
    //     float min_X = limit.x;
    //     float max_X = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,Camera.main.nearClipPlane)).x;

    //     var new_X = Mathf.Clamp(bodyParts[0].position.x, max_X, min_X);

    //     bodyParts[0].position = new Vector3(new_X, bodyParts[0].position.y, bodyParts[0].position.z);
    // }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane)), 2);
    //     Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,Camera.main.nearClipPlane)), 2);
    // }
    #endregion
}