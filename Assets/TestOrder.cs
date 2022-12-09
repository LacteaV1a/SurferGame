using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrder : MonoBehaviour
{
    private OrderProcessing _orderProcessing;
    void Start()
    {
        _orderProcessing = new OrderProcessing(2,5);
    }

    [ContextMenu("GetId")]
    public void GetId()
    {
        if(_orderProcessing.IsFinished == false)
        {
        Debug.Log(_orderProcessing.GetId());

        }
        else
        {
            Debug.Log("Done");

        }
    }


}
