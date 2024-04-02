using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Scripts --
    PlayerSettings playerSettings;

    [SerializeField] TextMeshProUGUI sensitivity;
    [SerializeField] float sensitivityValue;

    void Awake()
    {
        playerSettings = GameObject.FindGameObjectWithTag("PlayerSettings").GetComponent<PlayerSettings>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sensitivityValue = playerSettings.mouseSensitivity;
        sensitivity.text = sensitivityValue.ToString();
    }
}
