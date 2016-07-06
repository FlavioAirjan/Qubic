using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SettingMenuScript : MonoBehaviour {

    public GameObject Alert;
    public GameObject InputFieldText;
    public GameObject QubicInfo;

    
    public void EasyGame()
    {
        QubicInfo.GetComponent<QubicInfo>().depth = 1;
        try
        {
            QubicInfo.GetComponent<QubicInfo>().cubeSize  = Int32.Parse(InputFieldText.GetComponent<Text>().text);
        }
        catch
        {
            Alert.SetActive(true);
            return;
        }
        if (QubicInfo.GetComponent<QubicInfo>().cubeSize >= 3)
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
        QubicInfo.GetComponent<QubicInfo>().depth = 3;
        try
        {
            QubicInfo.GetComponent<QubicInfo>().cubeSize = Int32.Parse(InputFieldText.GetComponent<Text>().text);
            
        }
        catch
        {
            Alert.SetActive(true);
            return;
        }
        if (QubicInfo.GetComponent<QubicInfo>().cubeSize >= 3)
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
        QubicInfo.GetComponent<QubicInfo>().depth = 5;
        try
        {
            QubicInfo.GetComponent<QubicInfo>().cubeSize = Int32.Parse(InputFieldText.GetComponent<Text>().text);
           
        }
        catch
        {
            Alert.SetActive(true);
            return;
        }
        if (QubicInfo.GetComponent<QubicInfo>().cubeSize >= 3)
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
