namespace OkromaContentPipeline
{
    /// <summary>
    /// Processor Result.
    /// </summary>
    /// <typeparam name="T">A Generic type</typeparam>
    public class ProcessorResult<T>
    {
        public T Result { get; }

        public ProcessorResult(T result)
        {
            Result = result;
        }
    }
}
