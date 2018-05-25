using System;
using System.Collections.Generic;
using System.Reflection;

namespace IocExample
{
    public class DependencyResolver
    {
        private readonly Dictionary<Type, Type> types;
        private readonly Dictionary<Type, object> objects;

        public DependencyResolver()
        {
            types = new Dictionary<Type, Type>();
            objects = new Dictionary<Type, object>();
        }
        
        public void Bind<TClass, TImplementation>()
        {
            types[typeof(TClass)] = typeof(TImplementation);
        }
        public void Bind<TClass, TImplementation>(TImplementation instance)
        {
            objects[typeof(TClass)] = instance;
        }
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }
        private object Get(Type type)
        {
            if (objects.ContainsKey(type))
                return objects[type];
            Type implementation = types[type];
            ConstructorInfo constructor = Utils.GetSingleConstructor(implementation);
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
                return Utils.CreateInstance(implementation);
            List<object> parameters = new List<object>(constructorParameters.Length);
            foreach (ParameterInfo parameterInfo in constructorParameters)
                parameters.Add(Get(parameterInfo.ParameterType));
            return Utils.CreateInstance(implementation, parameters);
        }
    }
}