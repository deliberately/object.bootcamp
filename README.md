# Object Bootcamp for .NET

From a powershell window run: `.\build.ps1`

If you get an error saying: 

```
.\build.ps1 : File .\build.ps1 cannot be loaded because running scripts is disabled on this system. For more information, see 
about_Execution_Policies at http://go.microsoft.com/fwlink/?LinkID=135170.
At line:1 char:1
+ .\build.ps1
+ ~~~~~~~~~~~
    + CategoryInfo          : SecurityError: (:) [], PSSecurityException
    + FullyQualifiedErrorId : UnauthorizedAccess
```

Execute `Set-ExecutionPolicy Unrestricted` at the command prompt and re-run the command.
