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

    public static int ExerciseTime=180;
    public static int MyCoin=0;


    //BLE link status
    public static string ServiceUUID = "";
    public static string TransforUUID = "";
    public static string _deviceAddress = "";

    public static int Resistance = 0; //阻力值
     /*
     調整阻力值方法:
     private void SendBytes(byte[] data)
     {
        BluetoothLEHardwareInterface.WriteCharacteristic(GlobalValue._deviceAddress, GlobalValue.ServiceUUID, GlobalValue.TransforUUID, data, data.Length, true, (characteristicUUID) =>
        {

            BluetoothLEHardwareInterface.Log("Write Succeeded");
        });
     }

     byte Resistance_byte = Convert.ToByte(GlobalValue.Resistance * 10);
     byte[] over = { 0x55, 0xA4, 0x01, 0x00, 0x00, 0x90 };
     SendBytes(over);//傳驅動關閉
     */
}
