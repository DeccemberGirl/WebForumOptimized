namespace BLL.Infrastructure
{
    /// <summary>
    /// Holds information about operations statuses
    /// </summary>
    public class OperationDetails
    {
        /// <summary>
        /// Creates instance of <see cref="OperationDetails">class</see>
        /// </summary>
        /// <param name="succedeed">Bollean value which indicates whether the operation was successful or not</param>
        /// <param name="message">Message about operation status</param>
        /// <param name="prop">Property which was changed due to this operation</param>
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }

        /// <summary>
        /// Determines whether the operation was successful
        /// Returns true if yes, and false otherwice
        /// </summary>
        public bool Succedeed { get; private set; }

        /// <summary>
        /// Returns the status message of the operation
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Returns the property changed in process of this operation
        /// </summary>
        public string Property { get; private set; }
    }
}
