// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

internal static partial class Interop
{
    internal static partial class Crypt32
    {
        [DllImport(Libraries.Crypt32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool CryptAcquireCertificatePrivateKey(
            SafeCertContextHandle pCert,
            CryptAcquireCertificatePrivateKeyFlags dwFlags,
            IntPtr pvParameters,
            out IntPtr phCryptProvOrNCryptKey,
            out CryptKeySpec pdwKeySpec,
            out bool pfCallerFreeProvOrNCryptKey);
    }
}
