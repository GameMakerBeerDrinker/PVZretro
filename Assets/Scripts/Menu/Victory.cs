using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;

            //�ж��뾶
            if ((mousePosition - transform.position).magnitude < 0.5f)
            {
                //TODO:ʤ���ݳ�

                SceneManager.LoadScene("LevelSelectScene");
            }
        }
    }
}
