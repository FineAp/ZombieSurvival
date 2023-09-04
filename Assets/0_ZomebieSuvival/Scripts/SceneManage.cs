using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{

    public void ToMain()
    {
        SceneManager.LoadScene("1__Main__");
    }

    public void ToGaime()
    {
        SceneManager.LoadScene("2__Game__");
    }

    public void ToExit()
    {
        Application.Quit();
    }
}
