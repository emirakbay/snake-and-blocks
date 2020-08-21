using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public float minDist = 0.25f;
    public float rotationSpeed = 50;
    public float speed = 1;
    public int beginSize = 1;
    public GameObject bodyPrefab;
    private float dist;
    private Transform currBodyPart;
    private Transform prevBodyPart;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < beginSize -1; i++) {
            AddBodyParts();
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Q))
            AddBodyParts();
    }
    public void Move() {

        float currSpeed = speed;

        if (Input.GetKey(KeyCode.W))
            currSpeed *= 2;

        bodyParts[0].Translate(bodyParts[0].forward * currSpeed * Time.smoothDeltaTime, Space.World);

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
}
