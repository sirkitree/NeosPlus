using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using BaseX;
using FrooxEngine;

namespace NEOSPlus.Components.Security
{
    [Category("Security")]
    public class X509CertificateLoader : Component
    {
        public readonly Sync<string> URI;
        public readonly Sync<X509Certificate2> Certificate;

        protected override void OnChanges()
        {
            base.OnChanges();

            string uri = URI.Value;
            if (string.IsNullOrEmpty(uri))
            {
                Certificate.Value = null;
                return;
            }

            try
            {
                // Check if the URI is a file path or a network URL
                if (uri.StartsWith("file://"))
                {
                    uri = new Uri(uri).LocalPath;
                    Certificate.Value = new X509Certificate2(uri);
                }
                else if (uri.StartsWith("http://") || uri.StartsWith("https://"))
                {
                    // Download the certificate from the network
                    ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                    HttpWebRequest request = WebRequest.CreateHttp(uri);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    System.IO.Stream responseStream = response.GetResponseStream();
                    byte[] x509Data = ReadX509FromStream(responseStream);
                    Certificate.Value = new X509Certificate2(x509Data);
                    responseStream.Close();
                }
                else
                {
                    UniLog.Log($"Invalid URI: {uri}");
                    Certificate.Value = null;
                }
            }
            catch (Exception ex)
            {
                UniLog.Error($"Failed to load certificate from URI {uri}: {ex.Message}");
                Certificate.Value = null;
            }
        }

        public static byte[] ReadX509FromStream(System.IO.Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                byte[] x509Data = reader.ReadBytes((int)stream.Length);
                return x509Data;
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // We're accepting all server certificates, regardless of their chain or policy errors
            return true;
        }
    }
}

