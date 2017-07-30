namespace Common.ArgumentMust
{
	using System;
	using System.Linq;
	using System.Reflection;

	internal static class ArgumentFuncExtensions
	{
		public static FieldInfo GetParameter<T>(this Func<T> argumentFunc, Func<T, bool> satisfactionFunc)
		{
			var targetClass = argumentFunc.Target;

			var fields = targetClass.GetType().GetFields();
			var fieldsWithTheSameType = fields.Where(field => field.FieldType == typeof(T));
			var fieldsSatisfyingTheFunc = fieldsWithTheSameType.Where(x => satisfactionFunc((T)x.GetValue(targetClass)));

			return fieldsSatisfyingTheFunc.First(field => ReferenceEquals((T)field.GetValue(targetClass), argumentFunc()));
		}
	}
}