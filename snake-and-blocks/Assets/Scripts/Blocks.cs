using UnityEngine.UI;
using UnityEngine;
public class Blocks : MonoBehaviour
{
    public GameObject myObj;
    // Start is called before the first frame update
    void Start()
    {
        ChangeText();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText()
    {
        foreach (Text text in myObj.GetComponentsInChildren<Text>())
        {
            
        
        }
    }
}
