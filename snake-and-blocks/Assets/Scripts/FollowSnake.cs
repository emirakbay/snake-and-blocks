 using UnityEngine;
 public class FollowSnake : MonoBehaviour
 {
    public Transform TargetObject;
    public float followDistance = 5f;
    public float followHeight = 2f;
    public bool smoothedFollow = false;         
    public float smoothSpeed = 5f;
    public bool useFixedLookDirection = false;
    public Vector3 fixedLookDirection = Vector3.one;

    // Start is called before the first frame update
    void Start ()
    {
        
    }
    // Update is called once per frame
    void Update ()
    {
        //get a vector pointing from camera towards the snake
        Vector3 lookToward = TargetObject.position - transform.position;
        if(useFixedLookDirection )
            lookToward  = fixedLookDirection ;

        Vector3 newPos;
        newPos =  TargetObject.position - lookToward.normalized * followDistance;
        newPos.y = TargetObject.position.y + followHeight ;
 
        if (!smoothedFollow)
        {
            transform.position = newPos;    //move exactly to follow target
        }
         else  
        {
            transform.position += (newPos - transform.position) * Time.deltaTime * smoothSpeed;
        }
 
        lookToward = TargetObject.position - transform.position;
        
        //make this camera look at target
        transform.forward = lookToward.normalized;
    }
}