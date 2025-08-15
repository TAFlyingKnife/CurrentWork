using UnityEngine;

public class Star1 : MonoBehaviour
{
    //Struct Array that the code references when moving selectable options
    private HandData[] Planets;

    private Transform Subject;
    
    //Struct that holds the GameObject action and the coordinates on the wheel it should be at
    public struct HandData
    {
        public HandData(Vector3 coords, GameObject choice)
        {
            C = coords;
            G = choice;
        }
        
        public Vector3 C;
        public GameObject G;

        public override string ToString() => $"({C}, {G})";
    }
    
    //Loads all available options
    public void SetDefault()
    {
        var section = Resources.LoadAll("MoveOptions");
        Planets = new HandData[section.Length];
        for (int i = 0; i < section.Length; i++)
            Planets[i].G = (GameObject)section[i];
    }

    //Rotates the selected option to the right option
    public void Next()
    {
        Subject.Rotate(0, 0, -360/Planets.Length);
    }

    //Rotates the selected option to the left option
    public void Last()
    {
        Subject.Rotate(0, 0, 360/Planets.Length);
    }
    
    // -------------------------------------------- Okay this point on is where the runtime code is!! --------------------------------------------
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Subject = transform;
        SetDefault();
        for (int i = 0; i < Planets.Length; i++)
        {
            Vector3 OptionSpawn = new Vector3(0, 2, 0);
            Instantiate(Planets[i].G, OptionSpawn, Quaternion.identity, Subject);
        }
        
        //Code Imported from HandDraw(), just a test of function for now
        for (int j = 0; j < Subject.childCount; j++)
        {
            GameObject child = Subject.GetChild(j).gameObject;
            child.transform.RotateAround(Subject.position, Vector3.forward, j*-360/Planets.Length);
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Next();
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            Last();
        }
    }
}
