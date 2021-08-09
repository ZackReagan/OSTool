﻿using Microsoft.Win32;

namespace OSTool.Core
{
    public class CustomKindToRegistryKind
    {
        public static RegistryValueKind CustomToRegsitry(CustomRegistryValueKind kind)
        {
            return kind == CustomRegistryValueKind.DWord ? RegistryValueKind.DWord : kind == CustomRegistryValueKind.MultiString ? RegistryValueKind.MultiString : RegistryValueKind.String;
        }
    }
}