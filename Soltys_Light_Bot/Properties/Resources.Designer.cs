﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Soltys.ProgrammerBot.Properties
{


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Soltys.ProgrammerBot.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Models/dragon.obj.
        /// </summary>
        internal static string DragonModelPath
        {
            get
            {
                return ResourceManager.GetString("DragonModelPath", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Icon/robot.ico.
        /// </summary>
        internal static string IconPath
        {
            get
            {
                return ResourceManager.GetString("IconPath", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Textures/JuliaColorTable_16_1.bmp.
        /// </summary>
        internal static string JuliaSetColorTable
        {
            get
            {
                return ResourceManager.GetString("JuliaSetColorTable", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Shaders/JuliaSet_SM3_FS_16_1.glsl.
        /// </summary>
        internal static string JuliaSetFragmentShader
        {
            get
            {
                return ResourceManager.GetString("JuliaSetFragmentShader", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Shaders/JuliaSet_SM2_FS_16_1.glsl.
        /// </summary>
        internal static string JuliaSetFragmentShaderBackup
        {
            get
            {
                return ResourceManager.GetString("JuliaSetFragmentShaderBackup", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Data/Shaders/JuliaSet_VS.glsl.
        /// </summary>
        internal static string JuliaSetVertexShader
        {
            get
            {
                return ResourceManager.GetString("JuliaSetVertexShader", resourceCulture);
            }
        }

        internal static System.Drawing.Icon Robot
        {
            get
            {
                object obj = ResourceManager.GetObject("robot", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Programmer Bot - Paweł &apos;sołtys&apos; Sołtysiak .
        /// </summary>
        internal static string WindowTitle
        {
            get
            {
                return ResourceManager.GetString("WindowTitle", resourceCulture);
            }
        }
    }
}
