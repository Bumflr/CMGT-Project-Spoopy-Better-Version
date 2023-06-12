using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LoadEndCreditScene : MonoBehaviour
{

    public void StartEndCreditScene(){

        SceneManager.LoadSceneAsync("EndCreditScene");

    }
        
     
}
