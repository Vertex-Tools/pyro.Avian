// ====================================
// <copyright file="AssemblyEmbedder.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

using System;
using System.IO;
using Microsoft.CodeAnalysis;

namespace pyro.Avian.Internal;

internal static class AssemblyEmbedder
{
    public static string ConvertToReferenceString(MetadataReference reference)
    {
        if (reference.Display == null || !File.Exists(reference.Display))
        {
            return "\"\"";
        }

        try
        {
            byte[] fileBytes = File.ReadAllBytes(reference.Display);
            string base64String = Convert.ToBase64String(fileBytes);
            
            return $"\"{base64String}\"";
        }
        catch
        {
            return "\"\"";
        }
    }
}