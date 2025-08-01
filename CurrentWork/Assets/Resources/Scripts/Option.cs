using UnityEngine;

public class Option : MonoBehaviour
{

    public string option;
    public int power;

    public int value;

    public int OrientationDoNotTouch;
    
    public Sprite icon;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = icon;
    }

    public void SetDefault()
    {
        option = "Default"; // Name of the selected option.
        
        power = 1; // Increases when upgraded by a player.
        
        value = 25; // 25% of base enemy/player HP by default, change as necessary.
    }

}
