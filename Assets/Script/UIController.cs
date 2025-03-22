using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private UIDocument ui;
    void Start()
    {
        ui = GetComponent<UIDocument>();
        var buttonsContainer = ui.rootVisualElement.Q<VisualElement>("menu-container").Q<VisualElement>("menu-buttons");
        buttonsContainer.Q<Button>("Play").clicked += OnPlayClick;
        buttonsContainer.Q<Button>("Exit").clicked += OnExitClick;
        buttonsContainer.Q<Button>("Settings").clicked += OnSettingsClick;
        buttonsContainer.Q<Button>("Shop").clicked += OnShopClick;
    }

    private void OnShopClick()
    {
        throw new NotImplementedException();
    }

    private void OnSettingsClick()
    {
        throw new NotImplementedException();
    }

    private void OnExitClick()
    {
        throw new NotImplementedException();
    }

    private void OnPlayClick()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        var buttonsContainer = ui.rootVisualElement.Q<VisualElement>("menu-container").Q<VisualElement>("menu-buttons");
        buttonsContainer.Q<Button>("Play").clicked -= OnPlayClick;
        buttonsContainer.Q<Button>("Exit").clicked -= OnExitClick;
        buttonsContainer.Q<Button>("Settings").clicked -= OnSettingsClick;
        buttonsContainer.Q<Button>("Shop").clicked -= OnShopClick;
    }
}
