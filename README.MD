BibliTech's Roslyn ViewModels is a small `dotnet` tool to generate raw view model files from Entity Framework entity files. View Model files in this scope is a file without any link/reference properties. For example:

![Entity vs View Models](https://i.imgur.com/BraFyya.png)

This tool will scan the entire folder and generate a single file with your basic View Model classes.

# Installation

Roslyn ViewModels uses .NET Core tools to execute. You will need [.NET Core >= 2.1](https://www.microsoft.com/net/download) installed.

You can either install Roslyn ViewModels per-project or globally on the PC.

## Install globally
To install once and run anywhere, run this command once:

`dotnet tool install dotnet-viewmodel -g`

NOTE: the current version does not work for global tool yet. I will try to fix it as soon as possible. Please use the per-project for now.

## Install per-project

To use the tool for the current project only, open your `.csproj` file with any text-editor. You need to add the following lines:

```
<ItemGroup>
    <DotNetCliToolReference Include="dotnet-viewmodel" Version="1.0.4" />
</ItemGroup>
```

Note: please always check for newest version of the tool. Sometimes I may forget to update the version number in this README file.

To make sure it works, please try running `dotnet restore` or `dotnet build` afterwards (you don't need to run any nuget install).

# Use the tool

Using your command line (note: please do not use Nuget Package Manager console in Visual Studio), run the following command:

> dotnet viewmodel -?

You should see the help text for using the tool. You can see [the full demo command in this file](https://github.com/BibliTech/Roslyn.ViewModels/blob/master/BibliTech.Roslyn.ViewModels.Demo/generate-command.ps1).

For example:

> dotnet viewmodel Entities ViewModels\EntityViewModels.cs

will tell the tool to scan the `Entities` folder and output the result into `ViewModels\EntityViewModel.cs` file.

# The parameters

All the parameters are optional. Example:

> dotnet viewmodel Entities ViewModels\EntityViewModels.cs -c "{0}ViewModel" -ns "BibliTech.Demo.ViewModels" -attr "ViewModel, EntityViewModel" -bases "BaseViewModel, IViewModel" -u "BibliTech" -u "BibliTech.Roslyn.ViewModels.Demo" -f

* `-f`: Force overwriting the result. Without -f, if the output file exists, the tool will not do anything and print out an error.
* `-c`: the class name of the output ViewModel, the default is `{0}BasicViewModel`. For example, if your entity class name is `Account`, the output file in the example will be `AccountViewModel`.
* `-ns` the namespace of the output file.
* `-attr` If you want to append any attribute(s) to the output classes, use this.
* `-bases` If you want to add any inheritance declaration, use this option.
* `-u` Add `using` directives at the beginning of the files. The `System` value is there by default so don't add it.