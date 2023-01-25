
using UnityEngine;

public class WallData : MonoBehaviour
{
    public int yDirection;

    [SerializeField] private Color blue;
    [SerializeField] private Color red;
    
    private SpriteRenderer image;

    void Start()
    {
        image = gameObject.GetComponent<SpriteRenderer>();
        image.color = yDirection == 1 ? blue : red;
    }

    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (image.color.Equals(blue))
        {
            image.color = red;
        } else if (image.color.Equals(red))
        {
            image.color = blue;
        }

        yDirection *= -1;
    }
}
