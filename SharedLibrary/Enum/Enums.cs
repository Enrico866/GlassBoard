using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Enum
{
    public class Enums
    {
        public enum SortField { Id, Nome, DataScadenza }
        public enum TipiPeriodicitaContratti
        {
            Mensile = 1,
            Ingressi = 2,
            ND = 0
        }

        public enum TipoTabella
        {
            Esterna,
            Interna
        }

        public enum InstrumentTypes
        {
            Snmp = 0,
            Ssh = 1,
            Telnet = 2,
            Syslog = 3,
            Api = 4,
            Ping = 5,
            Grpc = 6,
            InstrumentedService = 7
        }

        public enum AccessScopeTypes
        {
            MonitoringService = 1,
            AdminUser = 2,
            NocUser = 3,
            OrganizationUser = 4
        }

        public enum ResourceTypes
        {
            /// <summary>Unknown node type.</summary>
            Unknown = 0,

            /// <summary>Generic network device.</summary>
            NetworkDevice = 1,

            /// <summary>Network router.</summary>
            Router = 2,

            /// <summary>Network switch.</summary>
            Switch = 3,

            /// <summary>Firewall device for network security.</summary>
            Firewall = 4,

            /// <summary>Wireless LAN Controller (WLC).</summary>
            Wlc = 5,

            /// <summary>Load balancer device.</summary>
            Balancer = 6,

            /// <summary>Wireless access point.</summary>
            AccessPoint = 7,

            /// <summary>Uninterruptible Power Supply (UPS).</summary>
            Ups = 8,

            /// <summary>Wireless network device.</summary>
            WiFi = 9,

            /// <summary>Closed Circuit Television (CCTV) system.</summary>
            Tvcc = 10,

            /// <summary>Voice over IP (VoIP) system.</summary>
            VoIP = 11,

            /// <summary>Virtual Private Network (VPN) system.</summary>
            Vpn = 12,

            /// <summary>Base Transceiver Station (BTS) for mobile networks.</summary>
            BaseTransceiverStation = 13,

            /// <summary>Radio antenna for communication.</summary>
            RadioAntenna = 14,

            /// <summary>Internet of Things (IoT) device.</summary>
            Iot = 15,

            /// <summary>Operational Technology (OT) system.</summary>
            Ot = 16,

            /// <summary>Compute cluster.</summary>
            Cluster = 17,

            /// <summary>Physical or virtual server.</summary>
            Server = 18,

            /// <summary>On-premises service.</summary>
            OnpremService = 19,

            /// <summary>Cloud-based service.</summary>
            CloudService = 20,

            /// <summary>Software application.</summary>
            Application = 21,

            /// <summary>CPU (Central Processing Unit).</summary>
            Cpu = 22,

            /// <summary>Storage unit or disk array.</summary>
            Storage = 23,

            /// <summary>Network interface device.</summary>
            NetworkInterface = 24,

            /// <summary>RAM (Random Access Memory) module.</summary>
            Memory = 25,

            /// <summary>Disk or storage drive.</summary>
            Disk = 26,

            /// <summary>End-user device such as a workstation or PC.</summary>
            EndPoint = 27,

            /// <summary>Cisco Meraki device.</summary>
            Meraki = 28,

            /// <summary>Rubrik backup and recovery appliance.</summary>
            Rubrik = 29,

            /// <summary>Cohesity storage and backup solution.</summary>
            Cohesity = 30,

            /// <summary>Round trip time.</summary>
            Rtt = 31,

            /// <summary>Temperature sensor</summary>
            TemperatureSensor = 32,

            /// <summary>Power supply unit sensor</summary>
            PsuSensor = 33,

            /// <summary>Fan sensor</summary>
            FanSensor = 34,

            /// <summary>Stack of devices.</summary>
            Stack = 35,

            /// <summary>Virtual stack of devices.</summary>
            StackVirtual = 36,
            PhysicalComponent = 37,
            Ospf = 38,
            UpsSensor = 39,
            Psu = 40,
            VoltageSensor = 41
        }

    }

    public enum SecretScopeTypes
    {
        MonitoringService = 0,
        ReadOnlyUser = 1,
        ReadWriteUser = 2,
        PamUser = 3,
        PamService = 4,
        NetConfig = 5
    }

    public enum AsymmetricAlgorithmTypes
    {
        Rsa = 0,
        Dsa = 1,
        DiffieHellman = 2,
        Ed25519 = 3,
        Ed448 = 4,
        X25519 = 5,
        X448 = 6
    }

    public enum SnmpV3AuthProtocolTypes
    {
        Sha = 0,
        Md5 = 1,
        Sha224 = 2,
        Sha256 = 3,
        Sha384 = 4,
        Sha512 = 5
    }

    public enum SecurityLevelTypes
    {
        NoAuthNoPriv = 0,
        AuthNoPriv = 1,
        AuthPriv = 2
    }

    public enum SnmpV3PrivacyProtocolTypes
    {
        Aes = 0,
        Des = 1,
        Aes192 = 2,
        Aes192C = 3,
        Aes256 = 4,
        Aes256C = 5,
        Aes512 = 6,
        Aes512C = 7
    }

    public enum SymmetricAlgorithmTypes
    {
        Cbc = 0,
        Ecb = 1,
        Cfb = 2,
        Gcm = 3,
        ChaCha20Poly1305 = 4
    }

    public enum MacAlgorithmTypes
    {
        Hmac = 0
    }

    public enum SecurityTypes
    {
        Credential = 0,
        Token = 1,
        SnmpCommunityAuth = 2, // For SNMP v1 and v2c
        SnmpUsmAuth = 3, // For SNMP v3
        PreSharedKey = 4,
        SymmetricEncryption = 5,
        AsymmetricEncryption = 6,
        ApiAuth = 7
    }

    public enum MetricUnits
    {
        [Description("MBy")]
        MegaByte,
        [Description("kBy")]
        KiloByte,
        [Description("By")]
        Byte,
        [Description("s")]
        Second,
        [Description("cs")]
        CentiSecond,
        [Description("bit/s")]
        BitPerSecond,
        [Description("%")]
        Percentage,
        [Description("Cel")]
        Celsius,
        [Description("V")]
        Volt,
        [Description("/min")]
        RevolutionsPerMinute,
        [Description("1")]
        Generic
    }

    public enum CheckCategoryTypes
{
    Performance,
    Security,
    Availability,
    Compliance,
}

    public enum CheckStatusTypes
{
    Success,
    Warning,
    Error
}

    public enum CheckQueryTypes
    {
        Single = 1,
        TimeRange = 2,
        Count = 3
    }
}
