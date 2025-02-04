using UnityEngine;

public class Battery : MonoBehaviour
{
    float BatteryCharge;

    float FlashlightRate = 10.0f;

    void Start()
    {
        BatteryCharge = 1000.0f;    // full charge
    }

    void Update()
    {
        float usageThisFrame = FlashlightRate * Time.deltaTime;

        BatteryCharge -= usageThisFrame;

        Debug.Log("Charge remaining: " + BatteryCharge);  // spam the charge out

        if (BatteryCharge <= 0)
        {
            // do whatever you want when the battery runs out.
        }
    }
}
