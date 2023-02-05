using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeekBox.TechnologyTree
{
    public static class UnityExtensions
    {
        public static List<FieldInfo> GetFieldsWithAttribute<T>(this Type target) where T : Attribute
        {
            return target.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(x => x.IsDefined(typeof(T), false)).ToList();
        }

        public static List<Type> GetTypesWithAttribute<T>(this Assembly[] assemblies) where T : Attribute
        {
            List<Type> types = new List<Type>();
            for (int i = 0; i < assemblies.Length; i++)
            {
                types.AddRange(assemblies[i].GetTypesWithAttribute(typeof(T)));
            }

            return types;
        }

        public static List<Type> GetTypesWithAttribute(this Assembly assembly, Type lookup)
        {
            List<Type> output = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true && type.IsAbstract == false && type.IsDefined(lookup, false))
                {
                    output.Add(type);
                }
            }

            return output;
        }
    }
}
