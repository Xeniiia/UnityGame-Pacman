using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;


public class PacmanPos : MonoBehaviour
{
    public Pacman pacman;

    public static bool FlagForPacmanStartPos = false;
    public static float startX;
    public static float startY;
    public static float WallSize;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlagForPacmanStartPos)
        {
            FlagForPacmanStartPos = false;
            AllData pacmanStartPosInfo = ClientNamespace.HandleNetworkInformation.message;
            PacmanStartLocation(pacmanStartPosInfo.StartPosClass.startPosX_proto, pacmanStartPosInfo.StartPosClass.startPosY_proto);
        }
    }

    private void PacmanStartLocation(int x, int y)
    {
        var position = new Vector2(startX + ((x-1) * WallSize), startY + ((y-1) * WallSize));
        pacman.transform.localPosition = position;

        //Сообщение, что можно продолжать
        ClientNamespace.Client.handleSignal.Set();
    }
}
