using Assets.Resources.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameStart : MonoBehaviour, ICheckHandler {

    public bool IsOK()
    {
        return true;
    }
}
