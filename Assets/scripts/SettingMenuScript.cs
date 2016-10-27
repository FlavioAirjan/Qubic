using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SettingMenuScript : MonoBehaviour {

    public GameObject Alert;
    public GameObject InputFieldText;
    public GameObject QubicInfo;
    public Toggle Tres;
    public Toggle quatro;
    public Toggle cinco;

     void Start()
     {
            
     }
    public void EasyGame()
    {
        int numC=3;
        if (Tres.isOn)
        {
            numC = 3;
        }else if (quatro.isOn)
        {
            numC = 4;
        } else if (cinco.isOn)
        {
            numC = 5;
        }

        try
        {
            QubicInfo = GameObject.Find("QubicInfos");
            QubicInfo.GetComponent<QubicInfos>().cubeSize = numC;
            QubicInfo.GetComponent<QubicInfos>().depth = 3;

        }
        catch
        {
            Debug.Log("QubicInfoNotFound");
        }
        
        if (QubicInfo.GetComponent<QubicInfos>().cubeSize >= 3)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Alert.SetActive(true);
        }
    }

    public void MediumGame()
    {
        QubicInfo.GetComponent<QubicInfos>().depth = 3;
        try
        {
            QubicInfo.GetComponent<QubicInfos>().cubeSize = Int32.Parse(InputFieldText.GetComponent<Text>().text);
            
        }
        catch
        {
            Alert.SetActive(true);
            return;
        }
        if (QubicInfo.GetComponent<QubicInfos>().cubeSize >= 3)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Alert.SetActive(true);
        }
    }
    public void HardGame()
    {
        QubicInfo.GetComponent<QubicInfos>().depth = 5;
        try
        {
            QubicInfo.GetComponent<QubicInfos>().cubeSize = Int32.Parse(InputFieldText.GetComponent<Text>().text);
           
        }
        catch
        {
            Alert.SetActive(true);
            return;
        }
        if (QubicInfo.GetComponent<QubicInfos>().cubeSize >= 3)
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Alert.SetActive(true);
        }
    }
    public void GoBack()
    {
       
        SceneManager.LoadScene("StartMenu");
    }
}
