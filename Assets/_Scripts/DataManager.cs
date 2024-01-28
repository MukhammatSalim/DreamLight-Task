using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveLoadJSON : MonoBehaviour
{
    string saveFilePath;
    public UIManager uIManager;
    public bool DebugMode;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/PanelData.json";
    }
    public void SaveGame()
    {
        GameObject[] allPanels = GameObject.FindGameObjectsWithTag("UI_Element");
        List<PanelData> allPanelsData = new List<PanelData>();
        foreach (GameObject panel in allPanels)
        {
            allPanelsData.Add(ConvertPanelToData(panel));
        }
        PanelData[] panelDatas = allPanelsData.ToArray();

        string savedPanelDataString = JsonHelper.ToJson(panelDatas, true);
        File.WriteAllText(saveFilePath, savedPanelDataString);

        if(DebugMode) Debug.Log("Number of saved panels" + allPanels.Length);
        if(DebugMode) Debug.Log("Save file created at: " + saveFilePath);
        if(DebugMode) Debug.Log("Final data is " + savedPanelDataString);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPanelData = File.ReadAllText(saveFilePath);
            PanelData[] panelDatas = JsonHelper.FromJson<PanelData>(loadPanelData);
            uIManager.ClearAllPanels();
            for (int i = 0; i < panelDatas.Length; i++)
            {
                uIManager.LoadPanel(panelDatas[i]);
            }

        }
        else
            if(DebugMode) Debug.Log("There is no save files to load!");

    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            if(DebugMode) Debug.Log("Save file deleted!");
        }
        else
            if(DebugMode) Debug.Log("There is nothing to delete!");
    }
    PanelData ConvertPanelToData(GameObject panel)
    {
        PanelData newPanelData = new PanelData();
        newPanelData.Index = panel.transform.GetSiblingIndex();
        if (uIManager.IsLeftContent(panel)) newPanelData.side = "left";
        else newPanelData.side = "right";
        newPanelData.PanelText = panel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
        newPanelData.PanelNumber = panel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text;
        return newPanelData;
    }
}
[Serializable]
public class PanelData
{
    public int Index;
    public string side;
    public string PanelText;
    public string PanelNumber;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}