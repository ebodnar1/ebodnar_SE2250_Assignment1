using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    /*
     * Simply define a public field that represents the type of pickup item, 
     * to be used to determine the scoring in the Sphere class
    */
    public string itemType;

    /*
     * The update method allows the pickup item to constantly rotate
    */
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
