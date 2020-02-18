using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransaction : MonoBehaviour
{
    private static SceneTransaction _instance;
    public static SceneTransaction Instance{
        get{
            return _instance;
        }
    }
    private void Start()
    {
        if(_instance == null || _instance != this){
            _instance = this;
        }
    }
    public Animator anim;
    public void SceneLoad(string index){
        StartCoroutine(LoadScene(index));
    }
    IEnumerator LoadScene(string scene){
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
}
