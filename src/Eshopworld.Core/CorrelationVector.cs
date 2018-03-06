namespace Eshopworld.Core
{
    using System;
    using System.Text;

    public class CorrelationVector
    {
        public const string CorrelationVectorHeaderName = "X-Correlation-ID";

        internal string Id { get; set; }

        internal string PreviousDimensions { get; set; } = "";

        internal int CurrentDimension { get; set; } = 1;

        internal void Initialize()
        {
            Id = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }

        internal void Initialize(string vector)
        {
            Id = vector.Substring(0, vector.IndexOf(".", StringComparison.Ordinal));
            PreviousDimensions = vector.Substring(vector.IndexOf(".", StringComparison.Ordinal), vector.Length - vector.IndexOf(".", StringComparison.Ordinal));
        }

        internal void CheckInitialized()
        {
            if (string.IsNullOrEmpty(Id))
                throw new InvalidOperationException("This vector hasn't been initialized properly.\nIt needs initialization before it can be used.");
        }

        public void Increment()
        {
            CheckInitialized();

            CurrentDimension++;
        }

        public void Augment()
        {
            CheckInitialized();

            PreviousDimensions += $".{CurrentDimension}";
            CurrentDimension = 1;
        }

        public override string ToString()
        {
            CheckInitialized();

            return $"{Id}{PreviousDimensions}.{CurrentDimension}";
        }
    }
}
