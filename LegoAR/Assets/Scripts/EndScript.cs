using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("finishFunc", 5);
    }

    private void finishFunc()
    {
        SceneManager.LoadScene("MainWindow");
    }
}
