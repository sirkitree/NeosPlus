using BaseX;
using FrooxEngine;
using FrooxEngine.LogiX;
using System;
using System.Security.Cryptography.X509Certificates;

[NodeName("Certificate Info")]
[Category("LogiX/Security")]
public class CertificateInfo : LogixNode
{
    public readonly Input<X509Certificate2> Certificate;
    public readonly Output<string> Subject;
    public readonly Output<string> Issuer;
    public readonly Output<DateTime> NotBefore;
    public readonly Output<DateTime> NotAfter;
    public readonly Output<string> Thumbprint;
    public readonly Impulse OnDone;

    protected override void OnEvaluate()
    {
        X509Certificate2 certificate = Certificate.Evaluate();
        if (certificate == null)
        {
            Subject.Value = null;
            Issuer.Value = null;
            NotBefore.Value = default(DateTime);
            NotAfter.Value = default(DateTime);
            Thumbprint.Value = null;
            return;
        }

        Subject.Value = certificate.Subject;
        Issuer.Value = certificate.Issuer;
        NotBefore.Value = certificate.NotBefore;
        NotAfter.Value = certificate.NotAfter;
        Thumbprint.Value = certificate.Thumbprint;
        OnDone.Trigger();
    }

    [ImpulseTarget]
    public void OnExtractCertificateInfo()
    {
        OnEvaluate();
    }
}
