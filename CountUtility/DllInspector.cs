#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace CountUtility
{
    /// <summary>
    ///     Class containing features to inspect a DLL.
    /// </summary>
    public class DllInspector
    {
        #region Properties

        /// <summary>
        ///     The path to the dll.
        /// </summary>
        private readonly String _dllPath;

        /// <summary>
        ///     The assembly to inspect.
        /// </summary>
        private Assembly _assembly;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the assembly.
        /// </summary>
        /// <value>The assembly.</value>
        public Assembly Assembly
        {
            get { return _assembly ?? (_assembly = Assembly.LoadFrom(_dllPath)); }
        }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="DllInspector" /> class.
        /// </summary>
        /// <param name="dllPath">The path to a dll.</param>
        public DllInspector(String dllPath)
        {
            _dllPath = dllPath;
        }

        #endregion

        #region Public Members

        /// <summary>
        ///     Gets the public accessable types of the dll.
        /// </summary>
        /// <returns>Returns the public accessable types of the dll.</returns>
        public IEnumerable<Type> GetExportedTypes()
        {
            return Assembly.GetExportedTypes();
        }

        /// <summary>
        ///     Gets all public static methods of the dll.
        /// </summary>
        /// <returns>Returns all public static methods of the dll.</returns>
        public IEnumerable<MethodInfo> GetAllStaticMethods()
        {
            return GetExportedTypes().SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Static));
        }

        /// <summary>
        ///     Gets a unique name (Class.Method) for each method given.
        /// </summary>
        /// <param name="methodInfos">The methods.</param>
        /// <returns>A collection of unique names.</returns>
        public IEnumerable<String> GetUniqueName(IEnumerable<MethodInfo> methodInfos)
        {
            return
                methodInfos.Select(x => (x.DeclaringType != null ? x.DeclaringType.Name : String.Empty) + "." + x.Name);
        }

        #endregion
    }
}