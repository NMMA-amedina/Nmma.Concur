using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;
using Api.Nmma.Configuration;
using NLog;

namespace Api.Nmma.Tracing
{
	/// <summary>
	/// 
	/// </summary>
	public class NLog : ITraceWriter
	{
		static readonly Logger classLogger = LogManager.GetCurrentClassLogger();

		static readonly Lazy<Dictionary<TraceLevel, Action<string>>> loggingMap = new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>> { {TraceLevel.Info, classLogger.Info}, {TraceLevel.Debug, classLogger.Debug}, {TraceLevel.Error, classLogger.Error}, {TraceLevel.Fatal, classLogger.Fatal}, {TraceLevel.Warn, classLogger.Warn} });

		Dictionary<TraceLevel, Action<string>> Logger
		{
			get { return loggingMap.Value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="category"></param>
		/// <param name="level"></param>
		/// <param name="action"></param>
		public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> action)
		{
			if (Record(request, level))
			{
				var record = new TraceRecord(request, category, level);
				action(record);
				Log(record);
			}
		}

		void Log(TraceRecord record)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(record.Request != null && record.Request.Method != null ? record.Request.Method.ToString() : String.Empty);
			sb.Append(record.Request != null && record.Request.RequestUri != null ? String.Format("{0}{1}", sb.Length > 0 ? " " : String.Empty, record.Request.RequestUri) : String.Empty);
			sb.Append(!String.IsNullOrWhiteSpace(record.Category) ? String.Format("{0}{1}", sb.Length > 0 ? " " : String.Empty, record.Category) : String.Empty);
			sb.Append(!String.IsNullOrWhiteSpace(record.Operator) ? String.Format("{0}{1} {2}", sb.Length > 0 ? " " : String.Empty, record.Operator, record.Operation) : String.Empty);
			sb.Append(!String.IsNullOrWhiteSpace(record.Message) ? String.Format("{0}{1}", sb.Length > 0 ? " " : String.Empty, record.Message) : String.Empty);
			sb.Append(record.Exception != null && !String.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message) ? String.Format("{0}{1}", sb.Length > 0 ? " " : String.Empty, record.Exception.GetBaseException().Message) : String.Empty);
			Logger[record.Level](sb.ToString());
		}

		bool Record(HttpRequestMessage request, TraceLevel level)
		{
			return level != TraceLevel.Off && GlobalWebApiConfiguration.Configuration.Tracing.Mode.Allows(request) && GlobalWebApiConfiguration.Configuration.Tracing.Levels.Any(x => x == level);
		}
	}
}