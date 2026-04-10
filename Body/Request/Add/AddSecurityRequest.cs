using SharedLibrary.Enum;

using static SharedLibrary.Enum.Enums;

namespace GlassBoard.Request.Add
{
public class AddSecurityRequest
    {
        public string? Name { get; set; }
        public SecurityTypes SecurityType { get; set; }
        public SecretScopeTypes SecretScope { get; set; }
        public SecretData Secret { get; set; } = new();
    }

    public class SecretData
    {
        public string? Key { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecretKey { get; set; }
        public string? PrivateKey { get; set; }
        public string? PublicKey { get; set; }
        public AsymmetricAlgorithmTypes? Algorithm { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? SecondaryPassword { get; set; }
        public string? PreSharedKey { get; set; }
        public string? Community { get; set; }
        public string? AuthPassword { get; set; }
        public SnmpV3AuthProtocolTypes? AuthProtocol { get; set; }
        public SecurityLevelTypes? SecurityLevel { get; set; }
        public string? ContextName { get; set; }
        public string? PrivacyPassword { get; set; }
        public SnmpV3PrivacyProtocolTypes? PrivacyProtocol { get; set; }
        public string? EncryptionKey { get; set; }
        public SymmetricAlgorithmTypes? EncryptionAlgorithm { get; set; }
        public string? MacKey { get; set; }
        public MacAlgorithmTypes? MacAlgorithm { get; set; }
        public string? Token { get; set; }
    }
}
