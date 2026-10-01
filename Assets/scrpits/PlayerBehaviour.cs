using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public Transform RespawnPoint;
    InputAction upButton;
    InputAction DownButton;
    InputAction RightButton;
    InputAction LeftButton;
    
    AudioSource GameFailed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = RespawnPoint.position;

        upButton = InputSystem.actions.FindAction("Up");
        DownButton = InputSystem.actions.FindAction("Down");
        RightButton = InputSystem.actions.FindAction("Right");
        LeftButton = InputSystem.actions.FindAction("Left");

        GameFailed = GetComponent<AudioSource>();
    }


    // Update is called once per frame
        void Update()
        {
            Vector3 playerPosition = transform.position;
            if (upButton.IsPressed())
            {
                playerPosition.y += moveSpeed * Time.deltaTime;
            }

            if (DownButton.IsPressed())
            {
                playerPosition.y -= moveSpeed * Time.deltaTime;
            }

            if (RightButton.IsPressed())
            {
                playerPosition.z += moveSpeed * Time.deltaTime;
            }

            if (LeftButton.IsPressed())
            {
                playerPosition.z -= moveSpeed * Time.deltaTime;
            }

            transform.position = playerPosition;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Destination")
            {
                Debug.Log("Destination!");
                transform.position = RespawnPoint.position;
                return;
            }
            
            GameFailed.Play();
            transform.position = RespawnPoint.position;
            Debug.Log("Crashed!!");
        }
    
}
