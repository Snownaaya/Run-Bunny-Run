using UnityEngine;

public class InputProviderFactory
{
    private static TouchInputProvider _touchInputProviderPrefab;

    static InputProviderFactory()
    {
        _touchInputProviderPrefab = Resources.Load<TouchInputProvider>("Prefabs/TouchInputProvider");
        if (_touchInputProviderPrefab == null)
            Debug.LogWarning("TouchInputProvider prefab not found in Resources/Prefabs!");
    }

    public static IInputProvider GetInputProvider()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (SystemInfo.deviceType == DeviceType.Handheld && _touchInputProviderPrefab != null)
                return Object.Instantiate(_touchInputProviderPrefab);
            else
                return new KeyboardInputProvider();
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld && _touchInputProviderPrefab != null)
        {
            return Object.Instantiate(_touchInputProviderPrefab);
        }
        else
        {
            return new KeyboardInputProvider();
        }
    }
}