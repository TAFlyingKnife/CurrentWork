using UnityEngine;

public class Camera_Platforming : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera cam;
    private Transform camGirl;
    private Vector3[] followSpots = new Vector3[60];
    private int trackSpot = 0;
    public int delay;
    public float camMaxZoom;
    public float camMinZoom;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        camGirl = GetComponent<Transform>();
        SetFS();
    }
    
    void FixedUpdate()
    {
        camGirl.position = followSpots[trackSpot + delay];
        UpdateFS();
        if (Input.GetKey(KeyCode.Minus))
        {
            ShrinkShot();
        } else if (Input.GetKey(KeyCode.Equals))
        {
            WidenShot();
        }
    }

    void SetFS() //Fills followSpots with Vectors for the start position
    {
        for (int i = 0; i < followSpots.Length; i++)
        {
            followSpots[i] = new Vector3(target.position.x, target.position.y, -10f);
        }
    }
    
    void UpdateFS() //Loops the target's current position into followSpots
    {
        for (int i = 1; i < followSpots.Length; i++)
        {
            followSpots[i] = followSpots[i - 1];
        }
        followSpots[0] = new Vector3(target.position.x, target.position.y, -10f);
    }

    void WidenShot()
    {
        if (cam.orthographicSize < camMaxZoom)
        {
            cam.orthographicSize += 0.1f;
        }
        else
        {
            cam.orthographicSize = camMaxZoom;
        }
    }

    void ShrinkShot()
    {
        if (cam.orthographicSize > camMinZoom)
        {
            cam.orthographicSize -= 0.1f;
        }
        else
        {
            cam.orthographicSize = camMinZoom;
        }
    }
}