using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnKey : MonoBehaviour
{
    [SerializeField] KeyCode _targetKey = KeyCode.R;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_targetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
