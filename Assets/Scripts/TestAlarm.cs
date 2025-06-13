using UnityEngine;

public class TestAlarm : MonoBehaviour
{
    public Alarm alarmTest;

    void Update()
    {
        alarmTest.Tick(Time.deltaTime);
    }
}
