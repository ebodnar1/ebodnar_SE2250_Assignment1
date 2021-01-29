using UnityEngine;

public class Main : MonoBehaviour
{
    //The public game objects that hold the Prefab for each pickup object
    public GameObject cubePrefabVar;
    public GameObject cylinderPrefabVar;
    public GameObject starPrefabVar;
    public GameObject compoundPrefabVar;

    //The private gameobjects that represent copies of the different Prefabs
    private GameObject _cube;
    private GameObject _cylinder;
    private GameObject _star;
    private GameObject _compound;

    //Create various different pickup objects upon program start
    void Start()
    {
        //Create 5 cube pickup objects 20 units apart
        Vector3 cubePos = new Vector3(20, 0, 0);
        for(int i = 0; i < 5; i ++)
        {
            _cube = Instantiate(cubePrefabVar);
            _cube.transform.position = _cube.transform.position + i * cubePos;
        }

        //Create 4 cylinder pickup objects 20 units apart
        Vector3 cylinderPos = new Vector3(0, 0, -20);
        for(int j = 0; j < 4; j++)
        {
            _cylinder = Instantiate(cylinderPrefabVar);
            _cylinder.transform.position = _cylinder.transform.position + j * cylinderPos;
        }

        //Create 4 star pickup objects 20 units apart
        Vector3 starPos = new Vector3(0, 0, -20);
        for (int j = 0; j < 4; j++)
        {
            _star = Instantiate(starPrefabVar);
            _star.transform.position = _star.transform.position + j * starPos;
        }

        //Create one compound pickup object
         _compound = Instantiate(compoundPrefabVar);
         _compound.transform.position = _compound.transform.position;
    }
}
