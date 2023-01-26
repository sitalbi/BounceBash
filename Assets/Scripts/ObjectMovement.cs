
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int movementSpeed;
    private GameManager gameManager;

    void Start() {
        movementSpeed = 10;
        Destroy(gameObject, 50f);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if (gameManager.inGame)
        {
            transform.position += Vector3.down * (movementSpeed * Time.deltaTime);
        }
    }
}