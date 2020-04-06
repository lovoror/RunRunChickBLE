using UnityEngine;

[System.Serializable]
public class GlobalValue
{
    [SerializeField]
    //public static int RpmDirection = 0;// 0=+ , 1=-
    public static int Rpm = 0; //have + , -
    public static int Watt = 0;
    public static string MachineState = "";
    public static int Level = 0;

    //BLE link status
    public static string ServiceUUID = "";
    public static string TransforUUID = "";
    public static string _deviceAddress = "";
}
