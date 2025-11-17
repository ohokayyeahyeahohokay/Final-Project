using UnityEngine;
using UnityEngine.UI;

public class MagnetCharges : MonoBehaviour
{
     public int maxCharges = 3;
    public float rechargeTime = 2f;

    public Image[] chargeIcons;

    private float[] chargeTimers;
    private bool[] chargeAvailable;

    void Start()
    {
        chargeTimers = new float[maxCharges];
        chargeAvailable = new bool[maxCharges];

        for (int i = 0; i < maxCharges; i++)
        {
            chargeAvailable[i] = true;
        }

        UpdateUI();
    }

    void Update()
    {
        for (int i = 0; i < maxCharges; i++)
        {
            if (!chargeAvailable[i]) 
            {
                chargeTimers[i] -= Time.deltaTime;

                if (chargeTimers[i] <= 0)
                {
                    chargeAvailable[i] = true;
                    UpdateUI();
                }
            }
        }
    }

    public bool UseCharge()
    {
        for (int i = maxCharges - 1; i >= 0; i--)
        {
            if (chargeAvailable[i])
            {
                chargeAvailable[i] = false;
                chargeTimers[i] = rechargeTime;

                UpdateUI();
                return true;
            }
        }

        return false; 
    }

    void UpdateUI()
    {
        for (int i = 0; i < maxCharges; i++)
        {
            if (chargeAvailable[i])
                chargeIcons[i].color = Color.cyan;   
            else
                chargeIcons[i].color = Color.gray;   
        }
    }
}
