using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BLTCoreController : MonoBehaviour
{

    public string DeviceName = "";
    public string ServiceUUID = "0000ffe0-0000-1000-8000-00805f9b34fb";//設定好傳去GlobalValue
    public string TransforUUID = "0000ffe1-0000-1000-8000-00805f9b34fb";//設定好傳去GlobalValue

    enum States
    {
        None,
        Scan,
        ScanRSSI,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }

    private bool _connected = false;
    private float _timeout = 0f;
    private States _state = States.None;
    private string _deviceAddress;//設定好傳去GlobalValue
    private bool _foundTransforUUID = false;
    private bool _rssiOnly = false;
    private int _rssi = 0;

    public GameObject DeinitializeButton;//反初始化
    public GameObject BLECanvas;//遊戲初始選單
    public Text StatusText;
    public Text ButtonPositionText;
    public Text DeviceNameInputField;
    public bool StartGameBtnFlag=false;
    public GameObject StartGameBtn;
    private string StatusMessage
    {
        set
        {
            BluetoothLEHardwareInterface.Log(value);
            StatusText.text = value;
        }
    }

    public void OnDeinitializeButton()
    {
        BluetoothLEHardwareInterface.DeInitialize(() =>
        {
            StatusMessage = "反初始化";
        });
    }

    void Reset()
    {
        StartGameBtnFlag = false;
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _deviceAddress = null;
        _foundTransforUUID = false;
        _rssi = 0;
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    void StartProcess()
    {
        Reset();
        if (DeviceNameInputField.text.Trim().CompareTo("") != 0)
        {
            DeviceName = DeviceNameInputField.text.Trim();
            BluetoothLEHardwareInterface.Initialize(true, false, () =>
            {

                SetState(States.Scan, 0.1f);

            }, (error) =>
            {

                StatusMessage = "Error during initialize: " + error;
            });
        }
        else
        {
            StatusText.text = "尚未輸入裝置名稱";
        }

    }

    // Use this for initialization
    void Start()
    {
        if (DeinitializeButton != null)
        {
            DeinitializeButton.SetActive(false);
        }

        //StartProcess();
    }

    private void ProcessButton(byte[] bytes)
    {
        //if (bytes[0] == 0x00)
        //    ButtonPositionText.text = "Not Pushed";
        //else
        //    ButtonPositionText.text = "Pushed";
        ButtonPositionText.text = DecodeData(bytes);

    }



    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        break;

                    case States.Scan:
                        StatusMessage = "掃描: " + DeviceName;

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {

                            // if your device does not advertise the rssi and manufacturer specific data
                            // then you must use this callback because the next callback only gets called
                            // if you have manufacturer specific data

                            if (!_rssiOnly)
                            {
                                if (name.Contains(DeviceName))
                                {
                                    StatusMessage = "已找到: " + name;

                                    BluetoothLEHardwareInterface.StopScan();

                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, (address, name, rssi, bytes) =>
                        {

                            // use this one if the device responses with manufacturer specific data and the rssi

                            if (name.Contains(DeviceName))
                            {
                                StatusMessage = "已找到: " + name;

                                if (_rssiOnly)
                                {
                                    _rssi = rssi;
                                }
                                else
                                {
                                    BluetoothLEHardwareInterface.StopScan();

                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, _rssiOnly); // this last setting allows RFduino to send RSSI without having manufacturer data

                        if (_rssiOnly)
                            SetState(States.ScanRSSI, 0.5f);
                        break;

                    case States.ScanRSSI:
                        break;

                    case States.Connect:
                        StatusMessage = "連線中...";

                        // set these flags
                        _foundTransforUUID = false;

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) =>
                        {
                            StatusMessage = "連線中...";

                            if (IsEqual(serviceUUID, ServiceUUID))
                            {
                                StatusMessage = "服務設備已連線!!!";

                                _foundTransforUUID = _foundTransforUUID || IsEqual(characteristicUUID, TransforUUID);

                                // if we have found both characteristics that we are waiting for
                                // set the state. make sure there is enough timeout that if the
                                // device is still enumerating other characteristics it finishes
                                // before we try to subscribe
                                if (_foundTransforUUID)
                                {
                                    //GameCanvas.SetActive(true);//到此階段遊戲開始
                                    GlobalValue.ServiceUUID = ServiceUUID;
                                    GlobalValue.TransforUUID = TransforUUID;
                                    //GlobalValue._deviceAddress = _deviceAddress;
                                    _connected = true;
                                    SetState(States.Subscribe, 2f);
                                }
                            }
                        });
                        break;

                    case States.Subscribe:
                        StatusMessage = "協定溝通中...";//Subscribing to characteristics...

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, ServiceUUID, TransforUUID, (notifyAddress, notifyCharacteristic) =>
                        {
                            StatusMessage = "連線正常;等待開始...";//Waiting for user action (1)...
                            _state = States.None;

                            // read the initial state of the button
                            BluetoothLEHardwareInterface.ReadCharacteristic(_deviceAddress, ServiceUUID, TransforUUID, (characteristic, bytes) =>
                            {
                                ProcessButton(bytes);
                            });
                            if (StartGameBtnFlag == false)
                            {
                                StartGameBtn.SetActive(true);
                                StartGameBtnFlag = true;
                            }

                        }, (address, characteristicUUID, bytes) =>
                        {
                            if (_state != States.None)
                            {
                                // some devices do not properly send the notification state change which calls
                                // the lambda just above this one so in those cases we don't have a great way to
                                // set the state other than waiting until we actually got some data back.
                                // The esp32 sends the notification above, but if yuor device doesn't you would have
                                // to send data like pressing the button on the esp32 as the sketch for this demo
                                // would then send data to trigger this.
                                StatusMessage = "連線正常;等待開始...";//Waiting for user action (2)...

                                _state = States.None;
                            }

                            // we received some data from the device
                            ProcessButton(bytes);
                        });
                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, ServiceUUID, TransforUUID, null);
                        SetState(States.Disconnect, 4f);
                        break;

                    case States.Disconnect:
                        StatusMessage = "中斷連線.";

                        if (_connected)
                        {
                            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
                            {
                                StatusMessage = "已斷開連線.";
                                BluetoothLEHardwareInterface.DeInitialize(() =>
                                {
                                    _connected = false;
                                    _state = States.None;
                                });
                            });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface.DeInitialize(() =>
                            {
                                _state = States.None;
                            });
                        }
                        break;
                }
            }
        }
    }

    //LED測試
    private bool ledON = false;
    public void OnLED()
    {
        byte[] data = { 0x55, 0xA4, 0x01, 0x01, 0x00, 0x90 };
        SendBytes(data);
    }

    public void ScanAndConnect()
    {
        GlobalValue._deviceAddress = DeviceNameInputField.text;
        StartProcess();
    }
    /*
    public void StartGame_CityBike_UnityDemo()
    {
        byte[] data = { 0x55, 0xA4, 0x01, 0x01, 0x00, 0x90 };
        SendBytes(data);
        DontDestroyOnLoad(AllLinkObj);
        AllLinkObj_Plane.SetActive(false);
        WaitAnimation_Text.SetActive(true);
        GameCanvas.SetActive(false);
        StartCoroutine(LoadingAnimation());
    }
    */
    public void EnableComeData()//開啟資料回傳
    {
        byte[] data = { 0x55, 0xA7, 0x01, 0x01, 0x00, 0x90 };
        SendBytes(data);
    }
    public void DisableComeData()//關閉資料回傳
    {
        byte[] data = { 0x55, 0xA7, 0x01, 0x00, 0x00, 0x90 };
        SendBytes(data);
    }

    private string DecodeData(byte[] data)
    {

        byte cmdCode = data[1];
        StringBuilder stringBuilder = new StringBuilder(BitConverter.ToString(data));
        switch (cmdCode)
        {
            case 0x28:
                byte[] totalTimeMin = new byte[] { data[4], data[3] };
                int totalTimeSec = data[5];
                byte[] totalDistance = new byte[] { data[6], data[7] };
                byte[] totalCal = new byte[] { data[8], data[9] };
                int hr = data[10];
                int direction = data[11];
                byte[] rpm = new byte[] { data[13], data[12] };
                byte[] watt = new byte[] { data[15], data[14] };
                stringBuilder.Append("\n回傳訊息如上。");
                //stringBuilder.Append("rpm-byte:" + BitConverter.ToString(rpm));
                //print("傳入的rpm-byte:" + BitConverter.ToString(rpm));
                //print("傳入的rpm-value:" + BitConverter.ToInt16(rpm, 0));
                //print("傳入的watt-byte:" + BitConverter.ToString(watt));
                //print("傳入的watt-value:" + BitConverter.ToInt16(watt, 0));
                if (direction == 1)
                {
                    GlobalValue.Rpm = BitConverter.ToInt16(rpm, 0) * -1;
                }
                else
                {
                    GlobalValue.Rpm = BitConverter.ToInt16(rpm, 0);
                }
                stringBuilder.Append("rpm讀值測試:" + GlobalValue.Rpm);
                return stringBuilder.ToString();
        }
        return stringBuilder.ToString();
    }

    string FullUUID(string uuid)
    {
        string fullUUID = uuid;
        if (fullUUID.Length == 4)
            fullUUID = "0000" + uuid + "-0000-1000-8000-00805f9b34fb";

        return fullUUID;
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    public void SendBytes(byte[] data)
    {
        BluetoothLEHardwareInterface.WriteCharacteristic(_deviceAddress, ServiceUUID, TransforUUID, data, data.Length, true, (characteristicUUID) =>
        {

            BluetoothLEHardwareInterface.Log("Write Succeeded");
        });
    }
    //導入測試內容
    public void BLETestGame()
    {
        StartCoroutine(LoadingAnimation_RemoveBallGameTitle());
        //StartGameBtn.SetActive(false);
        //GameCanvas.SetActive(true);
        //BLECanvas.SetActive(false);
    }
    //返回連結內容
    public void GetBackBLELink()
    {
        StartGameBtn.SetActive(false);
        BLECanvas.SetActive(true);
        Reset();
        OnDeinitializeButton();
        SceneManager.LoadScene("BLELink");

    }
    //導入趣味遊戲選單
    public IEnumerator LoadingAnimation_RemoveBallGameTitle()
    {
        DontDestroyOnLoad(this.gameObject);

        /*byte Resistance_byte = Convert.ToByte(GlobalValue.Resistance * 10);
        byte[] start = { 0x55, 0xA4, 0x01, 0x01, 0x00, 0x90 };
        SendBytes(start);//傳驅動
        byte[] data = { 0x55, 0x44, 0x01, Resistance_byte, 0x00, 0x90 };
        SendBytes(data);//傳阻力值
        print("阻力設定:" + Resistance_byte);*/
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameTitle");

        /*AsyncOperation async = SceneManager.LoadSceneAsync("GameTitle");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
            pointtextobj1.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj2.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj3.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj4.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj5.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj6.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            pointtextobj1.SetActive(false);
            pointtextobj2.SetActive(false);
            pointtextobj3.SetActive(false);
            pointtextobj4.SetActive(false);
            pointtextobj5.SetActive(false);
            pointtextobj6.SetActive(false);
            print("進度:" + (async.progress * 100) + "%");
            if (async.progress >= 0.9f)
            {
                print("執行了趣味關卡");
                WaitAnimation_Text.SetActive(false);
                async.allowSceneActivation = true;
            }
        }*/
    }
}
