using System;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Data
            var data = new byte[] { 1, 2, 3 };
            // var hash = SHA256.Create().ComputeHash(data); // Calculate hash of data. See also ECDsa.SignHash();

            // PrivateKey
            var privateKey = new byte[32] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // RNGCryptoServiceProvider.Create().GetBytes(privateKey); // Fill random

            // PublicKey calculate
            var ecdsaCurve = ECCurve.NamedCurves.nistP256;
            var ecdsa = ECDsa.Create(new ECParameters
            {
                Curve = ecdsaCurve,
                D = privateKey
            });
            var publicAndPrivateKey = ecdsa.ExportParameters(true);

            // Sign
            var ecdsaSign = ECDsa.Create(new ECParameters
            {
                Curve = ecdsaCurve,
                D = publicAndPrivateKey.D,
                Q = publicAndPrivateKey.Q
            });
            var signature = ecdsaSign.SignData(data, HashAlgorithmName.SHA256); // Needs private and public key

            // Verify
            var ecdsaVerify = ECDsa.Create(new ECParameters
            {
                Curve = ecdsaCurve,
                Q = publicAndPrivateKey.Q
            });
            bool isValid = ecdsaVerify.VerifyData(data, signature, HashAlgorithmName.SHA256); // No private key needed!

            // Return
            Console.WriteLine("PrivateKey=" + Convert.ToHexString(publicAndPrivateKey.D));
            Console.WriteLine("PublicKey=" + Convert.ToHexString(publicAndPrivateKey.Q.X) + Convert.ToHexString(publicAndPrivateKey.Q.Y));
            Console.WriteLine("Data=" + Convert.ToHexString(data));
            Console.WriteLine("IsValid=" + isValid);

            Console.WriteLine("Hello World!");
        }
    }
}
