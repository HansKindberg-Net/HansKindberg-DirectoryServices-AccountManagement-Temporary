using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Hans Kindberg - open source")]
[assembly: AssemblyConfiguration(
#if DEBUG
	"Debug"
#else
	"Release"
#endif
	)]
[assembly: AssemblyInformationalVersion("1.0.0-alpha-0")]
#pragma warning disable 436

[assembly: AssemblyProduct(AssemblyInfo.AssemblyName)]
[assembly: AssemblyTitle(AssemblyInfo.AssemblyName)]
#pragma warning restore 436

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: ComVisible(false)]