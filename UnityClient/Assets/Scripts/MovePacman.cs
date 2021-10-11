using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovePacman : MonoBehaviour
{
    public static bool FlagForMovePacman = false;

    public RectTransform Pacman;                  //Чтобы двигать Image
    public Animator anim;

    //public float speed = 3.0f;
    public float wallSize = PacmanPos.WallSize;   //размер ячейки, а также расстояние, на которое нужно сдвинуться, если была нажата кнопка

    Vector2 direction;              //направление движения
    Vector2 destPos;                //позиция куда двигаемся
    Vector2 needPos;
    private bool GetDirection = false;


    void Update()
    {
        //Если получены данные о перемещении
        if (FlagForMovePacman)
        {
                
        AllData pacmanMoveInfo = ClientNamespace.HandleNetworkInformation.message;

            //Если сервер вернул что передвинуться на клетку можно
            if (pacmanMoveInfo.MoveClass.can_step_proto)
            {

                Move(pacmanMoveInfo.MoveClass.direction_step_proto);

                //достигли нужной позиции
                if (Pacman.anchoredPosition == needPos)
                {
                    GetDirection = false;

                    FlagForMovePacman = false;  //убираем сигнал о получении

                    //Сообщение, что можно продолжать прием данных от сервера
                    ClientNamespace.Client.handleSignal.Set();
                }

            } else
            {
                FlagForMovePacman = false;  //убираем сигнал о получении
                //Сообщение, что можно продолжать прием данных от сервера
                ClientNamespace.Client.handleSignal.Set();
            }
        } 


    }

    private void Move(string dir)
    {
        Rotate(dir);
        //Определить направление:
        if (!GetDirection)
        {
            GetDirectionAndDestPos(dir);
            GetDirection = true;
        }
        Vector2 p = new Vector2((destPos.x * 0.5f), (destPos.y * 0.5f));
        Pacman.offsetMin += p;
        Pacman.offsetMax -= -1 * p;
    }



    private void GetDirectionAndDestPos(string dir)
    {
        switch (dir)
        {
            case "L":
                direction = Vector3.left;
                destPos = direction * wallSize;
                needPos = Pacman.anchoredPosition + destPos;
                break;

            case "R":
                direction = Vector3.right;
                destPos = direction * wallSize;
                needPos = Pacman.anchoredPosition + destPos;
                break;

            case "U":
                direction = Vector3.up;
                destPos = direction * wallSize;
                needPos = Pacman.anchoredPosition + destPos;
                break;

            case "D":
                direction = Vector3.down;
                destPos = direction * wallSize;
                needPos = Pacman.anchoredPosition + destPos;
                break;
        }
    }

    private void Rotate(string dir)
    {
        switch (dir)
        {
            case "L":
                Pacman.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;

            case "R":
                Pacman.transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;

            case "U":
                Pacman.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;

            case "D":
                Pacman.transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }
}
