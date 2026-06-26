# <img width="64" height="64" alt="Pyro Avian" src="https://github.com/user-attachments/assets/acf8d942-0c6c-4360-b33c-8fd29ae52cef" /> pyro.Avian

[![NuGet Version](https://img.shields.io/nuget/v/pyro.Avian.svg)](https://www.nuget.org/packages/pyro.Avian/)
[![License](https://img.shields.io/badge/license-CC%20BY--SA%201.0-orange.svg)](https://creativecommons.org/licenses/by-sa/1.0/)
[![Platform](https://img.shields.io/badge/.NET-%3E%3D%20Standard%202.0-blue.svg)](https://dotnet.microsoft.com/)

**pyro.Avian** is a modern, high-performance Roslyn Source Generator designed to automatically embed and fuse external assembly dependencies directly into your final binary. 

By leveraging C# 9+ `[ModuleInitializer]` attributes and an optimized compile-time Base64 delivery pipeline, it hooks into the application lifecycle to load dependencies directly from memory at runtime. This completely eliminates the need to distribute separate physical third-party `.dll` files alongside your main application or plugin.

---

## Why pyro.Avian?

Unlike older IL-weaving tools (like Costura.Fody) that disassemble and modify compiled binaries after the build, **pyro.Avian** operates entirely in-memory during the compilation phase using official Roslyn APIs.

* ⚡ **Zero Post-Processing**: No binary hacking. Code is generated natively in real-time, matching your IDE workflow perfectly.
* 🛡️ **IDE & Debugger Friendly**: Since it generates pure C# source text under the hood, breakpoints, debugging, and source maps work out of the box without corruption.
* 📦 **Dependency Isolation**: Embedded assemblies are resolved via memory streams, protecting your plugins from version conflicts with external files on disk.

---

## Installation

Install the package via the .NET CLI:

```bash
dotnet add package pyro.Avian
