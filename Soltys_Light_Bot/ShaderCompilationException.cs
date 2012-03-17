using System;
using System.Runtime.Serialization;
namespace Soltys.ProgrammerBot
{
    [Serializable]
    public class ShaderCompilationException : Exception
    {
        public ShaderCompilationException() : base() { }
        public ShaderCompilationException(string msg) : base(msg) { }
        public ShaderCompilationException(string msg, Exception exception) : base(msg, exception) { }
        protected ShaderCompilationException(SerializationInfo info, StreamingContext streamingContext) : base(info, streamingContext) { }
    }
}
