using System.Collections;
using UnityEngine;

public class HandleRoadSpeed : MonoBehaviour
{
    private RoaderStorage _storage;

    private float _delay = 2f;
    private float _speedIncrement = 1f;
    private float _globalSpeed = 100f;

    private void Awake() =>
        _storage = GetComponent<RoaderStorage>();

    private void Start() =>
        StartCoroutine(IncreaseSpeedRoutine());

    public void SetCurrentSpeed(Roader roader)
    {
        roader.CurrentSpeed = GetCurrentSpeed();
        Debug.Log($"Задаю скорость {roader.CurrentSpeed} для {roader.gameObject.name}");
    }


    public void DecreaseSpeed(float decrement)
    {
        _globalSpeed -= decrement;

        foreach (Roader roader in _storage.ActiveRoads)
            roader.CurrentSpeed = _globalSpeed;
    }

    public void ResetSpeed(float defaultSpeed)
    {
        _globalSpeed = defaultSpeed;

        foreach (Roader roader in _storage.ActiveRoads)
            roader.CurrentSpeed = _globalSpeed;
    }

    private IEnumerator IncreaseSpeedRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_delay);
            IncreaseSpeed(_speedIncrement);
        }
    }

    private void IncreaseSpeed(float increment)
    {
        _globalSpeed += increment;

        foreach (Roader roader in _storage.ActiveRoads)
            roader.CurrentSpeed = _globalSpeed;
    }

    private float GetCurrentSpeed() =>
            _globalSpeed;
}
