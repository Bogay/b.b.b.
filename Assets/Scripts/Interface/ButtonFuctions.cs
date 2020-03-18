using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuctions : MonoBehaviour
{
    /*這個class內涵蓋所有可能的場景轉換會用到的函式*/
    /*社課教學不會修改到這個class*/

    public void Quit() //退出遊戲
    {
        Application.Quit();
    }

    public void ToMain() //進到場景1
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1; //加這行是因為有可能在場景轉換前時間常數是0
    }

    public void ToMenu() //進到場景0
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; //加這行是因為有可能在場景轉換前時間常數是0
    }
}
