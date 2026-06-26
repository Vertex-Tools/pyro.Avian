// ====================================
// <copyright file="PyroGenerator.cs" company="Spicco D'Aura">
// Copyright (c) Spicco D'Aura. All rights reserved.
// Licensed under the CC BY-SA 1.0 License.
// </copyright>
// ====================================

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using pyro.Avian.Internal;
using pyro.Avian.Templates;

namespace pyro.Avian;

[Generator(LanguageNames.CSharp)]
public class PyroGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilationAndOptions = context.CompilationProvider.Combine(context.AnalyzerConfigOptionsProvider);

        context.RegisterSourceOutput(compilationAndOptions, (productionContext, source) =>
        {
            var compilation = source.Left;
            var optionsProvider = source.Right;
            
            string targetAssembliesString = string.Empty;
            if (optionsProvider.GlobalOptions.TryGetValue("build_property.PyroEmbedAssemblies", out var propValue) && !string.IsNullOrWhiteSpace(propValue))
            {
                targetAssembliesString = propValue;
            }
            
            if (string.IsNullOrEmpty(targetAssembliesString))
            {
                targetAssembliesString = "Avian";
            }

            var assembliesToEmbed = targetAssembliesString
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            var embeddedAssembliesMap = new Dictionary<string, string>();

            foreach (var assemblyName in assembliesToEmbed)
            {
                var reference = compilation.References
                    .FirstOrDefault(r => r.Display != null && Path.GetFileNameWithoutExtension(r.Display).Equals(assemblyName, StringComparison.OrdinalIgnoreCase));

                if (reference != null)
                {
                    string byteArrayString = AssemblyEmbedder.ConvertToReferenceString(reference);
                    embeddedAssembliesMap[assemblyName] = byteArrayString;
                }
            }
            
            string bootstrapCode = BootstrapTemplate.Render(embeddedAssembliesMap);

            productionContext.AddSource(
                "PyroBootstrap.g.cs",
                SourceText.From(bootstrapCode, Encoding.UTF8)
            );
        });
    }
}