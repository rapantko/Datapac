namespace DatapacLibrary.Interfaces
{
    /// <summary>
    /// Generic interface that takes care of notification of any type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotificationService<T>
    {
        /// <summary>
        /// Generic method to send notification
        /// </summary>
        /// <param name="notification"></param>
        public void Send(T notification);
    }
}
