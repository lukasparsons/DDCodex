using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Common.Exceptions
{
    public class ResourceNotFoundException<T> : Exception
    {
        public ResourceNotFoundException(string id) : base($"Resource of type {typeof(T).Name} with id {id} was not found.")
        {
        }

        public ResourceNotFoundException(string filter, string value) : base($"Resource of type {typeof(T).Name} with {filter} {value} was not found.")
        {
        }

        public ResourceNotFoundException(string message, Exception innerException = null!) : base(message, innerException)
        {
        }
    }
}
