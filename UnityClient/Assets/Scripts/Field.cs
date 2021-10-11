using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    [Header("Field Poperties")]
    public float WallSize;
    public float FieldSize;

    [Space(10)]
    public Wall wallPref;
    public RectTransform rt;

    private Wall[,] field;
    public static bool FlagForLevelInfoHandle = false;


    void Update()
    {
        try
        {
            if (FlagForLevelInfoHandle)
            {
                FlagForLevelInfoHandle = false;
                AllData levelInfo = ClientNamespace.HandleNetworkInformation.message;
                CreateField(levelInfo.LevelClass.sizeX_proto, levelInfo.LevelClass.sizeY_proto, levelInfo.LevelClass.wall);
            }
        } catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void CreateField(int FieldSizeX, int FieldSizeY, List<ClientNamespace.Point> listWalls)
    {
        field = new Wall[FieldSizeX, FieldSizeY];

        float fieldWidth = FieldSizeX * WallSize;
        float fieldHight = FieldSizeY * WallSize;
        rt.sizeDelta = new Vector2(fieldWidth, fieldHight);

        float startX = -(fieldWidth / 2) + (WallSize / 2);      
        float startY = -(fieldHight / 2) + (WallSize / 2);      

        //Заполнить поля для StartPosition
        PacmanPos.startX = startX;
        PacmanPos.startY = startY;
        PacmanPos.WallSize = WallSize;
        //-------------------------------

        int length = listWalls.ToArray().Length;
        for (int i = 0; i < length; i++)
        {

            var wall = Instantiate(wallPref, transform, false);
            var position = new Vector2(startX + (listWalls[i].x * WallSize), startY + (listWalls[i].y * WallSize));
            wall.transform.localPosition = position;

            field[listWalls[i].x, listWalls[i].y] = wall;
            wall.SetValue(listWalls[i].x, listWalls[i].y);
        }

        //Сообщение, что можно продолжать
        ClientNamespace.Client.handleSignal.Set();
    }
}
