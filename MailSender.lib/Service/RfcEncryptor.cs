﻿using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MailSender.lib.Service
{
    public class RfcEncryptor : IEncryptorService
    {
        private static readonly byte[] Salt =
        {
            0x26, 0xdc, 0xff, 0x00,
            0xad, 0xed, 0x7a, 0xee,
            0xc5, 0xfe, 0x07, 0xaf,
            0x4d, 0x08, 0x22, 0x3c
        };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        //Алгоритм шифрования
        private static ICryptoTransform GetAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, Salt);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateEncryptor();
        }

        //Алгоритм расшифровки
        private static ICryptoTransform GetInverseAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, Salt);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateDecryptor();
        }

        public string Decrypt(string str, string Password)
        {
            var crypted_bytes = Convert.FromBase64String(str);
            var bytes = Decrypt(crypted_bytes, Password);
            var encoding = Encoding ?? Encoding.UTF8;
            return encoding.GetString(bytes);
        }

        public byte[] Decrypt(byte[] data, string Password)
        {
            var algorithm = GetInverseAlgorithm(Password);
            using var stream = new MemoryStream();
            using var crypto_stream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write);
            crypto_stream.Write(data, 0, data.Length);
            crypto_stream.FlushFinalBlock();
            return stream.ToArray();
        }

        public string Encrypt(string str, string Password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            var crypted_bytes = Encrypt(bytes, Password);
            return Convert.ToBase64String(crypted_bytes);
        }

        public byte[] Encrypt(byte[] data, string Password)
        {
            var algorithm = GetAlgorithm(Password);
            using var stream = new MemoryStream();
            using var crypto_stream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write);
            crypto_stream.Write(data, 0, data.Length);
            crypto_stream.FlushFinalBlock();
            return stream.ToArray();
        }
    }
}
