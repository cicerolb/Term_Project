using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    // Scripts --
    PlayerMovement playerMovement;

    // Settings --
    [SerializeField] public float mouseSensitivity;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.mouseSensitivity = mouseSensitivity;

    }

    public void IncreaseSensitivity()
    {
        mouseSensitivity += 0.5f;
    }

    public void DecreaseSensitivity()
    {
        mouseSensitivity -= 0.5f;
    }
}
