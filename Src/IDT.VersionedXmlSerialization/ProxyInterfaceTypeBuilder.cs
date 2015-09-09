using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace IDT.VersionedXmlSerialization
{
    /// <summary>
    /// Object that creates a concrete types that implement the properties of a provided interface.
    /// </summary>
    public class ProxyInterfaceTypeBuilder
    {
        private const string DynamicProxyNamespace = "IDT.VersionedXmlSerialization.DynamicProxy";
        private const MethodAttributes GetterSetterMethodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.Virtual;
        private const TypeAttributes ProxyClassTypeAttributes = TypeAttributes.Serializable | TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed;
        
        /// <summary>
        /// Creates a proxy class for an interface of the specified type.
        /// </summary>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <returns>The new <see cref="Type"/> object for the proxy class.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="interfaceType"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="interfaceType"/> is not an interface.</exception>
        public Type GetProxyType(Type interfaceType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException("interfaceType");
            }

            if (_createdTypeLookup.ContainsKey(interfaceType.FullName))
            {
                return _createdTypeLookup[interfaceType.FullName];
            }

            Type newType = CreateProxyType(interfaceType);
            _createdTypeLookup.Add(interfaceType.FullName, newType);

            return newType;
        }

        private static Type CreateProxyType(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException("Type is not an interface: " + interfaceType.Name, "interfaceType");
            }

            string proxyTypeName = DynamicProxyNamespace + "." + interfaceType.Name;

            ModuleBuilder moduleBuilder = GetModuleBuilder();
            TypeBuilder typeBuilder = moduleBuilder.DefineType(proxyTypeName, ProxyClassTypeAttributes, typeof(object), new[] { interfaceType });
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            foreach (var propertyInfo in GetAllProperties(interfaceType))
            {
                FieldBuilder fieldBuilder = typeBuilder.DefineField("field_" + propertyInfo.Name, propertyInfo.PropertyType, FieldAttributes.Private);

                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyInfo.Name, propertyInfo.Attributes, propertyInfo.PropertyType, null);

                MethodBuilder getMethod = DefineGetMethod(typeBuilder, fieldBuilder, propertyInfo);
                propertyBuilder.SetGetMethod(getMethod);

                MethodBuilder setMethod = DefineSetMethod(typeBuilder, fieldBuilder, propertyInfo);
                propertyBuilder.SetSetMethod(setMethod);
            }

            return typeBuilder.CreateType();
        }
        
        private static MethodBuilder DefineGetMethod(TypeBuilder typeBuilder, FieldInfo fieldInfo, PropertyInfo propertyInfo)
        {
            // Define the 'get_' method.
            MethodBuilder getMethod = typeBuilder.DefineMethod(
                "get_" + propertyInfo.Name,
                GetterSetterMethodAttributes,
                propertyInfo.PropertyType,
                null);

            // Generate IL code for 'get_' method.
            ILGenerator methodIl = getMethod.GetILGenerator();
            methodIl.Emit(OpCodes.Ldarg_0);
            methodIl.Emit(OpCodes.Ldfld, fieldInfo);
            methodIl.Emit(OpCodes.Ret);

            return getMethod;
        }

        private static MethodBuilder DefineSetMethod(TypeBuilder typeBuilder, FieldInfo fieldInfo, PropertyInfo propertyInfo)
        {
            Type[] methodArgs = {propertyInfo.PropertyType};

            // Define the 'set_' method.
            MethodBuilder setMethod = typeBuilder.DefineMethod(
                "set_" + propertyInfo.Name,
                GetterSetterMethodAttributes,
                typeof (void),
                methodArgs);

            // Generate IL code for 'set_' method.
            ILGenerator methodIl = setMethod.GetILGenerator();
            methodIl.Emit(OpCodes.Ldarg_0);
            methodIl.Emit(OpCodes.Ldarg_1);
            methodIl.Emit(OpCodes.Stfld, fieldInfo);
            methodIl.Emit(OpCodes.Ret);

            return setMethod;
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            var publicProperties = new List<PropertyInfo>();

            publicProperties.AddRange(type.GetProperties());

            foreach (var implementedInterfaceType in type.GetInterfaces())
            {
                publicProperties.AddRange(GetAllProperties(implementedInterfaceType));
            }

            return publicProperties;
        }

        private static ModuleBuilder GetModuleBuilder()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            var assemblyName = new AssemblyName(DynamicProxyNamespace);
            AssemblyBuilder assemblyBuilder = currentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            return assemblyBuilder.DefineDynamicModule(DynamicProxyNamespace);
        }

        private readonly Dictionary<string, Type> _createdTypeLookup = new Dictionary<string, Type>();
    }
}
