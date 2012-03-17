using System;
using System.Runtime.Serialization;

namespace Soltys.ProgrammerBot.Core.Level
{
    [Serializable]
    public class LevelFileException : Exception
    {
        public LevelFileException()
            : base() { }
        public LevelFileException(string message)
            : base(message) { }
        public LevelFileException(string message, Exception innerException)
            : base(message, innerException) { }
        protected LevelFileException(SerializationInfo info, StreamingContext streamingContext)
            : base(info, streamingContext) { }
    }
}
