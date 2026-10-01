using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    private float basespeed = 4.0f;
    public Transform RespawnPoint;
    
    public Transform PowerSpawnPoint;
   
    InputAction upButton;
    InputAction DownButton;
    InputAction RightButton;
    InputAction LeftButton;
    
    AudioSource GameFailed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basespeed = moveSpeed;
        
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
                moveSpeed = basespeed;
                
                GameObject powerObj = GameObject.Find("Power");
                
                powerObj.GetComponent<MeshRenderer>().enabled = true;
                powerObj.GetComponent<Collider>().enabled = true;
                powerObj.transform.position = PowerSpawnPoint.position;
                
                return;
            }

            if (other.name == "Power" || other.name.Contains("Power"))
            {
                Debug.Log("Power Up!");
                moveSpeed *= 2;
                return;
            }

            GameFailed.Play();
            transform.position = RespawnPoint.position;
            moveSpeed = basespeed;
            Debug.Log("Crashed!!");
            
        }
    
}
