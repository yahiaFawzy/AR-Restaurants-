using UnityEngine.Networking;

namespace TriLibCore.Samples
{
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}