using MailSender.lib.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class RfcEncryptorTests
    {
        private IEncryptorService _Encryptor = new lib.Service.RfcEncryptor();

        static RfcEncryptorTests() { }

        public RfcEncryptorTests() { }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext Context)
        {

        }

        [ClassInitialize]

        public static void ClassInitialize(TestContext Context)
        {

        }

        [TestInitialize]

        public void TestInitialize()
        {

        }

        [TestCleanup]
        public  void TestCleanup()
        {

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {

        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {

        }

        [TestMethod]
        public void Encrypt_Hello_World_and_Decrypt_with_Password()
        {
            const string str = "Hello World!";
            const string pass = "Password";
            var encrypted_str = _Encryptor.Encrypt(str,pass);
            var decrypted_str = _Encryptor.Decrypt(encrypted_str, pass);

            Assert.AreNotEqual(str, encrypted_str);
            Assert.AreEqual(str, decrypted_str);


        }

        [TestMethod, ExpectedException(typeof(CryptographicException))]
        public void Wrong_Decryption_Thrown_CryproException()
        {
            const string str = "Hello World!";
            const string pass = "Password";
            var encrypted_str = _Encryptor.Encrypt(str, pass);
            var wrong_password_decrypt = _Encryptor.Decrypt(encrypted_str, "asdasd");
            Assert.AreNotEqual(str, wrong_password_decrypt);
        }

    }
}
