using System;
using System.Reflection;
using System.Reflection.Emit;

namespace MyNamespace.EmitSample
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "HelloWorld";

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.Save );

            object t = assemblyBuilder.EntryPoint;


            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule( 
                "testmod", 
                "TestAsm.exe" );

            TypeBuilder typeBuilder = moduleBuilder.DefineType( "MyNameSpace.SampleCode.myType", TypeAttributes.Public );
//            MethodBuilder methodBuilder = typeBuilder.DefineMethod( 
//                "hi", 
//                MethodAttributes.Public |MethodAttributes.Static,
//                null,
//                null );

//            MethodBuilder methodBuilder = typeBuilder.DefineMethod( 
//                "Main", 
//                MethodAttributes.Public |MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
//                MethodAttributes.Private |MethodAttributes.Static | MethodAttributes.HideBySig,
//                null,
//                null );


            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "Main", 
                MethodAttributes.Public | MethodAttributes.HideBySig |MethodAttributes.Static, 
                typeof(void), 
                null );


//            moduleBuilder.SetUserEntryPoint( methodBuilder );
            ILGenerator generator = methodBuilder.GetILGenerator();
            generator.EmitWriteLine( "Hello world" );
            generator.Emit( OpCodes.Ret );
            typeBuilder.CreateType();

            assemblyBuilder.SetEntryPoint( methodBuilder );
            assemblyBuilder.Save( "TestAsm.exe" );


		}
	}
}
