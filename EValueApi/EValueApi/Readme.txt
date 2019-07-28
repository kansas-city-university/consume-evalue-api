

If you are going to use this in SSIS, make sure you have registered this in the GAC.

Install the following PowerShell module:

https://powershellgac.codeplex.com/

Another place to find it:

https://github.com/LTruijens/powershell-gac

Installation procedures:

https://github.com/LTruijens/powershell-gac/blob/master/INSTALL.md

You will need to compile this asembly and then register it.  You will see that it has an snk file included.  Make sure the assembly is signed.  

NOTE: You will not be able to debug this if it is signed.  Remove the snk file while debugging, and then re-sign it when you are ready to deploy.

