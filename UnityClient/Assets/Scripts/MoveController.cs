using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System.Reflection;

namespace ClientNamespace
{

    public class MoveController : MonoBehaviour
    {
        // 0 - L
        // 1 - R
        // 2 - U
        // 3 - D
        private bool[] pressedButtons = new bool[4];
        private static readonly string[] pressedButName = { "L", "R", "U", "D" };
        private static string lastButton = "L";
        public static bool can_step = true;

        void Update()
        {
            bool Bobol = false;
            for (int i = 0; i < 4; i++) pressedButtons[i] = false;

            AllData pacmanMoveInfo = ClientNamespace.HandleNetworkInformation.message;

            if (!MovePacman.FlagForMovePacman) //Если движения нет
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (lastButton == "L") Bobol = true;
                    else pressedButtons[0] = true;

                }

                if (Input.GetKey(KeyCode.D))
                {
                    if (lastButton == "R") Bobol = true;
                    else pressedButtons[1] = true;

                }

                if (Input.GetKey(KeyCode.W))
                {
                    if (lastButton == "U") Bobol = true;
                    else pressedButtons[2] = true;

                }

                if (Input.GetKey(KeyCode.S))
                {
                    if (lastButton == "D") Bobol = true;
                    else pressedButtons[3] = true;

                }

                if (Bobol && can_step)
                {
                    MoveProtobufClass moveClass = new MoveProtobufClass(lastButton);
                    AllData allDataMoveClass = new AllData(moveClass);
                    byte[] serializeMoveClass = ProtoSerialize.Serialize<AllData>(allDataMoveClass);
                    Client.SendDataTo(serializeMoveClass);
                }
                else
                {
                    
                    string saveSimvol = "";
                        switch (lastButton)
                        {
                            case "L":
                                if (pressedButtons[1] == true)
                                {
                                    saveSimvol = "R";
                                }
                                break;

                            case "R":
                                if (pressedButtons[0] == true)
                                {
                                    saveSimvol = "L";
                                }
                                break;

                            case "U":
                                if (pressedButtons[3] == true)
                                {
                                    saveSimvol = "D";
                                }
                                break;

                            case "D":
                                if (pressedButtons[2] == true)
                                {
                                    saveSimvol = "U";
                                }
                                break;
                        }
                    if (saveSimvol == "")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (pressedButtons[i] == true)
                            {
                                MoveProtobufClass moveClass = new MoveProtobufClass(pressedButName[i]);
                                AllData allDataMoveClass = new AllData(moveClass);
                                byte[] serializeMoveClass = ProtoSerialize.Serialize<AllData>(allDataMoveClass);
                                Client.SendDataTo(serializeMoveClass);
                                lastButton = pressedButName[i];
                                break;
                            }
                        }
                    }
                    else
                    {
                        MoveProtobufClass moveClass = new MoveProtobufClass(saveSimvol);
                        AllData allDataMoveClass = new AllData(moveClass);
                        byte[] serializeMoveClass = ProtoSerialize.Serialize<AllData>(allDataMoveClass);
                        Client.SendDataTo(serializeMoveClass);
                        lastButton = saveSimvol;
                    }
                }
            }
        }
    }

}
