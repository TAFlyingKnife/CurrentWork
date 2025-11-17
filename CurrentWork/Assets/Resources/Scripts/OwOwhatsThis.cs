using UnityEngine;

public class OwOwhatsThis : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private string[] texts;
    private bool talkable = false;

    public string[] GiveText()
    {
        return texts;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interact"))
        {
            talkable = true;
            texts = collision.gameObject.GetComponent<Interactable>().dialogue;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interact"))
        {
            talkable = false;
            texts = null;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 250, 300, 100), "Talkable: " + talkable);
    }
}