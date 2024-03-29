﻿using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace WPF.Logging
{
	public class CallerEnricher : ILogEventEnricher
	{
		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			var skip = 3;
			while (true)
			{
				var stack = new StackFrame(skip);
				if (!stack.HasMethod())
				{
					logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method> ")));
					return;
				}

				var method = stack.GetMethod();
				if (method.DeclaringType.Assembly != typeof(Log).Assembly)
				{
					//var caller = $"{method.DeclaringType.FullName}.{method.Name}({string.Join(", ", method.GetParameters().Select(pi => pi.ParameterType.FullName))})";
					var caller = $"{method.DeclaringType.Namespace}: [{method.DeclaringType.Name}|{method.Name}] ";
					logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller)));
					return;
				}

				skip++;
			}
		}
	}
}