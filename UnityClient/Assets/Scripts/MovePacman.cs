using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovePacman : MonoBehaviour
{
    public static bool FlagForMovePacman = false;

    public RectTransform Pacman;                  //����� ������� Image
    public Animator anim;

    //public float speed = 3.0f;
    public float wallSize = PacmanPos.WallSize;   //������ ������, � ����� ����������, �� ������� ����� ����������, ���� ���� ������ ������

    Vector2 direction;              //����������� ��������
    Vector2 destPos;                //������� ���� ���������
    Vector2 needPos;
    private bool GetDirection = false;


    void Update()
    {
        //���� �������� ������ � �����������
        if (FlagForMovePacman)
        {
                
        AllData pacmanMoveInfo = ClientNamespace.HandleNetworkInformation.message;

            //���� ������ ������ ��� ������������� �� ������ �����
            if (pacmanMoveInfo.MoveClass.can_step_proto)
            {

                Move(pacmanMoveInfo.MoveClass.direction_step_proto);

                //�������� ������ �������
                if (Pacman.anchoredPosition == needPos)
                {
                    GetDirection = false;

                    FlagForMovePacman = false;  //������� ������ � ���������

                    //���������, ��� ����� ���������� ����� ������ �� �������
                    ClientNamespace.Client.handleSignal.Set();
                }

            } else
            {
                FlagForMovePacman = false;  //������� ������ � ���������
                //���������, ��� ����� ���������� ����� ������ �� �������
                ClientNamespace.Client.handleSignal.Set();
            }
        } 


    }

    private void Move(string dir)
    {
        Rotate(dir);
        //���������� �����������:
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
