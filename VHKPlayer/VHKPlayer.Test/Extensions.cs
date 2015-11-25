using Autofac;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test
{
    public static class Extensions
    {
        public static TInterface RegisterFake<TInterface>(this ContainerBuilder builder) where TInterface : class
        {
            var fake = Substitute.For<TInterface>();
            builder.RegisterInstance<TInterface>(fake);

            return fake;
        }

        public static void IgnoreAwait(this Task task)
        {

        }

        public static T RandomizeTheRest<T>(this T element) where T : class
        {
            return (T)RandomizeTheRest(element, new Type[0]);
        }

        public static object RandomizeTheRest(this object element, params Type[] excludedTypes)
        {
            var fixture = new Fixture();
            var fixtureType = typeof(SpecimenFactory);
            var fixtureMethods = fixtureType.GetMethods();

            MethodInfo fixtureCreateMethod = null;
            foreach (var method in fixtureMethods)
            {
                if (method.IsPublic && method.IsStatic && method.IsGenericMethod && method.Name == "Create")
                {
                    var genericArguments = method.GetGenericArguments();
                    var arguments = method.GetParameters();
                    if (genericArguments.Length == 1 && arguments.Length == 2)
                    {
                        var firstArgument = arguments[0];
                        var secondArgument = arguments[1];
                        if (firstArgument.ParameterType == typeof(ISpecimenBuilder) && secondArgument.ParameterType.IsGenericParameter)
                        {
                            if (fixtureCreateMethod != null)
                            {
                                throw new InvalidOperationException("Multiple create methods were found. Use better conditions for finding the create method.");
                            }

                            fixtureCreateMethod = method;
                        }
                    }
                }
            }

            if (fixtureCreateMethod == null)
            {
                throw new InvalidOperationException("No create methods were found. Use better conditions for finding the create method.");
            }

            var type = element.GetType();
            foreach (var property in type.GetProperties())
            {
                var getMethod = property.GetMethod;
                var setMethod = property.SetMethod;
                if (setMethod != null && getMethod != null && setMethod.IsPublic)
                {
                    var arguments = setMethod.GetParameters();
                    if (arguments.Length == 1)
                    {
                        var firstArgument = arguments[0];
                        var argumentType = firstArgument.ParameterType;

                        var argumentTypeDefaultValue = argumentType.IsValueType ? Activator.CreateInstance(argumentType) : null;

                        var isSimpleType = argumentTypeDefaultValue != null || argumentType == typeof(string) || (argumentType.IsGenericType && argumentType.GetGenericTypeDefinition() == typeof(Nullable<>));

                        var currentValue = getMethod.Invoke(element, new object[0]);
                        if ((currentValue == argumentTypeDefaultValue || !isSimpleType || currentValue == null || currentValue.Equals(argumentTypeDefaultValue)) && !excludedTypes.Contains(argumentType))
                        {
                            if (isSimpleType)
                            {
                                var genericFixtureCreateMethod = fixtureCreateMethod.MakeGenericMethod(argumentType);
                                setMethod.Invoke(element, new[] { genericFixtureCreateMethod.Invoke(null, new[] { fixture, null }) });
                            }
                            else if (!argumentType.IsInterface)
                            {
                                var newExcludedTypes = new List<Type>(excludedTypes);
                                newExcludedTypes.Add(type);

                                if (currentValue == null)
                                {
                                    currentValue = Activator.CreateInstance(argumentType);
                                }

                                currentValue.RandomizeTheRest(newExcludedTypes.ToArray());
                            }
                        }
                    }
                }
            }

            return element;
        }
    }
}
