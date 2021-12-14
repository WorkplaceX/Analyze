using Base58Check;
using NBitcoin;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bitcoin
            var privateKeyBitcoin = "L4Y1cGSsNv1Nf9dZpTkEyQjLU24zRyRQeRyE5i4MoVvrjrr15Koy"; // Example https://komodoplatform.com/en/academy/bitcoin-private-key/
            var privateKeyBillText = new BitcoinSecret(privateKeyBitcoin, Network.Main);
            var uncompressed = privateKeyBillText.Copy(false);
            var privateKeyBill = uncompressed.ToBytes();
            var publicKeyBillText = privateKeyBillText.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
            var publicKeyBillTextUncompressed = uncompressed.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString(); // TODO Remove
            var publicKeyBillHex = Convert.ToHexString(uncompressed.PubKey.ToBytes().Skip(1).ToArray());

            // Data
            var data = new byte[] { 1, 2, 3 };
            // var hash = SHA256.Create().ComputeHash(data); // Calculate hash of data. See also ECDsa.SignHash();

            // PrivateKey
            var privateKey = privateKeyBill; // new byte[32] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // RNGCryptoServiceProvider.Create().GetBytes(privateKey); // Fill random

            // PublicKey calculate
            var ecdsaCurve = ECCurve.CreateFromFriendlyName("secp256k1");
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

            // Output
            var publicKey = Enumerable.Concat(publicAndPrivateKey.Q.X, publicAndPrivateKey.Q.Y).ToArray();
            Console.WriteLine("PrivateKey=" + Convert.ToHexString(publicAndPrivateKey.D));
            Console.WriteLine("PublicKey=" + Convert.ToHexString(publicKey));
            Console.WriteLine("Data=" + Convert.ToHexString(data));
            Console.WriteLine("IsValid=" + isValid);

            Debug.Assert(publicKeyBillHex == Convert.ToHexString(publicKey));

            // P2PKH Address
            var x = new byte[65];
            x[0] = 0x04;
            publicAndPrivateKey.Q.X.CopyTo(x, 1);
            publicAndPrivateKey.Q.Y.CopyTo(x, 33);

            var sha256 = System.Security.Cryptography.SHA256.Create().ComputeHash(x);
            var ripemd160 = RIPEMD160Managed.Create().ComputeHash(sha256);
            Debug.Assert(ripemd160.Length == 20);
            var publicKeyText = Base58CheckEncoding.Encode(Enumerable.Concat(new byte[] { 0 }, ripemd160).ToArray());
            // Debug.Assert(publicKeyBillText == publicKeyText); // TODO
            Debug.Assert(publicKeyBillTextUncompressed == publicKeyText); // TODO Remove. Use compressed

            Console.WriteLine("Hello World!");
        }
    }
}
