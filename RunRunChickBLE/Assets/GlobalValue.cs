using UnityEngine;

[System.Serializable]
public class GlobalValue
{
    [SerializeField]
    //public static int RpmDirection = 0;// 0=+ , 1=-
    public static int Rpm = 0; //have + , -
    public static int Watt = 0;
    public static string MachineState = "";
    public static int Level = 0;//阻力值

    //Game Message, Title Set Exercise
    public static int Map = 0;//0是白天/1是火山/2是冰原/3是沙漠/4是晚上
    public static int ParnetCharacter=1;//1~7
    public static int PlayerCharacter=1;//1~7
    public static int ChaserCharacter=1;//1~10


    //BLE link status
    public static string ServiceUUID = "";
    public static string TransforUUID = "";
    public static string _deviceAddress = "";

}
