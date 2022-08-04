using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName = "";
    [SerializeField] GameObject[] targets;
    [SerializeField] int liveCnd;

    //liveCnd 0:ëSñ≈, 1:àÍêlÇ≈Ç‡åáÇØÇΩÇÁ

    void SceneChange()
    {
        int nullcnt = 0;
        foreach (GameObject g in targets)
        {
            if (g == null)
            {
                nullcnt++;
                if (liveCnd == 1)
                {
                    SceneManager.LoadScene(sceneName);
                }
                
            }
        }
        if (liveCnd == 0 && nullcnt == targets.Length)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SceneChange();
    }

}
