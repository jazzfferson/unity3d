using JazzDev.Executor;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.UIElements.StyleSheets;
using System;
using System.Collections.Generic;


[CustomEditor(typeof(ExecutorManager))]
public class ExecutorManagerEditor : Editor
{
    private ExecutorManager executorManager;
    private VisualElement rootElement;
    private VisualTreeAsset visualTreeAsset;
    private VisualTreeAsset elementListVisualTree;
    private VisualTreeAsset elementInfoVisualTree;
    private ListView listView;

    private void OnEnable()
    {

        executorManager = target as ExecutorManager;
        executorManager.UpdateEditorWindowsEvent += ExecutorManager_UpdateEditorWindowsEvent;
        rootElement = new VisualElement();
        visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Framework/Executor/UI/ExecutorMangerInspector.uxml");
        elementListVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Framework/Executor/UI/ExecutorListElement.uxml");
        elementInfoVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Framework/Executor/UI/ExecutorInfoElement.uxml");
        var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Framework/Executor/UI/ExecutorMangerInspector.uss");
        rootElement.styleSheets.Add(uss);

    }

    private void ExecutorManager_UpdateEditorWindowsEvent()
    {
         if (executorManager != null) { UpdateListView(executorManager); }
    }

    public override VisualElement CreateInspectorGUI()
    {
        rootElement.Clear();
        visualTreeAsset.CloneTree(rootElement);
        Label executorsCount = rootElement.Q<Label>("executors-count-label");
        executorsCount.BindProperty(serializedObject.FindProperty("executorsCount"));
        listView = rootElement.Q<ListView>("main-list-view");

        if (executorManager != null) { UpdateListView(executorManager); }

        return rootElement;
    }



    private void UpdateListView(ExecutorManager manager)
    {
        listView.Clear();

        for (int i = 0; i < manager.TimersList.Count; i++)
        {
            var executor = manager.TimersList[i];
            var visualItem = elementListVisualTree.CloneTree();
            var infoPanel = visualItem.Q<VisualElement>("executor-info");
            infoPanel.Clear();

            /*   infoPanel.Add(AddNewInfoElement("Id:", executor.Id));
               infoPanel.Add(AddNewInfoElement("Default Delay:", executor.Parameters.defaultDelay.ToString()));
               infoPanel.Add(AddNewInfoElement("Duration:", executor.Parameters.duration.ToString()));*/

            var parms = executor.ParametersList;

            for (int j = 0; j < parms.Count; j++)
            {
                var info = parms[j].GetInfo();
                infoPanel.Add(AddNewInfoElement(info.name, info.value));
            }

            listView.Insert(i, visualItem);
        }


        rootElement.Add(listView);
    }

    private VisualElement AddNewInfoElement(string title, string value)
    {
        var newInfo = elementInfoVisualTree.CloneTree();
        newInfo.Q<Label>("info-item-title").text = title;
        newInfo.Q<Label>("info-item-value").text = value;
        return newInfo;
    }
}

