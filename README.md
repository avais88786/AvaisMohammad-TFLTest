# Transport For London!

**Project Description:**
	This is a .NET Core 2.2 Console app which functionally is a rest api client for tfl services. This project comes with a set of unit tests as well as specflow feature tests (integration testing). Enjoy!   

# Prerequisites

 - Visual Studio 2017+
 - .NET Core 2.2+
 - TFL - An AppId and an AppKey
 - SpecFlow dependencies
	 - This should be restored and installed while building the solution.  

# Building, Publishing and Usage

 - Build the solution:
	   - git clone
	   - cd into cloned folder
	   - execute command (cmd/ps/bash) - "dotnet build TFLTest_AvaisMohammad.sln"
	  
 - Using the application:
 > The application accepts a command line argument which is taken as roadname to search against or if no argument is specified the application will ask you to provide one.
 > eg. dotnet exec TFLTest_AvaisMohammad.dll A2 **OR** TFLTest_AvaisMohammad.exe A2
 - Run the application:
    **Edit TFLTest_AvaisMohammad\appsettings.development.json with your appId and appKey.**
	    **Run as .csproj:**
	   - cd into TFLTest_AvaisMohammad
	   - execute command (cmd/ps/bash) - "dotnet run --project TFLTest_AvaisMohammad.csproj"
	   **Run as .dll:**
	   - cd into TFLTest_AvaisMohammad\bin\\{ReleaseType}\netcoreapp2.2\
	   - execute command (cmd/ps/bash) - "dotnet exec TFLTest_AvaisMohammad.dll"
	   **Run as executable (.exe - windows):**
	   - cd into TFLTest_AvaisMohammad
	   - execute command (cmd/ps/bash) - "dotnet publish -c debug -r win-x64" 
	   - cd into bin\debug\netcoreapp2.2\
	   - execute command (cmd/ps) - "TFLTest_AvaisMohammad.exe < roadname > " 	   

## Running the Tests

There are unit tests (TDD) and feature files (BDD) which are separate projects.
>make sure you have build the solution and it was built successfully.

### Unit Tests:

 - cd into TFLTest_AvaisMohammad.Tests
 - execute command - "dotnet test TFLTest_AvaisMohammad.Tests.csproj"

### Integration Tests (Specflow):
**Edit TFLTest_AvaisMohammad.Specs\appsettings.development.json with your appId and appKey.**
 - cd into TFLTest_AvaisMohammad.Specs
 - execute command - "dotnet test TFLTest_AvaisMohammad.Specs.csproj"

 ## CI/CD
 appsettings.< environment >.json can be replaced per environment given there is an environment variable with the value under key "Environment"
 > eg. on systemtest, one can have environment variable 'Environment' set as 'systemtest', then you need to deploy a 'appsettings.systemtest.json' with appropriate environment-specific credentials .