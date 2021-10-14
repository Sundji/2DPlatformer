using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLSManager : MonoBehaviour
{

    private static SLSManager selfSLSM;

    public static SLSManager SLSM
    {
        get
        {
            
            if (selfSLSM == null)
            {
                selfSLSM = FindObjectOfType<SLSManager>();
                if (selfSLSM != null)
                    selfSLSM.LoadBestTime();
            }

            return selfSLSM;

        }
    }
    

    private float bestTime;

    public float BestTime
    {
        get
        {
            return bestTime;
        }
    }

    private void Awake()
    {

        if (selfSLSM == null)
            selfSLSM = this;
        
        if (selfSLSM != this)
            Destroy(gameObject);

        LoadBestTime();

    }

    public void LoadBestTime()
    {
        bestTime = BinarySerializer.LoadTime();
    }

    public void SaveBestTime(float time)
    {
        BinarySerializer.SaveTime(time);
        LoadBestTime();
    }

}
