# vr3

VR3 is a mixed C#/Unity and ASP.NET project containing shaders (ShaderLab/HLSL) and Mathematica assets. This repository contains the code and assets used to build and run the VR experience and any supporting backend/web components.

Languages (approx.):
- C#: 59.5%
- ASP.NET: 27.4%
- ShaderLab: 9.3%
- Mathematica: 2.1%
- HLSL: 1.7%

## Overview

This repository appears to combine Unity runtime code (C#, ShaderLab/HLSL) with an ASP.NET backend or tooling. The README below gives general guidance to get started, how to build the project, and where to look for more details.

## Prerequisites

- Unity (recommended: check ProjectSettings/ProjectVersion.txt for the exact project Unity version)
- .NET SDK (if there is an ASP.NET/web component) — dotnet 3.1, 5.0 or newer depending on the project. Check any *.sln or *.csproj files for target frameworks.
- A compatible IDE: Visual Studio, Rider, or Visual Studio Code with C# support

## Getting started

1. Clone the repo:

   git clone https://github.com/DonMdas/vr3.git

2. Open the Unity project
   - Launch Unity Hub and open the cloned folder, or open the project from Unity Editor.
   - Allow Unity to resolve packages and compile scripts.

3. If there is an ASP.NET component
   - Locate the web/server folder (look for a folder containing a .sln or *.csproj)
   - From that folder run:

     dotnet restore
     dotnet run

   - Visit the URL printed by dotnet run (usually http://localhost:5000 or http://localhost:5001).

## Building

- For the Unity application: use Unity Build Settings to select target platform (Windows, Android, Oculus, etc.) and click Build.
- For the ASP.NET/web component: use dotnet build in the folder containing the project file before deploying.

## Project structure (suggested)

- Assets/           -> Unity assets and C# scripts
- ProjectSettings/  -> Unity project configuration (including ProjectVersion.txt)
- Server/ or Web/   -> ASP.NET backend (if present)
- Shaders/          -> ShaderLab/HLSL shader files
- Docs/             -> Documentation and notes

Note: The actual structure may vary. Search for *.sln, *.csproj, and ProjectSettings to identify important subprojects.

## Contributing

Contributions are welcome. Please open issues to discuss larger changes or submit pull requests with a clear description of your changes and how to test them.

## Troubleshooting

- If Unity scripts fail to compile, open the Console window in Unity to see compile errors and fix missing package references or assembly definitions (.asmdef files).
- If the ASP.NET project fails to run, check the dotnet SDK version and review the project files for the target framework.

## License

Add a LICENSE file to this repository to clarify terms. If you are unsure, consider using a standard open-source license such as MIT.

## Contact

For questions, reach out to the repository owner: https://github.com/DonMdas
