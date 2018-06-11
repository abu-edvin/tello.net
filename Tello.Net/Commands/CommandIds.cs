namespace Tello.Net.Commands
{
    public enum TelloCommandId : ushort
    {
        Connect = 1,
        ConnectAck = 2,
        ReqVideoSpsPps = 37,
        TakePicture = 48,
        SwitchPictureVideo = 49,
        SetEv = 52,
        DateTime = 70,
        Stick = 80,
        LogHeaderWrite = 4176,
        LogDataWrite = 4177,
        LogConfiguration = 4178,
        WifiSignal = 26,
        VideoBitRate = 40,
        LightStrength = 53,
        VersionString = 69,
        ActivationTime = 71,
        LoaderVersion = 73,
        Status = 86,
        GetAltLimit = 4182,
        SetJpegQuality = 55,
        TakeOff = 84,
        Land = 85,
        SetAltLimit = 88,
        Flip = 92,
        Throw = 93,
        PalmLand = 94,
        PlaneCalibration = 4180,
        GetLowBattThreshold = 4183,
        SetLowBattThreshold = 4181,
        GetAttitudeAngle = 4185,
        SetAttitudeAngle = 4184,
        Error1 = 67,
        Error2 = 68,
        FileSize = 98,
        FileData = 99,
        FileComplete = 100,
        HandleImuAngle = 90,
        SetVideoBitRate = 32,
        SetDynAdjRate = 33,
        SetEis = 36,
        SmartVideoStart = 128,
        SmartVideoStatus = 129,
        Bounce = 4179
    }

    public enum TelloSmartVideo
    {
        Stop = 0,
        Start = 1,
        r360 = 1,
        circle = 2,
        upOut = 3
    }

    public enum TelloPacketType
    {
        Connection = 0x00,
        Settings = 0x48,
        DateTime = 0x50,
        Control = 0x60,
        Special = 0x68
    }

    internal enum IgnoredCommands
    {
        GetSsid = 17,
        SetSsid = 18,
        GetWifiPassword = 19,
        SetWifiPassword = 20,
        GetRegion = 21,
        SetRegion = 22,
    }
}
