using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string level;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)-Camera.main.transform.position;
            
            //关卡按钮的判定半径
            if ((mousePosition - transform.position).magnitude < 0.5f)
            {
                SceneManager.LoadScene(level);
            }
        }
    }
}
