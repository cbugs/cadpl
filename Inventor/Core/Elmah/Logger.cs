using System;
using Elmah;
using System.Web;
using System.Diagnostics;

namespace Cadbury.Inventor.Core.Elmah
{
    public static class Logger
    {
        /// <summary>
        /// Trace will add Filename and Method and line number name from stacktrace
        /// </summary>
        /// <param name="error"></param>
        public static void Trace(string errorMessage, string dataString)
        {
            try
            {
                // frame 1, true for source info
                StackFrame frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                //var fileName = frame.GetFileName();
                //var lineNumber = frame.GetFileLineNumber();
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    var fileInfo = new FileInfo(frame.GetFileName());
                //    fileName = fileInfo.Name + ":" + lineNumber;
                //}

                var fileName = method.DeclaringType.Name;

                Trace(fileName, method.Name, errorMessage, dataString);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        [Obsolete("Please use Trace(string errorMessage, string dataString).", false)]
        public static void Trace(string filename, string methodname, string errorMessage)
        {
            //TraceException ex = new TraceException(string.Format("[{0}:{1}] => {2}]", filename, methodname, errorMessage));
            //ex.Source = string.Format("[{0}:{1}]", filename, methodname);
            //Log(ex);

            Trace(filename, methodname, errorMessage, "");
        }

        /// <summary>
        /// Warn will add Filename and Method name and line number from stacktrace
        /// </summary>
        /// <param name="error"></param>
        public static void Warn(string errorMessage, string dataString)
        {
            try
            {
                // frame 1, true for source info
                StackFrame frame = new StackFrame(1, true);
                var method = frame.GetMethod();
                //var fileName = frame.GetFileName();
                //var lineNumber = frame.GetFileLineNumber();
                //if (!string.IsNullOrEmpty(fileName))
                //{
                //    var fileInfo = new FileInfo(frame.GetFileName());
                //    fileName = fileInfo.Name + ":" + lineNumber;
                //}

                var fileName = method.DeclaringType.Name;

                Warn(fileName, method.Name, errorMessage, dataString);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        [Obsolete("Please use Warn(string errorMessage, string dataString).", false)]
        public static void Warn(string filename, string methodname, string errorMessage)
        {
            //WarnException ex = new WarnException(string.Format("[{0}:{1}] => {2}]", filename, methodname, errorMessage));
            //ex.Source = string.Format("[{0}:{1}]", filename, methodname);
            //Log(ex);

            Warn(filename, methodname, errorMessage, "");
        }

        public static void Log(Exception ex)
        {
            try
            {
                ErrorSignal.FromCurrentContext().Raise(ex);

                // VS Debug
                Debug.WriteLine(ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                    Debug.WriteLine(ex.StackTrace);
            }
            catch (Exception)
            {
                Error err = new Error(ex, HttpContext.Current);
                ErrorLog.GetDefault(HttpContext.Current).Log(err);
            }
        }

        #region Private methods

        private static void Trace(string filename, string methodname, string errorMessage, string dataString)
        {
            TraceException ex = new TraceException(string.Format("[{0}:{1}] => {2}  =>  {3}", filename, methodname, errorMessage, dataString));
            ex.Source = string.Format("{0}:{1}", filename, methodname);
            Log(ex);
        }

        private static void Warn(string filename, string methodname, string errorMessage, string dataString)
        {
            WarnException ex = new WarnException(string.Format("[{0}:{1}] => {2}  =>  {3}", filename, methodname, errorMessage, dataString));
            ex.Source = string.Format("{0}:{1}", filename, methodname);
            Log(ex);
        }

        #endregion
    }

    public class TraceException : System.ApplicationException
    {
        public TraceException(string message) : base(message) { }
    }

    public class WarnException : System.ApplicationException
    {
        public WarnException(string message) : base(message) { }
    }
}
