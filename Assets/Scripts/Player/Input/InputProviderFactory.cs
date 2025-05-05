using UnityEngine;
using YG;

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
#if UNITY_EDITOR
        return new KeyboardInputProvider();
#else
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return new KeyboardInputProvider();

        if (YandexGame.savesData == null || YandexGame.EnvironmentData == null || YandexGame.EnvironmentData.isDesktop)
            return new KeyboardInputProvider();

        if ((YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet) && _touchInputProviderPrefab != null)
            return Object.Instantiate(_touchInputProviderPrefab);

        return new KeyboardInputProvider();
#endif
    }
}