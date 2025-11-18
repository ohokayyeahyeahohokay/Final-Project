using UnityEngine;
using UnityEngine.UI;

public class PlayerCharges : MonoBehaviour
{
    public int maxCharges = 3;
    public float rechargeTime = 2f;

    public Image[] chargeIcons; 
    private float[] timers;
    private int charges;

    void Start()
    {
        charges = maxCharges;
        timers = new float[maxCharges];
        UpdateUI();
    }

    void Update()
    {
        HandleRecharge();
    }

    public bool UseCharge()
    {
        if (charges <= 0)
            return false;

        charges--;
        timers[charges] = rechargeTime;
        UpdateUI();
        return true;
    }

    void HandleRecharge()
    {
        for (int i = 0; i < maxCharges; i++)
        {
            if (i >= charges)
            {
                timers[i] -= Time.deltaTime;

                if (timers[i] <= 0f)
                {
                    charges++;
                    UpdateUI();
                }
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < maxCharges; i++)
        {
            if (i < charges)
                chargeIcons[i].color = Color.green;
            else
                chargeIcons[i].color = Color.gray;
        }
    }
}
