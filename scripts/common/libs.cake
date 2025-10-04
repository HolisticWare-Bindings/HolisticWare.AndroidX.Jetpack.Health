#load "./nuget-restore.cake"

LibrarySourceSolutions  = GetFiles(source_solutions);
LibrarySourceProjects   = GetFiles(source_projects);

//----------------------------------------------------------------------------------------------------------------------
Task("libs")
    .IsDependentOn ("nuget-restore-libs")
    .IsDependentOn ("libs-dotnet-solutions")
    .IsDependentOn ("libs-dotnet-solutions-binderator")
    .IsDependentOn ("libs-dotnet-projects")
    .IsDependentOn ("libs-msbuild-solutions")
    .IsDependentOn ("libs-msbuild-projects")
    ;

//----------------------------------------------------------------------------------------------------------------------
Task("libs-dotnet-solutions")
    .Does
    (
        () =>
        {
            FilePathCollection files = LibrarySourceSolutions;
            foreach(FilePath file in files)
            {
                AnsiConsole.MarkupLine($"{line_hash}");
                AnsiConsole.MarkupLine($"[bold aqua] Library solution[/]");
                AnsiConsole.MarkupLine($"       [aqua]{file}[/]");

                foreach (string configuration in configurations)
                {
                    if (! BuildSystem.IsLocalBuild && configuration.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    dotnet_settings.Configuration = configuration;

                    try
                    {
                        DotNetClean($"{file}");
                        DotNetBuild($"{file}", dotnet_settings);
                    }    
                    catch(Exception exc)
                    {
                        AnsiConsole.MarkupLine($"[bold red] Error[/]");
                        AnsiConsole.MarkupLine($"       [red] file      : {file}[/]");
                        AnsiConsole.MarkupLine($"       [red] exception : {exc.Message}[/]");
                    }
                }
            }

            return;
        }
    );
//----------------------------------------------------------------------------------------------------------------------
Task("libs-dotnet-projects")
    .Does
    (
        () =>
        {
            FilePathCollection files = LibrarySourceProjects;
            foreach(FilePath file in files)
            {
                AnsiConsole.MarkupLine($"{line_hash}");
                AnsiConsole.MarkupLine($"[bold aqua] Library solution[/]");
                AnsiConsole.MarkupLine($"       [aqua]{file}[/]");

                foreach (string configuration in configurations)
                {
                    if (! BuildSystem.IsLocalBuild && configuration.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    dotnet_settings.Configuration = configuration;

                    try
                    {
                        DotNetClean($"{file}");
                        DotNetBuild($"{file}", dotnet_settings);
                    }    
                    catch(Exception exc)
                    {
                        AnsiConsole.MarkupLine($"[bold red] Error[/]");
                        AnsiConsole.MarkupLine($"       [red] file      : {file}[/]");
                        AnsiConsole.MarkupLine($"       [red] exception : {exc.Message}[/]");
                    }
                }
            }

            return;
        }
    );
//----------------------------------------------------------------------------------------------------------------------

Task("libs-msbuild-solutions")
    .Does
    (
        () =>
        {
            FilePathCollection files = LibrarySourceSolutions;
            foreach(FilePath file in files)
            {
                AnsiConsole.MarkupLine($"{line_hash}");
                AnsiConsole.MarkupLine($"[bold aqua] Library solution[/]");
                AnsiConsole.MarkupLine($"       [aqua]{file}[/]");

                foreach (string configuration in configurations)
                {
                    if (! BuildSystem.IsLocalBuild && configuration.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    dotnet_settings.Configuration = configuration;

                    try
                    {
                        DotNetClean($"{file}");
                        DotNetBuild($"{file}", dotnet_settings);
                    }    
                    catch(Exception exc)
                    {
                        AnsiConsole.MarkupLine($"[bold red] Error[/]");
                        AnsiConsole.MarkupLine($"       [red] file      : {file}[/]");
                        AnsiConsole.MarkupLine($"       [red] exception : {exc.Message}[/]");
                    }
                }
            }

            return;
        }
    );


Task("libs-msbuild-projects")
    .Does
    (
        () =>
        {
            FilePathCollection files = LibrarySourceProjects;
            foreach(FilePath file in files)
            {
                AnsiConsole.MarkupLine($"{line_hash}");
                AnsiConsole.MarkupLine($"[bold aqua] Library project[/]");
                AnsiConsole.MarkupLine($"       [aqua]{file}[/]");

                foreach (string configuration in configurations)
                {
                    if (! BuildSystem.IsLocalBuild && configuration.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    dotnet_settings.Configuration = configuration;

                    try
                    {
                        DotNetClean($"{file}");
                        DotNetBuild($"{file}", dotnet_settings);
                    }    
                    catch(Exception exc)
                    {
                        AnsiConsole.MarkupLine($"[bold red] Error[/]");
                        AnsiConsole.MarkupLine($"       [red] file      : {file}[/]");
                        AnsiConsole.MarkupLine($"       [red] exception : {exc.Message}[/]");
                    }
                }
            }

            return;
        }
    );
//---------------------------------------------------------------------------------------
Task("libs-dotnet-solutions-binderator")
    .Does
    (
        () =>
        {
            if ( ! FileExists($"{path_project}/config.json") )
            {
                return;
            }

            RunTarget("holisticware-android-binderator");
            System.Threading.Thread.Sleep(3000);

            foreach (string c in configurations)
            {
                DotNetBuild
                        (
                            $"{path_project}/generated/HolisticWare.sln",
                            new DotNetBuildSettings
                            {
                                Configuration = c
                            }
                        );
            }
            return;
        }
    );

